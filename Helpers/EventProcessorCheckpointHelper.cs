#region Copyright
//=======================================================================================
// Microsoft Azure Customer Advisory Team 
//
// This sample is supplemental to the technical guidance published on my personal
// blog at http://blogs.msdn.com/b/paolos/. 
// 
// Author: Paolo Salvatori
//=======================================================================================
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// LICENSED UNDER THE APACHE LICENSE, VERSION 2.0 (THE "LICENSE"); YOU MAY NOT USE THESE 
// FILES EXCEPT IN COMPLIANCE WITH THE LICENSE. YOU MAY OBTAIN A COPY OF THE LICENSE AT 
// http://www.apache.org/licenses/LICENSE-2.0
// UNLESS REQUIRED BY APPLICABLE LAW OR AGREED TO IN WRITING, SOFTWARE DISTRIBUTED UNDER THE 
// LICENSE IS DISTRIBUTED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY 
// KIND, EITHER EXPRESS OR IMPLIED. SEE THE LICENSE FOR THE SPECIFIC LANGUAGE GOVERNING 
// PERMISSIONS AND LIMITATIONS UNDER THE LICENSE.
//=======================================================================================
#endregion

#region Using Directives
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public class EventProcessorCheckpointHelper
    {
        #region Private Constants
        //***************************
        // Constants
        //***************************
        private const string FileName = "EventHubPartitionCheckpoints.json";
        private const string KeyFormat = "{0}-{1}-{2}";
        #endregion

        #region Private Static Fields
        private static readonly Dictionary<string, EventProcessorCheckpointInfo> mapDictionary = new Dictionary<string, EventProcessorCheckpointInfo>();
        private static readonly string filePath = Path.Combine(Environment.CurrentDirectory, FileName);
        private static List<EventProcessorCheckpointInfo> itemList;
        #endregion

        #region Public Static Methods
        /// <summary>
        /// Reads the checkpoints for event hub partitions from a JSON file in the current directory.
        /// </summary>
        public static void ReadCheckpoints()
        {
            lock (filePath)
            {
                if (!File.Exists(filePath))
                {
                    itemList = new List<EventProcessorCheckpointInfo>();
                    return;
                }
                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    itemList = JsonSerializerHelper.Deserialize<List<EventProcessorCheckpointInfo>>(stream) ?? new List<EventProcessorCheckpointInfo>();
                }
                if (itemList == null || itemList.Count == 0)
                {
                    itemList = new List<EventProcessorCheckpointInfo>();
                    return;
                }
                foreach (var item in itemList)
                {
                    mapDictionary.Add(string.Format(KeyFormat, item.Namespace, item.EventHub, item.ConsumerGroup), item);
                }
            }
        }

        /// <summary>
        /// Writes the checkpoints for event hub partitions to a JSON file in the current directory.
        /// </summary>
        public static void WriteCheckpoints()
        {
            if (itemList.Count == 0)
            {
                return;
            }
            JsonSerializerHelper.Serialize(itemList, Formatting.Indented);
            WriteFile(filePath, JsonSerializerHelper.Serialize(itemList, Formatting.Indented));
        }

        /// <summary>
        /// Gets the Leases, if any persisted to file, for an event hub partitions in the current namespace.
        /// </summary>
        /// <param name="ns">Namespace name.</param>
        /// <param name="eventHub">Event hub name.</param>
        /// <param name="consumerGroup"></param>
        /// <returns>The in-memory or persisted lease for the event hub partition.</returns>
        public static Dictionary<string, Lease> GetLease(string ns, string eventHub, string consumerGroup)
        {
            if (string.IsNullOrWhiteSpace(ns) ||
                string.IsNullOrWhiteSpace(eventHub) ||
                string.IsNullOrWhiteSpace(consumerGroup))
            {
                return null;
            }
            var key = string.Format(KeyFormat, ns, eventHub, consumerGroup);
            return mapDictionary.ContainsKey(key) ?
                   mapDictionary[key].Leases :
                   null;
        }

        /// <summary>
        /// Gets the Lease, if any persisted to file, for an event hub partition in the current namespace.
        /// </summary>
        /// <param name="ns">Namespace name.</param>
        /// <param name="eventHub">Event hub name.</param>
        /// <param name="consumerGroup">Consumer group</param>
        /// <param name="partitionId">Partition Id.</param>
        /// <returns>The in-memory or persisted lease for the event hub partition.</returns>
        public static Lease GetLease(string ns, string eventHub, string consumerGroup, string partitionId)
        {
            if (string.IsNullOrWhiteSpace(ns) ||
                string.IsNullOrWhiteSpace(eventHub) ||
                string.IsNullOrWhiteSpace(consumerGroup) ||
                string.IsNullOrWhiteSpace(partitionId))
            {
                return null;
            }
            var key = string.Format(KeyFormat, ns, eventHub, consumerGroup);
            var value = mapDictionary.ContainsKey(key) &&
                   mapDictionary[key].Leases != null && 
                   mapDictionary[key].Leases.ContainsKey(partitionId) ?
                   mapDictionary[key].Leases[partitionId] :
                   GetDefaultLease(partitionId);
            return value;
        }

        /// <summary>
        /// Sets the Lease for an event hub partition in the current namespace. If the lease is null,
        /// the function removes the lease from the internal structure.
        /// </summary>
        /// <param name="ns">Namespace name.</param>
        /// <param name="eventHub">Event hub name.</param>
        /// <param name="consumerGroup">Consumer group</param>
        /// <param name="leases">A dictionary of partition leases.</param>
        public static void SetLease(string ns, string eventHub, string consumerGroup, Dictionary<string, Lease> leases)
        {
            if (string.IsNullOrWhiteSpace(ns) ||
                string.IsNullOrWhiteSpace(eventHub) ||
                string.IsNullOrWhiteSpace(consumerGroup))
            {
                return;
            }
            var key = string.Format(KeyFormat, ns, eventHub, consumerGroup);
            if (mapDictionary.ContainsKey(key))
            {
                if (leases != null)
                {
                    mapDictionary[key].Leases = leases;
                }
                else
                {
                    lock (mapDictionary)
                    {
                        itemList.Remove(mapDictionary[key]);
                        mapDictionary.Remove(key);
                    }
                }
            }
            else
            {
                if (leases == null)
                {
                    return;
                }

                var item = new EventProcessorCheckpointInfo 
                {
                    Namespace = ns, 
                    EventHub = eventHub, 
                    ConsumerGroup =  consumerGroup,    
                    Leases = leases
                };
                lock (mapDictionary)
                {
                    itemList.Add(item);
                    mapDictionary.Add(key, item);
                }
            }
        }

        /// <summary>
        /// Sets the Lease for an event hub partition in the current namespace. If the lease is null,
        /// the function removes the lease from the internal structure.
        /// </summary>
        /// <param name="ns">Namespace name.</param>
        /// <param name="eventHub">Event hub name.</param>
        /// <param name="consumerGroup"></param>
        /// <param name="partitionId">Partition Id.</param>
        /// <param name="lease">The lease for the event hub partition.</param>
        public static void SetLease(string ns, string eventHub, string consumerGroup, string partitionId, Lease lease)
        {
            if (string.IsNullOrWhiteSpace(ns) ||
                string.IsNullOrWhiteSpace(eventHub) ||
                string.IsNullOrWhiteSpace(consumerGroup) ||
                string.IsNullOrWhiteSpace(partitionId))
            {
                return;
            }
            var key = string.Format(KeyFormat, ns, eventHub, consumerGroup);
            if (mapDictionary.ContainsKey(key))
            {
                if (lease != null)
                {
                    if (mapDictionary[key].Leases == null)
                    {
                        mapDictionary[key].Leases = new Dictionary<string, Lease>();
                    }
                    mapDictionary[key].Leases[partitionId] = lease;
                }
                else
                {
                    if (mapDictionary[key].Leases == null || 
                        !mapDictionary[key].Leases.ContainsKey(partitionId))
                    {
                        return;
                    }
                    mapDictionary[key].Leases.Remove(partitionId);
                }
            }
            else
            {
                if (lease == null)
                {
                    return;
                }
                var item = new EventProcessorCheckpointInfo
                {
                    Namespace = ns,
                    EventHub = eventHub,
                    ConsumerGroup = consumerGroup,
                    Leases = new Dictionary<string, Lease> { { partitionId, lease } }
                };
                lock (mapDictionary)
                {
                    itemList.Add(item);
                    if (mapDictionary.ContainsKey(key))
                    {
                        mapDictionary[key].Leases[partitionId] = lease;
                    }
                    else
                    {
                        mapDictionary.Add(key, item);
                    }
                }
            }
            if (lease != null)
            {
                mapDictionary[key].Leases[partitionId] = lease;
            }
            else
            {
                mapDictionary[key].Leases.Remove(partitionId);
            }
        }

        public static Task CheckpointAsync(string ns, string eventHub, string consumerGroup, Lease lease, string offset, long sequenceNumber)
        {
            if (string.IsNullOrWhiteSpace(ns) ||
                string.IsNullOrWhiteSpace(eventHub) ||
                string.IsNullOrWhiteSpace(consumerGroup) ||
                lease == null ||
                string.IsNullOrWhiteSpace(offset))
            {
                Task.FromResult<object>(null); 
            }
            if (lease != null && !string.IsNullOrWhiteSpace(offset))
            {
                lease.Offset = offset;
                SetLease(ns, eventHub, consumerGroup, lease.PartitionId, lease);
            }
            return Task.FromResult<object>(null);
        } 
        #endregion

        #region Private Static Methods
        private static void WriteFile(string path, string content)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(path) || string.IsNullOrWhiteSpace(content))
                {
                    return;
                }

                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                using (var writer = new StreamWriter(path))
                {
                    writer.Write(content);
                    writer.Flush();
                }
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
            }
        }
        #endregion

        #region Private Static Methods
        private static Lease GetDefaultLease(string partitionId)
        {
            return new Lease
            {
                PartitionId = partitionId,
                Epoch = 0,
                SequenceNumber = 0,
                Offset = null,
                Owner = "ServiceBusExplorer",
                Token = null
            };
        }
        #endregion
    }
}
