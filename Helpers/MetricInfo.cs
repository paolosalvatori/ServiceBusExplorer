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
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public class MetricInfo
    {
        #region Private Constants
        //***************************
        // Formats
        //***************************
        private const string ExceptionFormat = "Exception: {0}";
        private const string InnerExceptionFormat = "InnerException: {0}";
        private const string RetrievingMetricsFormat = "Retrieving metrics for the [{0}] entity...";
        private const string NoMetricRetrievedFormat = "No metric was retrieved for the [{0}] entity. Check the subscriptionId and certificateThumbprint settings in the configuration file.";
        private const string MetricSuccessfullyRetrievedFormat = "[{0}] metrics successfully retrieved for the [{1}] entity.";
        private const string NoSubscriptionIdOrCertificateThumbprint = "Warning: the subscriptionId or certificateThumbprint settings are not defined in the configuration file.\n\rSpecify your Azure subscription ID and the thumbprint of an Azure management certificate in the local machine or current user store, if you want to use entity metrics.";

        //***************************
        // Entities
        //***************************
        private const string QueueEntity = "Queue";
        private const string TopicEntity = "Topic";
        private const string SubscriptionEntity = "Subscription";
        private const string EventHubEntity = "Event Hub";
        private const string ConsumerGroupEntity = "Consumer Group";
        private const string NotificationHubEntity = "Notification Hub";
        private const string RelayEntity = "Relay";

        //***************************
        // Constants
        //***************************
        private const string QueueEntities = "queues";
        private const string TopicEntities = "topics";
        private const string EventHubEntities = "eventhubs";
        private const string NotificationHubEntities = "notificationhubs";
        private const string RelayEntities = "relays";
        #endregion

        #region Public Instance Properties
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string PrimaryAggregation { get; set; }
        public List<Rollup> Rollups { get; set; } 
        #endregion

        #region Public Static Constructor
        static MetricInfo()
        {
            EntityMetricDictionary = new Dictionary<string, BindingList<MetricInfo>>();
        }
        #endregion

        #region Private Static Fields

        private static readonly Dictionary<string, string> entityToUrlSegmentMapDictionary = new Dictionary<string, string>
        {
            {QueueEntity, QueueEntities},
            {TopicEntity, TopicEntities},
            {SubscriptionEntity, TopicEntities},
            {EventHubEntity, EventHubEntities},
            {ConsumerGroupEntity, EventHubEntities},
            {NotificationHubEntity, NotificationHubEntities},
            {RelayEntity, RelayEntities}
        };

        private static bool warningShown;
        private static bool retrieveMetrics = true;
        #endregion

        #region Public Static Properties
        public static Dictionary<string, BindingList<MetricInfo>> EntityMetricDictionary { get; private set; }
        #endregion

        #region Public Static Properties

        public async static Task GetMetricInfoListAsync(string ns, string entityType, string entityPath)
        {
            try
            {
                if (!retrieveMetrics)
                {
                    return;
                }
                if (string.IsNullOrWhiteSpace(ns) ||
                string.IsNullOrEmpty(entityType) ||
                string.IsNullOrWhiteSpace(entityPath) ||
                !entityToUrlSegmentMapDictionary.ContainsKey(entityType) ||
                EntityMetricDictionary.ContainsKey(entityType))
                {
                    return;
                }
                if (string.IsNullOrWhiteSpace(MainForm.SingletonMainForm.SubscriptionId) ||
                    string.IsNullOrWhiteSpace(MainForm.SingletonMainForm.CertificateThumbprint))
                {
                    if (!warningShown)
                    {
                        Trace.WriteLine(NoSubscriptionIdOrCertificateThumbprint);
                        warningShown = true;
                    }
                    return;
                }
                var uri = MetricHelper.BuildUriForDataPointDiscoveryQuery(MainForm.SingletonMainForm.SubscriptionId,
                                                                          ns,
                                                                          entityToUrlSegmentMapDictionary[entityType],
                                                                          entityPath);
                var enumerable = await MetricHelper.GetSupportedMetricsAsync(uri, MainForm.SingletonMainForm.CertificateThumbprint);
                Trace.WriteLine(string.Format(RetrievingMetricsFormat, entityType));
                if (enumerable == null)
                {
                    retrieveMetrics = false;
                    Trace.WriteLine(string.Format(NoMetricRetrievedFormat, entityType));
                    return;
                }
                var metricInfoList = enumerable.ToList();
                var count = metricInfoList.Count;
                metricInfoList.Insert(0, new MetricInfo { DisplayName = "All", Name = "all", Unit = "Requests", PrimaryAggregation = "Total" });
                foreach (var item in metricInfoList)
                {
                    item.DisplayName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.DisplayName.Replace('.', ' '));
                }
                if (!metricInfoList.Any())
                {
                    return;
                }
                EntityMetricDictionary[entityType] = new BindingList<MetricInfo>(metricInfoList.ToList())
                {
                    AllowEdit = true,
                    AllowNew = true,
                    AllowRemove = true
                };
                Trace.WriteLine(string.Format(MetricSuccessfullyRetrievedFormat, count, entityType));
            }
            catch (ArgumentException ex)
            {
                if (string.Compare(ex.ParamName, "certificateThumbprint", StringComparison.OrdinalIgnoreCase) != 0)
                {
                    HandleException(ex);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        #endregion

        #region Private Static Methods
        private static void HandleException(Exception ex)
        {
            if (ex == null || string.IsNullOrWhiteSpace(ex.Message))
            {
                return;
            }
            Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, ExceptionFormat, ex.Message));
            if (ex.InnerException != null && !string.IsNullOrWhiteSpace(ex.InnerException.Message))
            {
                Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, InnerExceptionFormat, ex.InnerException.Message));
            }
        }
        #endregion
    }

    public class Rollup
    {
        public TimeSpan TimeGrain { get; set; }
        public TimeSpan Retention { get; set; }
        public ICollection<MetricValue> Values { get; set; }
    }
}
