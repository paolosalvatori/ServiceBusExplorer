// <copyright file="IEventGridClient.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.Azure.Management.EventGridV2
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface used to talk to EventGrid.
    /// </summary>
    public interface IEventGridControlPlaneClient
    {
        /// <summary>
        /// Create an event subscription under a namespace topic.
        /// </summary>
        /// <param name="resourceGroupName">Resource group for which the namespace is a part of</param>
        /// <param name="namespaceName">name of the namespace</param>
        /// <param name="namespaceTopicName">name of the namespace topic</param>
        /// <param name="subscriptionName">name of subscription </param>
        /// <param name="deliveryMode">delivery mode</param>
        /// <param name="filters">a map of all selected filters</param>
        /// <param name="eventTypes">a list of event types used in filtering</param>
        Task<string> CreateNamespaceTopicEventSubscriptionAsync(
           string resourceGroupName,
           string namespaceName,
           string namespaceTopicName,
           string subscriptionName,
           string deliveryMode,
           List<Dictionary<string, string>> filters,
           List<string> eventTypes);

        /// <summary>
        /// Create a namespace topic in a tenant's namespace.
        /// </summary>
        /// <param name="resourceGroupName">Resource group for which the namespace is a part of</param>
        /// <param name="namespaceName">name of the namespace</param>
        /// <param name="namespaceTopicName">name of the namespace topic</param>
        Task<string> CreateNamespaceTopicAsync(
           string resourceGroupName,
           string namespaceName,
           string namespaceTopicName);

        /// <summary>
        /// Delete a namespace topic event subscription.
        /// </summary>
        /// <param name="resourceGroupName">Resource group for which the namespace is a part of</param>
        /// <param name="namespaceName">name of the namespace</param>
        /// <param name="namespaceTopicName">name of the namespace topic</param>
        /// <param name="subscriptionName">name of the subscription</param>
        Task<string> DeleteNamespaceTopicEventSubscriptionAsync(
            string resourceGroupName, 
            string namespaceName, 
            string namespaceTopicName, 
            string subscriptionName);

        /// <summary>
        /// Delete a namespace topic in a tenant's namespace.
        /// </summary>
        /// <param name="resourceGroupName">Resource group for which the namespace is a part of</param>
        /// <param name="namespaceName">name of the namespace</param>
        /// <param name="namespaceTopicName">name of the namespace topic</param>
        Task<string> DeleteNamespaceTopicAsync(
            string resourceGroupName, 
            string namespaceName, 
            string namespaceTopicName);

    }
}
