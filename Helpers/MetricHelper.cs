#region Copyright
//=======================================================================================
// Windows Azure Customer Advisory Team  
//
// This sample is supplemental to the technical guidance published on the community
// blog at http://www.appfabriccat.com/. 
// 
// Author: Paolo Salvatori
//=======================================================================================
// Copyright © 2011 Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER 
// EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF 
// MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE. YOU BEAR THE RISK OF USING IT.
//=======================================================================================
#endregion

#region Using Directives
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public static class MetricHelper
    {
        #region Private Constants
        //***************************
        // Formats
        //***************************
        //private const string MetricsUriBatchFormat = "https://management.core.windows.net/{0}/services/ServiceBus/Namespaces/{1}/$batch";
        private const string MetricsUriFormatDiscover = "https://management.core.windows.net/{0}/services/ServiceBus/Namespaces/{1}/{2}/metrics";
        private const string MetricsUriFormatDiscoverRollup = "https://management.core.windows.net/{0}/services/ServiceBus/Namespaces/{1}/{2}/metrics/{3}/rollups";
        private const string OneFilterMetricsUriFormat = "https://management.core.windows.net/{0}/services/ServiceBus/Namespaces/{1}/{2}/metrics/{3}/rollups/{4}/Values/?$filter=Timestamp {5} datetime'{6}'";
        private const string TwoFiltersMetricsUriFormat = "https://management.core.windows.net/{0}/services/ServiceBus/Namespaces/{1}/{2}/metrics/{3}/rollups/{4}/Values/?$filter=Timestamp {5} datetime'{6}' and Timestamp {7} datetime'{8}'";
        private const string ODataBatchOneFilterMetricsUriFormat = "https://management.core.windows.net/{0}/Namespaces/{1}/{2}/metrics/{3}/rollups/{4}/Values/?$filter=Timestamp {5} datetime'{6}'";
        private const string ODataBatchTwoFiltersMetricsUriFormat = "https://management.core.windows.net/{0}/Namespaces/{1}/{2}/metrics/{3}/rollups/{4}/Values/?$filter=Timestamp {5} datetime'{6}' and Timestamp {7} datetime'{8}'";
        private const string TimestampFormat = "yyyy-MM-ddTHH:mm:ss";
        private const string ExceptionFormat = "An error occurred while retrieving [{0}] metric data for the [{1}] entity:\n\r{2}";
        private const string SubscriptionFormat = "{0}{1}{2}";
        private const string MetricDataSuccessfullyRetrieved = "[{0}] metric data for the [{1}] entity has been successfully retrieved.";

        //***************************
        // Messages
        //***************************
        private const string UriCannotBeNull = "The uri cannot be null.";
        private const string UrisCannotBeNull = "The uris cannot be null.";
        private const string CertificateThumbPrintCannotBeNull = "The certificate thumbprint cannot be null.";
        private const string CertificateCannotBeNull = "The certificate cannot be null.";
        private const string CertificateCannotBeFound = "A certificate with thumbprint '{0}' cannot be found.";
        private const string SubscriptionIdCannotBeNull = "The subscriptionId parameter cannot be null.";
        private const string NamespaceCannotBeNull = "The namespace parameter cannot be null.";

        //***************************
        // Constants
        //***************************
        private const string JsonContentType = "application/json";
        private const string RdfeHeader = "x-ms-version";
        private const string RdfeHeaderValue = "2012-03-01";
        private const string QueueEntity = "Queue";
        private const string TopicEntity = "Topic";
        private const string SubscriptionEntity = "Subscription";
        private const string Unknown = "Unkown";
        private const string Metrics = "metrics/";
        private const string Namespaces = "namespaces/";

        //***************************
        // Metrics
        //***************************
        private const string QueueMetricFormat = "Queues/{0}";
        private const string TopicMetricFormat = "Topics/{0}";
        private const string SubscriptionMetricFormat = "Topics/{0}";
        #endregion

        #region Public Static Methods
        public static Uri BuildUriForDataPointDiscoveryQuery(string subscriptionId,
                                                             string namespaceName,
                                                             string entityPath)
        {
            return new Uri(string.Format(MetricsUriFormatDiscover,
                                         subscriptionId,
                                         namespaceName,
                                         entityPath));
        }

        public static Uri BuildUriForRollupDiscoveryQuery(string subscriptionId,
                                                          string namespaceName,
                                                          string entityPath,
                                                          string dataPoint)
        {
            return new Uri(string.Format(MetricsUriFormatDiscoverRollup,
                                         subscriptionId,
                                         namespaceName,
                                         entityPath,
                                         dataPoint));
        }

        public static Uri BuildUriForDataPointMetricQuery(string subscriptionId,
                                                          string namespaceName,
                                                          string entityPath,
                                                          string metric,
                                                          string granularity,
                                                          string timeFilterOperator,
                                                          string timeFiltervalue,
                                                          bool oDataBatch = false)
        {
            return new Uri(string.Format(oDataBatch ?
                                         ODataBatchOneFilterMetricsUriFormat :
                                         OneFilterMetricsUriFormat,
                                         subscriptionId,
                                         namespaceName,
                                         entityPath,
                                         metric,
                                         granularity,
                                         timeFilterOperator,
                                         timeFiltervalue));
        }

        public static Uri BuildUriForDataPointMetricQuery(string subscriptionId,
                                                          string namespaceName,
                                                          string entityPath,
                                                          string metric,
                                                          string granularity,
                                                          string timeFilterOperator1,
                                                          string timeFiltervalue1,
                                                          string timeFilterOperator2,
                                                          string timeFiltervalue2,
                                                          bool oDataBatch = false)
        {
            return new Uri(string.Format(oDataBatch ?
                                         ODataBatchTwoFiltersMetricsUriFormat :
                                         TwoFiltersMetricsUriFormat,
                                         subscriptionId,
                                         namespaceName,
                                         entityPath,
                                         metric,
                                         granularity,
                                         timeFilterOperator1,
                                         timeFiltervalue1,
                                         timeFilterOperator2,
                                         timeFiltervalue2));
        }

        public static IEnumerable<Uri> BuildUriListForDataPointMetricQueries(string subscriptionId,
                                                                             string namespaceName, 
                                                                             IEnumerable<MetricDataPoint> dataPoints,
                                                                             bool oDataBatch = false)
        {
            if (string.IsNullOrWhiteSpace(subscriptionId))
            {
                throw new ArgumentException(SubscriptionIdCannotBeNull);
            }
            if (string.IsNullOrWhiteSpace(namespaceName))
            {
                throw new ArgumentException(NamespaceCannotBeNull);
            }
            var uris = new List<Uri>();
            foreach (var dataPoint in dataPoints.Where(dataPoint => dataPoint != null &&
                                                       !string.IsNullOrWhiteSpace(dataPoint.Entity) &&
                                                       !string.IsNullOrWhiteSpace(dataPoint.Type) &&
                                                       !string.IsNullOrWhiteSpace(dataPoint.Metric) &&
                                                       !string.IsNullOrWhiteSpace(dataPoint.Granularity)))
            {
                try
                {
                    string format;

                    switch (dataPoint.Type)
                    {
                        case QueueEntity:
                            format = QueueMetricFormat;
                            break;
                        case TopicEntity:
                            format = TopicMetricFormat;
                            break;
                        case SubscriptionEntity:
                            format = SubscriptionMetricFormat;
                            break;
                        default:
                            format = QueueMetricFormat;
                            break;
                    }
                    var path = string.Format(format, dataPoint.Entity);
                    var filter1Defined = !string.IsNullOrWhiteSpace(dataPoint.FilterOperator1) &&
                                         !string.IsNullOrWhiteSpace(dataPoint.FilterValue1);
                    var filter2Defined = !string.IsNullOrWhiteSpace(dataPoint.FilterOperator2) &&
                                         !string.IsNullOrWhiteSpace(dataPoint.FilterValue2);
                    if (!filter1Defined && !filter2Defined)
                    {
                        uris.Add(BuildUriForDataPointMetricQuery(subscriptionId,
                                                                 namespaceName,
                                                                 path,
                                                                 dataPoint.Metric,
                                                                 dataPoint.Granularity,
                                                                 "ge",
                                                                 DateTime.UtcNow.Subtract(TimeSpan.FromHours(1)).ToString(TimestampFormat),
                                                                 oDataBatch));
                        continue;
                    }
                    if (filter1Defined && !filter2Defined)
                    {
                        uris.Add(BuildUriForDataPointMetricQuery(subscriptionId,
                                                                 namespaceName,
                                                                 path,
                                                                 dataPoint.Metric,
                                                                 dataPoint.Granularity,
                                                                 dataPoint.FilterOperator1,
                                                                 DateTime.Parse(dataPoint.FilterValue1).ToUniversalTime().ToString(TimestampFormat),
                                                                 oDataBatch));
                        continue;
                    }
                    if (!filter1Defined && filter2Defined)
                    {
                        uris.Add(BuildUriForDataPointMetricQuery(subscriptionId,
                                                                 namespaceName,
                                                                 path,
                                                                 dataPoint.Metric,
                                                                 dataPoint.Granularity,
                                                                 dataPoint.FilterOperator2,
                                                                 DateTime.Parse(dataPoint.FilterValue2).ToUniversalTime().ToString(TimestampFormat),
                                                                 oDataBatch));
                        continue;
                    }
                    uris.Add(BuildUriForDataPointMetricQuery(subscriptionId,
                                                             namespaceName,
                                                             path,
                                                             dataPoint.Metric,
                                                             dataPoint.Granularity,
                                                             dataPoint.FilterOperator1,
                                                             DateTime.Parse(dataPoint.FilterValue1).ToUniversalTime().ToString(TimestampFormat),
                                                             dataPoint.FilterOperator2,
                                                             DateTime.Parse(dataPoint.FilterValue2).ToUniversalTime().ToString(TimestampFormat),
                                                             oDataBatch));
                }
                catch (Exception ex)
                {
                    MainForm.SingletonMainForm.HandleException(ex);
                }
            }
            return uris;
        }

        public static IEnumerable<IEnumerable<MetricValue>> ReadMetricDataUsingTasks(IEnumerable<Uri> uris, string certificateThumbprint)
        {
            if (certificateThumbprint == null)
            {
                throw new ArgumentException(CertificateThumbPrintCannotBeNull);
            }

            return ReadMetricDataUsingTasks(uris, GetManagementCertificate(certificateThumbprint));
        }

        public static IEnumerable<IEnumerable<MetricValue>> ReadMetricDataUsingTasks(IEnumerable<Uri> uris, X509Certificate2 certificate)
        {
            if (uris == null)
            {
                throw new ArgumentException(UrisCannotBeNull);
            }

            if (certificate == null)
            {
                throw new ArgumentException(CertificateCannotBeNull);
            }

            var taskList = uris.Select(uri => Task.Factory.StartNew(u => InternalReadMetricData((Uri)u, certificate), uri)).ToList();
            // ReSharper disable CoVariantArrayConversion
            try
            {
                Task.WaitAll(taskList.ToArray());
            }
            catch (AggregateException ex)
            {
                ex.Handle(e => true);
            }
            // ReSharper restore CoVariantArrayConversion
            return taskList.Select(task => task.IsCompleted && !task.IsFaulted && task.Result != null ? task.Result : null).ToList();
        } 

        public static IEnumerable<MetricValue> ReadMetricData(Uri uri, string certificateThumbprint)
        {
            if (certificateThumbprint == null)
            {
                throw new ArgumentException(CertificateThumbPrintCannotBeNull);
            }

            return ReadMetricData(uri, GetManagementCertificate(certificateThumbprint));
        }

        public static IEnumerable<MetricValue> ReadMetricData(Uri uri, X509Certificate2 certificate)
        {
            if (uri == null)
            {
                throw new ArgumentException(UriCannotBeNull);
            }

            if (certificate == null)
            {
                throw new ArgumentException(CertificateCannotBeNull);
            }

            return InternalReadMetricData(uri, certificate);
        }
        #endregion

        #region Private Static Methods
        private static X509Certificate2 GetManagementCertificate(string certificateThumbprint)
        {
            var locations = new List<StoreLocation> 
            { 
                StoreLocation.LocalMachine ,
                StoreLocation.CurrentUser
            };

            foreach (var store in locations.Select(location => new X509Store(StoreName.My, location)))
            {
                try
                {
                    store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                    var certificates = store.Certificates.Find(X509FindType.FindByThumbprint, certificateThumbprint, false);
                    if (certificates.Count > 0)
                    {
                        return certificates[0];
                    }
                }
                finally
                {
                    store.Close();
                }
            }

            throw new ArgumentException(string.Format(CertificateCannotBeFound, certificateThumbprint));
        }

        private static IEnumerable<MetricValue> InternalReadMetricData(Uri uri, X509Certificate2 certificate)
        {
            var request = WebRequest.Create(uri) as HttpWebRequest;

            try
            {
                // Add Windows Azure subscription management Certificate to the request
                if (request != null)
                {
                    request.ClientCertificates.Add(certificate);
                    request.Accept = JsonContentType;
                    //create the request headers and specify the method required for this type of operation
                    request.Headers.Add(RdfeHeader, RdfeHeaderValue);
                    request.ContentType = JsonContentType;
                    request.Method = "GET";
                    request.KeepAlive = true;
                    using (var response = request.GetResponse() as HttpWebResponse)
                    {
                        if (response != null &&
                            response.StatusCode == HttpStatusCode.OK)
                        {
                            using (var stream = response.GetResponseStream())
                            {
                                if (stream != null)
                                {
                                    string metric, entity;
                                    GetMetricAndEntityFromUri(uri, out metric, out entity);
                                    Trace.WriteLine(string.Format(MetricDataSuccessfullyRetrieved, metric, entity));
                                    return JsonSerializerHelper.Deserialize(stream, typeof(MetricValue[])) as MetricValue[];
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(CreateExceptionMessage(uri, ex));
                throw;
            }
            return null;
        }

        private static void GetMetricAndEntityFromUri(Uri uri, out string metric, out string entity)
        {
            metric = Unknown;
            entity = Unknown;
            if (uri == null)
            {
                return;
            }
            var uriString = uri.ToString();
            if (string.IsNullOrWhiteSpace(uriString))
            {
                return;
            }
            if (uri.Segments.Count() > 7)
            {
                int j = 0, k = 0;
                for (var i = 0; i < uri.Segments.Count(); i++)
                {
                    if (string.Compare(Namespaces, uri.Segments[i], StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        j = i;
                        continue;
                    }
                    if (string.Compare(Metrics, uri.Segments[i], StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        k = i + 1;
                        break;
                    }
                }
                metric = !string.IsNullOrWhiteSpace(uri.Segments[k])
                                 ? uri.Segments[k].Substring(0, uri.Segments[k].Length - 1)
                                 : Unknown;

                if (k > j + 5)
                {
                    // The entity is a subscription
                    entity = string.Format(SubscriptionFormat,
                                           uri.Segments[j + 3],
                                           uri.Segments[j + 4],
                                           uri.Segments[j + 5].Substring(0, uri.Segments[j + 5].Length - 1));
                }
                else
                {
                    // The entity is a queue or topic
                    entity = uri.Segments[j + 3].Substring(0, uri.Segments[j + 3].Length - 1);
                }
            }
        }

        private static string CreateExceptionMessage(Uri uri, Exception ex)
        {
            string metric, entity;
            GetMetricAndEntityFromUri(uri, out metric, out entity);
            return string.Format(ExceptionFormat,
                                 metric, 
                                 entity,
                                 ex.Message);
        }
        #endregion
    }
}
