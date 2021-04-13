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

using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using System.Threading.Tasks;

namespace ServiceBusExplorer.ServiceBus.Helpers
{
    public class TopicSubscriptionServiceBusPurger : ServiceBusPurger<SubscriptionProperties>
    {
        public TopicSubscriptionServiceBusPurger(ServiceBusHelper2 serviceBusHelper) : base(serviceBusHelper) { }

        protected override ServiceBusReceiver CreateServiceBusReceiver(SubscriptionProperties entity, ServiceBusClient client, bool purgeDeadLetterQueueInstead)
        {
            return client.CreateReceiver(
                entity.TopicName,
                entity.SubscriptionName,
                new ServiceBusReceiverOptions
                {
                    PrefetchCount = 50,
                    ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete,
                    SubQueue = purgeDeadLetterQueueInstead ? SubQueue.DeadLetter : SubQueue.None
                });
        }

        protected async override Task<ServiceBusSessionReceiver> CreateServiceBusSessionReceiver(SubscriptionProperties entity, ServiceBusClient client, bool purgeDeadLetterQueueInstead)
        {

            return await client.AcceptNextSessionAsync(
                entity.TopicName,
                entity.SubscriptionName,
                new ServiceBusSessionReceiverOptions
                {
                    PrefetchCount = 10,
                    ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete
                })
                .ConfigureAwait(false);
        }

        protected override bool EntityRequiresSession(SubscriptionProperties entity)
        {
            return entity.RequiresSession;
        }

        protected override string GetEntityPath(SubscriptionProperties entity)
        {
            return $"{entity.TopicName}/subscriptions/{entity.SubscriptionName}";
        }

        protected async override Task<long> GetMessageCount(SubscriptionProperties entity, bool deadLetterQueueData)
        {
            var client = new ServiceBusAdministrationClient(serviceBusHelper.ConnectionString);

            if (deadLetterQueueData)
            {

                var runtimeInfoResponse = await client.GetSubscriptionRuntimePropertiesAsync(
                    entity.TopicName,
                    entity.SubscriptionName)
                    .ConfigureAwait(false);

                return runtimeInfoResponse.Value.DeadLetterMessageCount;
            }
            else
            {
                var runtimeInfo = await client.GetSubscriptionRuntimePropertiesAsync(
                    entity.TopicName,
                    entity.SubscriptionName)
                    .ConfigureAwait(false);

                return runtimeInfo.Value.ActiveMessageCount;
            }
        }
    }
}
