<div>**Author: **Paolo Salvatori (@babosbird)</div>

# Introduction

<div>

**Important Note**

The tool is also available on MSDN Code Gallery at [Service Bus Explorer](http://code.msdn.microsoft.com/windowsazure/Service-Bus-Explorer-f2abca5a)


<div>Queues and topics represent the foundation of a new cloud-based messaging and integration infrastructure that provides reliable message queuing and durable publish/subscribe messaging capabilities to both cloud and on-premises applications based on Microsoft  and non-Microsoft technologies. .NET applications can use the new functionality offered by queues and topics by using the new messaging API ([Microsoft.ServiceBus.Messaging](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.aspx))  or via WCF by using the new [ NetMessagingBinding](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.netmessagingbinding.aspx). Likewise, any Microsoft or non-Microsoft applications can use a [Service Bus REST API](http://msdn.microsoft.com/en-us/library/hh367521.aspx) to manage and access messaging entities over HTTPS. In November 2012, the&nbsp;[Service Bus for  Windows Server](http://msdn.microsoft.com/en-us/library/jj193022(v=azure.10).aspx) was released. The Service Bus Exploer can be used with manage namespaces hosted by both Windows Azure Service Bus and Service Bus for Windows Server.</div>
<div>Queues and topics were first introduced by the Community Technology Preview (CTP) of Windows Azure that was released in May 2011. At that time, the [Windows Azure Management Portal](https://windows.azure.com/default.aspx) didn&rsquo;t provide any user interface to administer, create and delete messaging entities and the only way to accomplish this task was using the .NET or REST API. For this  reason, In June 2011 I decided to build a tool called [ Service Bus Explorer](http://windowsazurecat.com/2011/07/exploring-topics-and-queues-by-building-a-service-bus-explorer-toolpart-1/) that would allow developers and system administrators to connect to a Service Bus namespace and administer its messaging entities.</div>
<div style="text-align: center;">&nbsp;</div>
<div>![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/72776/1/servicebusexplorer.png)</div>
<div>During the last few months I continued to develop this tool and add new features with the intended goal to facilitate the development and administration of new Service Bus-enabled applications. In the meantime, the [Windows Azure Management Portal](https://windows.azure.com/default.aspx) introduced the ability for a user to create, disable, update&nbsp;queues, topics, and subscriptions and define their properties, but not to define or display rules for an existing  subscription. Besides, the Service Bus Explorer enables to accomplish functionalities, such as importing, exporting and testing entities, that are not currently provided by the [Windows Azure Management Portal](https://windows.azure.com/default.aspx). For this reason, the Service Bus Explorer tool represents the perfect companion for the official Azure portal and it can also be used to explore the features (session-based  correlation, configurable detection of duplicate messages, deferring messages, etc.) provided out-of-the-box by the Service Bus brokered messaging.</div>
<div>For more information on the Service Bus, you can use the following resources:</div>

*   "[Windows Azure Service Bus](http://msdn.microsoft.com/en-us/library/ee732537.aspx)" section on the MSDN site.*   "[Service Bus for Windows Server](http://msdn.microsoft.com/en-us/library/jj193022(v=azure.10).aspx)" section on the MSDN site.*   &ldquo;[Service Bus](http://msdn.microsoft.com/en-us/library/ee732537.aspx)&rdquo; topic on the MSDN site.*   &ldquo;[Now Available: The Service Bus September 2011 Release](http://blogs.msdn.com/b/windowsazure/archive/2011/09/16/the-service-bus-september-2011-release.aspx)&rdquo; article on the Windows Azure Blog.*   &ldquo;[Queues, Topics, and Subscriptions](http://msdn.microsoft.com/en-us/library/windowsazure/hh367516.aspx)&rdquo; article on the MSDN site.*   &ldquo;[Understanding Windows Azure Queues (and Topics)](http://blogs.msdn.com/b/clemensv/archive/2011/06/10/understanding-windows-azure-appfabric-queues-and-topics.aspx)&rdquo; video on the Clemens Vasters Blog.*   &ldquo;[Building loosely-coupled apps with Windows Azure Service Bus Topics and Queues](http://channel9.msdn.com/Events/BUILD/BUILD2011/SAC-862T)&rdquo; video on the channel9 site.*   &ldquo;[Service Bus Topics And Queues](http://channel9.msdn.com/posts/ServiceBusTopicsAndQueues)&rdquo; video on the channel9 site.*   &ldquo;[Securing Service Bus with ACS](http://channel9.msdn.com/posts/Securing-Service-Bus-with-ACS)&rdquo; video on the channel9 site.



# Videos**<span style="font-size: 10px;">
 </span>**

For more information on how to use the Service Bus Explorer, see the following videos on Channel9:

*   [Getting Started with Service Bus. Part 3: Service Bus Explorer](http://www.digitalpodcast.com/items/10765228) by Clemens Vasters*   [Cross Platform Notifications using Windows Azure Notifications Hub](http://channel9.msdn.com/Shows/Cloud+Cover/Episode-116-Cross-Platform-Notifications-using-Windows-Azure-Notifications-Hub) by Elio Damaggio,&nbsp;Nick Harris and Chris Risner.

# License

Microsoft Corporation (&ldquo;Microsoft&rdquo;) grants you a nonexclusive, perpetual, royalty-free right to use and modify the software code provided by us for the purposes of illustration&nbsp; ("Sample Code") and to reproduce and distribute the object  code form of the Sample Code, provided that you agree: (i) to not use our name, logo, or trademarks to market your software product in which the Sample Code is embedded; (ii) to include a valid copyright notice on your software product in which the Sample  Code is embedded; and (iii) to indemnify, hold harmless, and defend us and our suppliers from and against any claims or lawsuits, whether in an action of contract, tort or otherwise, including attorneys&rsquo; fees, that arise or result from the use or distribution  of the Sample Code or the use or other dealings in the Sample Code. Unless applicable law gives you more rights, Microsoft reserves all other rights not expressly granted herein, whether by implication, estoppel or otherwise.&nbsp;

THE SAMPLE CODE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL MICROSOFT OR ITS LICENSORS BE LIABLE  FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THE SAMPLE CODE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

# Windows Azure Management Portal

<div>In order to use the Brokered and Relay messaging functionality provided by the Service Bus, the first operation to perform is to provision a new Service Bus namespace or modify an existing namespace to include the Service Bus. You can accomplish this task  from the [Windows Azure Management Portal](https://windows.azure.com/default.aspx).</div>
<div>[](file:///C:/Users/paolos/AppData/Local/Temp/1/WindowsLiveWriter393614076/supfiles18B3FF56/CreateNamespace4.jpg)</div>
<div>Once completed this step, you can start creating and using queues, topics and subscriptions. You have many options to provision and manage messaging entities:</div>

*   This first option is to exploit the [ CreateQueue](http://msdn.microsoft.com/en-us/library/hh293157.aspx), [ CreateTopic](http://msdn.microsoft.com/en-us/library/hh330826.aspx) and [ CreateSubscription](http://msdn.microsoft.com/en-us/library/hh330855.aspx) methods exposed by the [ NamespaceManager](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.namespacemanager.aspx) class that can be used to manage entities, such as queues, topics, subscriptions, and rules, in your service namespace. This is exactly the approach that I followed to build the provisioning functionality exposed by the Service Bus Explorer  tool.*   The second approach is to rely on the [ Service Bus REST API](http://msdn.microsoft.com/en-us/library/hh367521.aspx) to create and delete messaging entities. By using REST, you can write applications in any language that supports HTTP requests, without the need for a client SDK. In particular, this allows applications based on non-Microsoft technologies  (e.g. Java, Ruby, etc.) not only to send and receive messages from the Service Bus, but also to create or delete queues, topics and subscription in a given namespace.*   Finally, you can administer messaging entities from the user interface supplied by the [Windows Azure Management Portal](https://windows.azure.com/default.aspx). In particular, the buttons in the **Manage Entities **command bar highlighted in red at point 1 in the figure below allow creating a new queue, topic and subscription entity. Then you can use the navigation tree-view shown at point 2 to select an existing entity and display its  properties in the vertical bar highlighted at point 3. To remove the selected entity, you press the **Delete** button in the **Manage Entities** command bar.
<div>[](file:///C:/Users/paolos/AppData/Local/Temp/1/WindowsLiveWriter393614076/supfiles18B3FF56/ManagementPortalProvisioning9.jpg)</div>
<div>Using [Windows Azure Management Portal](https://windows.azure.com/default.aspx) is a handy and convenient manner to handle the messaging entities in a given Service Bus namespace. However, at least at the moment, the set of operations that a  developer or a system administrator can perform using its user interface is quite limited. For example, the [Windows Azure Management Portal](https://windows.azure.com/default.aspx) actually allows a user to create queues, topics, and subscriptions and define their properties, but not to create or display rules for an existing subscription. At the moment,  you can accomplish this task only by using the [ .NET Messaging API](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.aspx). In particular, to add a new rule to an existing subscription you can use the [AddRule(String, Filter)](http://msdn.microsoft.com/en-us/library/hh429499.aspx) or the [AddRule(RuleDescription)](http://msdn.microsoft.com/en-us/library/hh330813.aspx) methods exposed by the [ SubscriptionClient](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.subscriptionclient.aspx) class, while to enumerate the rules of an existing subscription, you can use the [ GetRules](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.namespacemanager.getrules.aspx) method of the [ NamespaceManager](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.namespacemanager.aspx) class. Besides, the [ Windows Azure Management Portal](https://windows.azure.com/default.aspx) actually does not provide the ability to perform the following operations:</div>

*   Properly visualize entities in a hierarchical manner. Actually, the [ Windows Azure Management Portal](https://windows.azure.com/default.aspx) displays queues, topics and subscriptions with a flat treeview. However, you can organize messaging entities in a hierarchical structure simply by specifying their name as an absolute path composed of multiple path segments,  for example crm/prod/queues/orderqueue.*   Export the messaging entities contained in a given Service Bus namespace to an XML binding file (a-la BizTalk Server). Instead, the Service Bus Explorer tool provides the ability to select and export
    *   Individual entities.*   Entities by type (queues or topics).*   Entities contained in a certain path (e.g. crm/prod/queues).*   All the entities in a given namespace.

*   Import queues, topics and subscriptions from an XML file to an existing namespace. The Service Bus Explorer supports the ability to export entities to an XML file and reimport them on the same or another Service Bus namespace. This feature comes in a handy  to perform a backup and restore of a namespace or simply to transfer queues and topics from a testing to a production namespace.
<div>That&rsquo;s why in June, I created a tool called [ Service Bus Explorer](http://windowsazurecat.com/2011/07/exploring-topics-and-queues-by-building-a-service-bus-explorer-toolpart-1/) that allows a user to create, delete and test queues, topics, subscriptions, and rules. My tool was able to manage entities in the [Labs Beta environment](https://portal.appfabriclabs.com/Default.aspx). However, the new version of the [Service Bus API](http://msdn.microsoft.com/en-us/library/hh394905.aspx) introduced some breaking changes, as you can read [ here](http://rickgaribay.net/archive/2011/09/14/azure-appfabric-service-bus-brokered-messaging-ga-amp-rude-ctp.aspx), so I built a new version of the Service Bus Explorer tool that introduces a significant amount of new features.</div>

# Service Bus Explorer

<div>The following picture illustrates the high-level architecture of the Service Bus Explorer tool. The application has been written in C# using [Visual Studio 2010](http://www.microsoft.com/visualstudio/en-us) and requires the installation of the .[NET Framework 4.0](http://www.microsoft.com/download/en/details.aspx?id=17718) and [ ](http://www.microsoft.com/download/en/details.aspx?displaylang=en&amp;id=27421)[Windows Azure SDK for .NET](http://www.microsoft.com/download/en/details.aspx?id=28045 "Windows Azure SDK for .NET"). The tool can be copied and used on any workstation that satisfies the prerequisites mentioned above to manage and test  the Brokered and Relay messaging services defined in a given Service Bus namespace.</div>
<div style="text-align: center;">&nbsp;</div>
<div>![](/site/view/file/45426/1/ServiceBusExplorer.jpg)Read the full article on [ MSDN](http://msdn.microsoft.com/en-us/library/hh532261(v=VS.103).aspx "MSDN").</div>
<div>**NOTE**: I'll continue to develop the tool and add new functionalities. So I strongly recommend you to visit this page from time to time for a new version.</div>
<div>**Author: **Paolo Salvatori</div>
<div>**Update**: 28 August 2012</div>
<div>This version introduces the following updates:</div>

*   A new flat UI. This change has been done primarily to match Windows 8 and Windows Server 2012 new UI style.*   Two new options in the configuration file and Options form that allow to save respectively the message text and user-defined properties of a BrokeredMessage between 2 runs. This way, you don't have to re-enter user-defined properties when you start a new  session.*   The possibility to define the body of BrokeredMessage as a stream. Now in the Sender tab of a queue and topic you can select 3 different formats for the payload: string, stream and WCF message.
<div>**Update**: 18&nbsp;December 2012</div>
<div>This version introduces the following updates:</div>

*   Implemented Disable/Enable operations for queues and topics.
<div>![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/73360/1/disableentity.png)</div>
<div>&nbsp;</div>

*   Implemented Update operation for queues, topics and subscriptions.*   Added OData filter support for queues, topics and subscriptions in the ConnectForm.*   Added OData filter support on context menu of queues, topics and subscriptions.*   Added FilterForm that allows to compose a valid OData filter both as free-text or using the UI. The 2 mechanisms are synchronized.
<div>![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/72778/1/filterform.png)</div>
<div>&nbsp;</div>

*   Added support for Windows Azure Service Bus connection strings in the Connect Form.
<div>![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/72779/1/connectionstring.png)</div>
<div>&nbsp;</div>

*   Added support for Service Bus for Windows Server and Windows Azure Service Bus connection strings in the configuration file.*   Added support for F5 shortcut to refresh current entity/entities*   Added connection string textbox in the Connect form.*   Added tooltip with samples for cloud &amp; server connection strings in the Connect form.*   Added checkbox for IsAnonymousAccessible property to topic and queue management control.*   Added checkbox for EnableFilteringMessagesBeforePublishing property to topic management control.*   Added TextBox for UserMetadata property to queue, topic and subscription management control.*   Added TextBox for ForwardTo property to queue and subscription management control.*   Added visualization of IsReadOnly property to queue, topic and subscription management control.*   Added ForwardToForm to select a target queue or topic from a treeview as value for the ForwardTo property.
<div>![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/73361/1/forwardform.png)</div>
<div>&nbsp;</div>

*   Added support for SendBatch to the Sender tab of queue/topic test controls.*   Added support for ReceiveBatch to the Receiver tab of queue/topic/subscription test controls.*   Added support for Get Message Sessions to queue and subscription context menus for session-aware queues and subscriptions.
<div>![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/73362/1/getmessagesessions.png)</div>
<div>&nbsp;</div>

*   Replaced the absolute Uri of a target queue or topic with the value of the Path property (queues) and name property (subscriptions) when loading the value of the ForwardTo property.*   Added ListView for entity information (AccessedAt, UpdatedAt, Status, MessageCountDetail, etc.) to queue, topic and subscription management control.*   Added User Meyadata fields.*   Replaced TextBox with TrackBar for Maximum Size of Queues and Topics.*   Differentiated possible and maximum values for max size of messaging entities for cloud and on-premises Service Bus namespaces.*   Fixed Refresh operation for Subscriptions and Rules nodes.*   Changed the style of DataGridViews in all controls.*   Changed the style of ListViews in all controls.
 Adjusted the width of the last column of DataGridViews and ListViews based on control width and vertical scroll bar visibility.*   The main panel is cleared when connecting to a new namespace.*   Fixed minor issues in the Rule control.*   Significantly improved the performance of graph drawing.*   Significantly improved the performance of logging.*   Fixed problems with graphs when stopping load test.*   Fixed problems with graphs when using PrefetchCount &gt; 0*   Hidden the Relay Services node for Service Bus for Windows Server namespaces.*   Hidden the Is Anonymous Accessible settings for cloud Service Bus namespaces.*   The ConnectForm remembers the last connectionstring opened.*   Improved the visualization of the namespace treeview.*   Change icon of the namespace treeview.*   Changed the About form.*   Changed the Windows Azure Logo.
<div>**Update**:&nbsp;7&nbsp;January 2013</div>
<div>This version introduces the following updates:</div>

*   Fixed&nbsp;Copy &lt;Queue|Topic|Subscription&gt;&nbsp;URL and Copy Deadletter Queue URL for Service Bus for Windows Server namespaces.
<div>**Update**:&nbsp;8&nbsp;January 2013</div>
<div>This version introduces the following updates:</div>

*   Fixed the address&nbsp;format&nbsp;of the&nbsp;To header when sending WCF messages to queues and topics of a Service Bus for Windows Server namespace.
<div>**Update**: 12&nbsp;April 2013</div>
<div>This version introduces the following updates:</div>

*   This version uses the Microsoft.ServiceBus.dll 2.0 Beta available on NuGet at [http://nuget.org/packages/WindowsAzure.ServiceBus](http://nuget.org/packages/WindowsAzure.ServiceBus).*   **SAS and Authorization Rules**: So far, the Windows Azure Service Bus had a strong dependency on ACS for authentication. As you know, Shared Secret is the most commonly used authentication mechanism, but SAML is a good alternative.This generated  some problems, because when ACS is unavailable, applications cannot authenticate and acquire the token necessary to access the Service Bus namespace. That&rsquo;s why the Service Bus team decided to implement Shared Access Signatures, like in storage services:  you will be able to create authorization rules at the queue and topic level to access them directly with a Primary or Secondary key without the need to authenticate against ACS. This should also speed up the access to messaging entities because it cuts the  hand shacking with ACS. In addition, the Windows Azure management portal will soon provide the ability to create multiple signatures with different rights. These signatures will be available to be used in a Service Bus connection string. The most restrictive  scope always win when you have two SAS, one at the namespace level and one at the entity level, with the same key and different access rights.&nbsp;The Service Bus Explorer 2.0 introduces the possibility to define authorization rules at the queue/topic level  as shown in the picture below:
<div>![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/79768/1/authorizationrulestab.png)</div>

*   **Metrics**: Windows Azure Service Bus 2.0 introduces the possibility to invoke metrics RESTful services to retrieve useful key performance indicators at the entity level.*   **Supported Metrics:&nbsp; **Telemetry and usage data include the following metrics:
    *   Length*   Size*   Incoming Messages*   Outgoing Messages*   Successful operations*   Failed operations*   Internal Server Errors*   Server Busy Errors*   Other Errors

*   Metrics support the following granularities:
    *   PT5M&nbsp;5 Minutes rollup*   PT1H&nbsp;1 Hour rollup*   P1D&nbsp;1 Hour rollup*   P7D&nbsp;1 Hour rollup

*   The Service Bus Explorer 2.0 introduces the ability to query telemetry and metrics data. Right-click the namespace root node and choose **Open Metrics in SDI **or **MDI mode **as shown in the following picture:
<div>![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/79769/1/metricrootnode.png)</div>

*   The tool gives the user the ability to define one or more Metrics rules to retrieve metrics data by invokingthe RESTful services exposed by the Service Bus. Note: these are the same services invoked by the Windows Azure Management Portal to show Service  Bus counter in the Dashboard tab. The user can define multiple rules to compare metrics from different entities (e.g. the incoming messages of request queue with the outgoing messages of a response queue in a given timeframe). Metrics from different entities  can be visualized together in the Main Graph, while data and charts for individual metrics can be analyzed on separate tabs. See the following screenshots:
<div>![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/79770/1/metricsquerytab.png)</div>
<div>
 ![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/79771/1/metricsmaingraph.png)
 ![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/79772/1/singlemetricstab.png)</div>

*   The rules can be exported/imported to/from an XML file.*   **Important Note**: to&nbsp;access Metrics data is mandatory to indicate in the configuration file or in the Options form the following values:
    *   Windows Azure Subscription Id*   Thumbprint of a Windows Azure Management Certificate

*   Metrics can also be access at the entity level from the Metrics tab as shown in the following screenshots:
<div>![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/79773/1/metricsonentitytab.png)</div>
<div>![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/79774/1/metricsonentitytab2.png)</div>

*   **Monitor**: the Service Bus Explorer 2.0 introduces the possibility to monitor in real time and define warning and critical thrshold values for the following properties of for queues, topics and subscriptions:
    *   Active Message Count*   Deadletter Message Count*   Size

*   The current state of a&nbsp;performance counter can be visualized in the Monitor Rules datagrid or in the chart.*   The state is represented by a different color:
    *   Green: Normal*   Yellow: Warning*   Red: Critical

*   State transition events are logged in the Monitor Events listbox.*   The rules can be exported/imported to/from an XML file.
<div>![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/79775/1/monitortab.png)</div>
<div>&nbsp;</div>

*   **Sessions**: the Service Bus Explorer 2.0 introduces the possibility to retrieve the current sessions for a sessionful queue or subscriptions. You can access this functionality from the new **Sessions** button or from the **Get Message Sessions**&nbsp;context menu of a sessionful entity:
<div>![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/79776/1/sessions.png)</div>

*   **Peek and Receive Active Messages**: the Service Bus Explorer 2.0 introduces the possibility to peek or receive a configurable amount&nbsp;of messages from a queue or subscription. You can access this functionality from the new **Messages** button or from the following items in the context menu item of a queue or subscription:
    *   Receive All Messages*   Receive Top k Messages*   Peek Top k Messages
<div>![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/79778/1/readmessages2.png)</div>

*   When you click the **Ok** button in the **Retrieve messages from queue **(or subscription) dialog, messages are retrieved and showned in the following tab.
<div>![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/79779/1/messages2.png)</div>

*   You can browse messages by selecting the corresponding row in the grid. The ** Messages** tab shows the following information for the selected message:
    *   Message Text*   Message System Properties*   Message Custom Properties

*   **Peek and Receive Deadletter Messages**: the Service Bus Explorer 2.0 introduces the possibility to peek or receive a configurable amount&nbsp;of messages from the deadletter queue of queues and subscriptions. You can access this functionality  from the new **Deadletter **button or from the following items in the context menu item of a queue or subscription:

*   &nbsp;
    *   Receive All Deadletter Queue Messages*   Receive Top k Deadletter Queue Messages*   Peek Top k Deadletter Queue Messages
<div>![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/79780/1/deadletter2.png)</div>

*   When you click the **Ok** button in the **Retrieve messages from deadletter&nbsp;queue&nbsp;**dialog, messages are retrieved and showned in the following tab.
<div>![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/79781/1/deadletter3.png)</div>
<div>&nbsp;</div>

*   You can browse messages by selecting the corresponding row in the grid. The ** Deadletter** tab shows the following information for the selected message:

*   &nbsp;
    *   Message Text*   Message System Properties*   Message Custom Properties

*   **Repair and Resubmit Message**: the Service Bus Explorer 2.0 introduces the possibility to repair and resubmit a message read or peeked from a queue, subscription or deadletter queue. To perform this operation, just double click a message  in the **Messages **or **Deadletter **tab. This action opens a modal dialog from which you can edit the text, system properties and custom properties of the current message and select a queue or topic in the current namespace to which  send the modified message.
<div>![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/79782/1/repairandresubmit.png)</div>

*   **Sender Think Time**: the test queue and test topic controls introduce the ability to define a think time in the **Sender** tab.
<div>![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/79783/1/senderthinktime.png)</div>
<div>&nbsp;</div>

*   **Receive Think Time**: the test queue and test topic controls introduce the ability to define a think time in the **Receiver **tab.
<div>![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/79786/1/receiverthinktime.png)</div>

*   **Use multiple&nbsp;files as message template**: the Service Bus Explorer 2.0 introduces the possibility to select multiple files from different directories to be used as message templates when testing queues, topics and subscriptions.&nbsp;As  shown in the picture below, in the&nbsp;**Message** tab select the Files sub-tab and use the **Select Files** button to add files to the list of message templates. Select the files that you want to use as message templates during a test. Sender tasks will use the content of these&nbsp;files in a round-robin fashion when sending messages  to queues and topics. All messages will share the custom and system properties defined on the **Message** and **Sender** tabs.
<div>![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/79787/1/messagefiles.png)</div>

*   **New configuration settings and Options form**: the following settings have been introduced in the Options form and in the configuration file. In addition, the **Options** form now provides the ability to persist settings to the configuration file by clicking the **Save** button.
    *   Monitor Refresh Interval*   Subscription Id*   Management Certificate Thumbprint*   Message Path*   Message Text*   Sender Think Time*   Receive Think Time
<div>![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/79788/1/optionsform.png)</div>

*   **New About form**: you can access my email address and twitter account in the new about form.
<div>![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/79789/1/aboutform.png)</div>

*   **Auto Delete On Idle**: the Service Bus 2.0 introduced for queues, topics and subscriptions the new TimeSpan property **AutoDeleteOnIdle**&nbsp;that defines the TimeSpan idle interval after which the topic is automatically deleted. The minimum duration is 5 minutes. The Service Bus Explorer 2.0 has been extended to support this property.
<div>![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/79791/1/autodelete2.png)</div>

*   **Minor changes**
    *   Added check to make sure that non-numeric values can be entered in textboxes that expect a number*   Added links to blog, email and twitter in the About form*   Changed the CreateFileName method of the MainForm class that is invoked when exporting entities*   Changed Session Timeout with Server Timeout in test queue/topic/subscription controls*   Deleted Expand and Collapse node from queue node context menu.*   Fixed a problem with in the ForwardTo form.*   The message text in the test queue, topic and relay service controls is now indented when is Xml.*   Added a border to flat comboboxes.*   Changed look &amp; feel of forms*   Dispose dialogs and forms after use.*   Changed Docking properties of chart Legends: moved from right to top.*   Fixed problem with MaxSizeInMegabytes and ForwardTo properties when importing/exporting entities.

*   &nbsp;**Service Bus for Windows Server **support: the Service Bus 2.0 temporarily interrupts the symmetry between the cloud and on-premises version of the Service Bus. In other words, the Microsoft.ServiceBus.dll 2.0 client is not compatible  with the Service Bus for Windows Server 1.0. For this reason, I included the old version of the Service Bus Explorer in a zip file called 1.8 which in turn is contained in the zip file of the current version. The old version of the Service Bus Explorer uses  the Microsoft.ServiceBus.dll 1.8 which is compatible with the Service Bus for Windows Server.
<div>**Update**: 19 April 2013</div>
<div>This version introduces the following updates:</div>

*   Fixed display problems when Control Panel -&gt; Display -&gt; Change the size of all items is set to 125%.
<div>&nbsp;</div>
<div>**Update**: 2 May 2013</div>
<div>This version introduces the following updates:</div>

*   RTM version of the Microsoft.ServiceBus.dll
<div>**Update**:&nbsp;10 May 2013</div>
<div>This version introduces the following updates:</div>

*   Fixed a regression bug affecting the bindingTreeView control in the TestRelayServiceControl.

**Update**:&nbsp;30 September 2013

This version introduces the following updates:

*   The **Connect Form**&nbsp;provides the possibility to choose the [ ConnectivityMode](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.connectivitysettings.mode.aspx) and [ TransportType](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.messagingfactorysettings.transporttype.aspx):

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/97215/1/connectivitymodetransporttype.png)

*   See the following resources for more information on these properties:
    *   [How to: Modify the Service Bus Connectivity Setting](http://msdn.microsoft.com/en-us/library/windowsazure/ee706755.aspx)*   [ServiceBusEnvironment.SystemConnectivity Property](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.servicebusenvironment.systemconnectivity.aspx)*   [ConnectivitySettings.Mode Property](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.connectivitysettings.mode.aspx)*   [ConnectivityMode Enumeration](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.connectivitymode.aspx)*   [MessagingFactorySettings.TransportType Property](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.messagingfactorysettings.transporttype.aspx)*   [TransportType Enumeration](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.transporttype.aspx)

*   **Important Note**: when using [ TransportType](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.messagingfactorysettings.transporttype.aspx) = Amqp, some features are not supported. So expect to see error messages such as "the method or operation is not supported": e.g. transactions and Batch-based APIs are not actually supported. For more information, please see the following  topic:
    *   [Using Service Bus from .NET with AMQP 1.0](http://msdn.microsoft.com/en-us/library/windowsazure/jj841075.aspx)

*   The **Options Form **allows to select the default [ ConnectivityMode](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.connectivitysettings.mode.aspx). This information is saved in the **appSettings ** section of the configuration file.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/97216/1/connectivitymodeoptionsform.png)

*   Added [ SelectionMode ](http://msdn.microsoft.com/en-us/library/system.windows.forms.listbox.selectionmode.aspx)= [ MultiExtended](http://msdn.microsoft.com/en-us/library/system.windows.forms.selectionmode.aspx) to the Log listbox. This feature allows the user to&nbsp;select multiple lines and use the SHIFT, CTRL, and arrow keys to make selections.*   Implemented copy &amp; paste of messages from the **Log **listbox (Ctrl + C).*   Added a context menu to the **Log **listbox.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/97217/1/log.png)

*   The context menu allows the user to select the perform actions: &nbsp;
    *   **Copy All**: copy all of the lines in the clipboard.*   **Copy Selected**: copy only the selected lines in the clipboard.*   **Clear All**: clear all of the lines in the log.*   C**lear Selected**:&nbsp;clear only the selected lines in the log.*   **Save all**: save all of the lines in a text file.*   **Save Selected**: save only the selected lines in a text file.

*   The **Service Bus Explorer 2.1** supports&nbsp;for **Service Bus for Windows Server 1.1**. In particular, this version introduces the possibility to visualize the information comtained in the [ MessageCountDetails ](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.messagecountdetails.aspx)property of a [ QueueDescription](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.queuedescription_properties.aspx), [ TopicDescription](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.topicdescription.aspx), [ SubscriptionDescription](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.subscriptiondescription.aspx) object.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/97218/1/messagecountdetails.png)

*   This functionality allows to clone and send the selected messages to a the same or alternative queue or topic in the **Service Bus **namespace. If you want to edit the payload, system properties or user-defined properties, you have to select, edit and send messages one at a time. In order to do so, double click a message in the DataGridView or right click the  message and click **Repair and Resubmit Selected Message** from the context menu. This opens up the following dialog that allows to modify and resubmit the message or to save the payload to a text file.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/97221/1/repairsinglemessage.png)

**Important Note**: the Service Bus does not allow to receive and delete a peeked [ BrokeredMessage](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.brokeredmessage.aspx) by [ SequenceNumber](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.brokeredmessage.sequencenumber.aspx). Only deferred messages can by received by [ SequenceNumber](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.brokeredmessage.sequencenumber.aspx).&nbsp;As a consequence, when editing and resubmitting a peeked message, there's no way to receive and delete the original copy.

*   The **Service Bus Explorer 2.1** introduces support for [ Notification Hubs](http://www.windowsazure.com/en-us/documentation/services/notification-hubs/). See the following resources for more information on this topic:
    *   [Notification Hubs](http://www.windowsazure.com/en-us/documentation/services/notification-hubs/)*   [What are Notification Hubs?](http://www.windowsazure.com/en-us/develop/net/how-to-guides/service-bus-notification-hubs/)*   [Getting Started with Notification Hubs](http://www.windowsazure.com/en-us/manage/services/notification-hubs/getting-started-windows-dotnet/)

*   The **Notification Hubs** node under the namespace root node allows to manage notification hubs in defined in **Windows Azue Service Bus **namespace.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/97222/1/notificationhubs.png)

*   The context menu allows to perform the following actions:
    *   **Create Notification Hub**: create a new notification hub*   **Delete Notification Hubs**: delete all the notification hubs defined in the current namespace.*   **Refresh Notification Hubs**: refreshed the list of notification hubs.*   **Export Notification Hubs**: exports the definition of all the notification hubs to a XML file.*   **Expand Subtree**: expands the tree under Notification Hubs node.

    *   **Collapse Subtree**: collapse the tree under Notification Hubs node.

*   The&nbsp;**Create Notification Hub **allows to define the path, credentials, and metadata for a new notification hub:

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/97223/1/newhub.png)

*   If you click an existing notification hub, you can view and edit credentials and metadata:

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/97224/1/notificationhub01.png)

*   The **Authorization Rules** tab allows to review or edit the** Shared Access Policies** for the selected notification hub.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/97225/1/authorizationrules.png)

*   The **Registrations **buttons opens a a dialof that allows the registrations to query:

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/97226/1/getregistrations.png)

*   You can select one of the following options:
    *   **PNS Handle**: this option allows to retrieve registrations by ** ChannelUri **(Windows Phone 8 and Windows Store Apps registrations), ** DevieToken **(Apple registrations), **GcmRegistrationId **(Google registrations)*   **Tag**: this options allows to find all the registrations sharing the specified tag.*   **All**: this options allows to receive **n** registrations where **n** is specified by the **Top Count** parameter. This value specifies also the page size. In fact, the tool supports registration data paging and allows to retrieve more data using the continuation mechanism.

*   The **Registrations **tab allows to select one or multiple registrations from the **DataGridView**.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/97227/1/registrations.png)

*   The navigation control in the bottom of the registrations control allows to navigate through pages.*   The **DataGridView&nbsp;**context menu provides access to the following actions:
    *   **Update Selected Registrations**: update the selected registrations.*   **Delete Selected Registrations**: delete the selected registrations.

*   The **PropertyGrid **on the right-hand side allows to edit the properties (e.g. **Tags** or **BodyTemplate**) of an existing registration.*   The **Create **button allows to create a new registration. Select the registration type from the dropdownlist and enter mandatory and optional (e.g. **Tags**, **Headers**) information and click the ** Ok** button to confirm.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/97228/1/createnewregistration.png)

*   The **Template **tab allows to send template notifications:
    *   The **Notification Payload** read-only texbox shows the payload in JSON format.*   The **Notification Properties** datagridview allows to define the template properties.&nbsp;*   The&nbsp;**Notification Tags&nbsp;**datagridview allows to define one or multiple tags. A separate notification is sent for each tag.*   The **Additional Headers **datagridview allows to define additional custom headers for the notification.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/97230/1/template.png)

*   The&nbsp;**Windows Phone&nbsp;**tab allows to send native notifications to **Windows Phone 8** devices.
    *   The&nbsp;**Notification Payload**&nbsp;texbox shows the payload in JSON format. When the **Manual **option is selected in the dropdownmenu under **Notification Template**, you can edit or paste the payload direcly in the control. When any of the other options (**Tile**, **Toast**, **Raw**) is selected, this field is read-only.*   The&nbsp;**Notification Template **dropdownlist allows to select between the following types of notification:
        *   Manual*   Toast*   Tile*   Raw

        *   When **Toast**,&nbsp;**Tile**, or&nbsp;**Raw**&nbsp;is selected, the datagridview under the **Notification Template **section&nbsp;allows to define the properties for the notification, as shown in the figures below.*   The&nbsp;**Notification Tags&nbsp;**datagridview allows to define one or multiple tags. A separate notification is sent for each tag.*   The&nbsp;**Additional Headers&nbsp;**datagridview allows to define additional custom headers for the notification.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/97231/1/windowsphone.png)

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/97232/1/windowsphonetile.png)

*   The&nbsp;**Windows **tab allows to send native notifications to&nbsp;**Windows Store Apps**&nbsp;running on** Windows 8** and **Windows 8.1**.
    *   The&nbsp;**Notification Payload**&nbsp;texbox shows the payload in JSON format. When the **Manual **option is selected in the dropdownmenu under&nbsp;**Notification Template**, you can edit or paste the payload direcly in the control. When any of the other options (**Tile**,&nbsp;**Toast**,&nbsp;**Raw**)  is selected, this field is read-only.*   The&nbsp;**Notification Template&nbsp;**dropdownlist allows to select between the following types of notification:
        *   Manual*   Toast templates. For more information, see [ The toast template catalog (Windows store apps)](http://msdn.microsoft.com/en-us/library/windows/apps/hh761494.aspx).*   Tile templates. For more information, see [ The tile template catalog (Windows store apps)](http://msdn.microsoft.com/en-us/library/windows/apps/hh761491.aspx).&nbsp;

        *   When&nbsp;**Toast**,&nbsp;**Tile**, or&nbsp;**Raw**&nbsp;is selected, the datagridview under the&nbsp;**Notification Template**section&nbsp;allows to define the properties for the notification, as shown  in the figures below.*   The&nbsp;**Notification Tags&nbsp;**datagridview allows to define one or multiple tags. A separate notification is sent for each tag.*   The&nbsp;**Additional Headers&nbsp;**datagridview allows to define additional custom headers for the notification.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/97238/1/windows2.png)

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/97233/1/windows.png)

*   The&nbsp;**Apple&nbsp;**and&nbsp;**Google&nbsp;**tabs provides the ability to send, respectively,&nbsp;**Apple&nbsp;**and&nbsp;**Gcm&nbsp;**native notifications. For brevity, I omit the description of  the **Apple **tab as it works the same way as the **Google** one.
    *   &nbsp; The&nbsp;**Json Payload**&nbsp;texbox allows to enter the payload in JSON format.*   The&nbsp;**Notification Tags&nbsp;**datagridview allows to define one or multiple tags. A separate notification is sent for each tag.*   The&nbsp;**Additional Headers&nbsp;**datagridview allows to define additional custom headers for the notification.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/97239/1/gcm.png)

*   Added the possibility to select and resubmit multiple messages in a batch mode from the **Messages** and **Deadletter** tabs of queues and subscriptions. It's sufficient to select messages in the **DataGridView** as shown in the following picture, right click to show the context menu and choose **Resubmit Selected Messages in Batch Mode**.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/97219/1/resubmitmultiplemessages.png)

*   **Minor changes**
    *   Fixed code of the **Click **event handler for the&nbsp;**Default **button in the **Options Form**.**
 **
    *   Replaced the [ DataContractJsonSerializer](http://msdn.microsoft.com/en-us/library/system.runtime.serialization.json.datacontractjsonserializer.aspx) with the [ JavaScriptSerializer](http://msdn.microsoft.com/en-us/library/system.web.script.serialization.javascriptserializer.aspx) class in the **JsonSerializerHelper** class.*   Fixed a problem when reading **Metrics**&nbsp;data from the&nbsp;RESTul services exposed by&nbsp;a **Windows Azure Service Bus** namespace.*   Changes the look and feel of messages in the **Messages **and ** Deadletter **tabs of queues and subscriptions.*   Introduced indent formatting when showing and editing **XML **messages.

**Update**: 9 October 2013

This version introduces the following updates:

*   Fixed a bug when accessing a notification hub with no registrations.*   Fixed a bug that prevented the tool to connect to a Windows Azure Service Bus namespace when using a SAS connection string.

**Update**: 14 October 2013

This version introduces the following updates:

*   Fixed a problem affecting Import functionality. When importing a subscription with no default rule ($Default), the code erroneously created the subscription with a default rule. The problem is now fixed.

**Update**: 29 November 2013

This version introduces the following updates for both the 2.1 and 2.2 version:

*   Added support to read the body of a WCF message when the payload is in JSON format.*   Added support to send the body of a WCF message when the payload is in JSON format.*   Implemented the possibility to pass command line arguments for both the 2.1 and 2.2 version:

ServiceBusExplorer.exe&nbsp; [-c|/c] [connectionstring]
 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; [-q|/q] [queue odata filter expression]
 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; [-t|/t] [topic odata filter expression]
 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; [-s|/s] [subscription odata filter expression]

&nbsp;

ServiceBusExplorer.exe&nbsp; [-n|/n] [namespace key in the configuration file]
 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; [-q|/q] [queue odata filter expression]
 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; [-t|/t] [topic odata filter expression]
 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; [-s|/s] [subscription odata filter expression]

**Example**: ServiceBusExplorer.exe -n paolosalvatori -q "Startswith(Path, 'request') Eq true" -t "Startswith(Path, 'request') Eq true"

*   Improved check when settings properties for Topics and Subscriptions.*   Fixed an error that added columns to message and deadletter datagridview every time the Update button was pressed.Fixed a error on CellDoubleClick for messages and deadletter datagridview that happened when double clicking a header cell.Improved the visualization  of sessions and added the possibility to sort sessions by column.*   Added sorting capability to messages and deadletter messages datagridview for queues and subscriptions.&nbsp;Click the column header to sort rows by the corresponfing property value in ASC or DESC order.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/103826/1/messages.png)

*   Added sorting capability to sessions datagridview for queues and subscriptions.&nbsp;Click the column header to sort rows by the corresponfing property value in ASC or DESC order.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/103828/1/sessions.png)

*   Added sorting capability to registrations datagridview for notification hubs.&nbsp;Click the column header to sort rows by the corresponfing property value in ASC or DESC order.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/103830/1/registrations3.png)

*   Introduced the possibility to define filter expression for peeked/received messages/deadletter messages. Click the button highlighted in the picture below to open a dialog and define a filtter expression using a SQL Expression (e.g. sys.Size &gt; 300 and  sys.Label='Service Bus Explorer' and City='Pisa'). For more information, see&nbsp;[SqlFilter.SqlExpression Property](http://msdn.microsoft.com/en-us/library/windowsazure/microsoft.servicebus.messaging.sqlfilter.sqlexpression.aspx).

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/103827/1/messages2.png)

*   Introduced the possibility to define filter expression for peeked/received messages/deadletter messages. Click the button highlighted in the picture below to open a dialog and define a filtter expression using a SQL Expression on public and n on public  properties of RegistrationDescription class (e.g.&nbsp;PlatformType contains 'windows' and ExpirationTime &gt; '2014-2-5' and TagsString contains 'productservice'). The filter engine supports the following predicates:
    *   =*   !=*   &gt;*   &gt;=*   &lt;*   &lt;=*   StartsWith*   EndsWith*   Contains

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/103831/1/registrations4.png)

*   Introduced support for TagExpressions introduced by Service Bus 2.2. When sending a notification, you can select the Tag Expression or Notification Tags to define, respectively, a tag expression (e.g. productservice &amp;&amp; (Italy || UK)) or a list of  tags. This feature is available only in the Service Bus Explorer 2.2.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/103832/1/tagexpression.png)

*   Introduced support for partitioned queues. For more information on partitioned entities, read[Partitioned Service Bus Queues and Topics](http://blogs.msdn.com/b/windowsazure/archive/2013/10/29/partitioned-service-bus-queues-and-topics.aspx).&nbsp;This  feature is available only in the Service Bus Explorer 2.2.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/103833/1/partitionedqueue.png)

*   Introduced support for partitioned topics. For more information on partitioned entities, read [ Partitioned Service Bus Queues and Topics](http://blogs.msdn.com/b/windowsazure/archive/2013/10/29/partitioned-service-bus-queues-and-topics.aspx).&nbsp;This feature is available only in the Service Bus Explorer 2.2.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/103834/1/partitionedtopic.png)

**Update**: 2 December 2013

This version introduces the following updates:

*   Support for the&nbsp;**Microsoft.ServiceBus.ConnectionString** appSetting. The connection string defined in this setting will be added to the list of connection strings in the corresponding drop-down-list in the **Connect **form.&nbsp;

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/103937/1/microsoft.servicebus.connectionstring.png)

**Update**: 18 December 2013

This version introduces the following updates:

*   Introduced a new command to start one or more listeners for a given queue or subscription. Right click the queue or subscription and select, respectively, **Create Queue Listerner** or **Create Subscription Listener** from the context menu as shown in the following picture.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/104717/1/listener04.png)

*   In the listener dialog you can set the following properties:
        1.  [MaxConcurrentCalls](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.onmessageoptions.maxconcurrentcalls.aspx): gets or sets the maximum number of concurrent calls to the callback the message pump should initiate.2.  Refresh Interval (sec): sets a refresh interval in seconds for queue or subscription message count information.3.  Graph: enable or disable the graph.4.  [AutoComplete](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.onmessageoptions.autocomplete.aspx): gets or sets a value that indicates whether the message-pump should call&nbsp;[Complete(Guid)](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.queueclient.complete.aspx)&nbsp;or&nbsp;[Complete(Guid)](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.subscriptionclient.complete.aspx)&nbsp;on  messages after the callback has completed processing5.  Tracking: enable or disable message tracking. When enabled, you can use the Messages tab to access messages.6.  Logging: enable or disable logging.7.  Verbose: enable or disable verbose mode when logging is enabled.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/104719/1/listener05.png)

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/104718/1/listener01.png)

*   Press the **Start **button to start the listener.&nbsp;

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/104720/1/listener01.png)

*   The **Start **button turns into the **Stop **button.&nbsp;*   Press the **Stop **button to stop the listener.&nbsp;*   Press the **Close **the listener dialog. If the listener is open, this operation closes the listener.*   Press the **Clear** button to clear tracked messages and log content.*   Press the **Messages** tab to access messages.&nbsp;*   You can click s message row to access its payload, custom and system properties.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/104722/1/listener02.png)

*   Click the the magnifying glass button&nbsp;to define a filter expression for received messages using a SQL Expression (e.g. sys.Size &gt; 300 and sys.Label='Service Bus Explorer' and City='Pisa'). For more information, see&nbsp;[SqlFilter.SqlExpression  Property](http://msdn.microsoft.com/en-us/library/windowsazure/microsoft.servicebus.messaging.sqlfilter.sqlexpression.aspx).

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/104723/1/listener06.png)

*   Double click a row or click **Repair and Resubmit Message** from the context menu to open a message in the a separate dialog. This functionality allows to clone and send the selected messages to a the same or alternative  queue or topic in the&nbsp;**Service Bus&nbsp;**namespace. If you want to edit the payload, system properties or user-defined properties, you have to select, edit and send messages one at a time.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/105723/1/listener03.png)

*   Minor changes:
    *   Fixed ListView control visualization when the vertical bar is visible*   Increased ReaderQuotas when reading WCF messages. This adds support for large messages.

**Update**: 10 February 2014

This version introduces the following updates:

*   Johann Cooper (@JohannCooper) extended the Import/Export functionality to support SAS rules for Windows Server Service Bus. I included the code in the 2.1 and 2.2 versions of the tool.

**Update**: 21 May 2014

This version introduces the following updates:

*   Improved support for **AMQP **transport protocol in the ** Connect Form**. For example, when using the Service Bus Explorer to connect to an on-premises namespace, if you select AMQP as Transport Type in the Connect Form, the tool automatically changes the value of the&nbsp;TransportType and RuntimePort parameters:
    *   **NetMessaging**:&nbsp;RuntimePort=9354;TransportType=NetMessaging*   **AMQP**:&nbsp;RuntimePort=5671;TransportType=Amqp

*   Added support for the **stsEndpoint **parameter in the connection string for cloud namespaces. This feature was specifically requested by the Service Bus team.*   The tool can now use the&nbsp;**AMQP&nbsp;**transport protocol&nbsp;to read messages from queues, subscriptions and deadletter queues.*   Added full support to namespace-level **SAS connection strings**, both in the configuration file and **Connect Form**.*   Changed Receive label into Receive and Delete in the ReceiveModeForm to make it clear that the receive operation deletes messages from the underlying **queue**, **subscription** or **deadletter queue**.*   Fixed a bug when reading messages from the deadletter queue of a queue or subscription.*   Fixed a bug in the **SelectEntityForm**: replaced **TreeView.TopNode** with **TreeView.Nodes[0]***   Implemented support for the new&nbsp;[ForwardDeadLetteredMessagesTo](http://msdn.microsoft.com/en-us/library/azure/microsoft.servicebus.messaging.queuedescription.forwarddeadletteredmessagesto.aspx) property of the [ QueueDescription](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.queuedescription.aspx) and&nbsp;[SubscriptionDescription](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.subscriptiondescription.aspx)&nbsp;classes.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/114913/1/forwarddeadlettermessages.png)

*   Implemented batching support in the listener for queues and subscriptions. You can now specify a value for the [ PrefetchCount](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.messagereceiver.prefetchcount.aspx) property used by the [ MessageReceiver](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.messagereceiver.aspx) object used by the listener to prefetch multiple messages from a queue or subscription. This greatly improves the overall performance of the listener. Now you can also specify the value for the [ Mode](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.messagereceiver.mode.aspx)&nbsp;property of the the&nbsp;[MessageReceiver](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.messagereceiver.aspx)&nbsp;object used by the listener.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/114914/1/listener.png)

*   Added the possibility to create a listener for **session-aware queues** and **subscriptions **([RequiresSession](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.queuedescription.requiressession.aspx) = true). The tool uses the&nbsp;[RegisterSessionHandlerFactoryAsync](http://msdn.microsoft.com/en-us/library/dn642578.aspx)&nbsp;method  of a [ QueueClient](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.queueclient.aspx) or [ SubscriptionClient](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.subscriptionclient.aspx) object to register an asynchronous Session Handler Factory. For more information, see&nbsp;[What's New in the Azure SDK 2.3 Release (April 2014)](http://msdn.microsoft.com/en-us/library/dn632585.aspx).

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/114915/1/listenerrequiressession.png)

*   Added the possibility to visualize the number of active and deadletter messages directly in the main treeview (see 1 and 2 in the figure below). The first number displays the valut of the&nbsp;[MessageCountDetails.ActiveMessageCount](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.messagecountdetails.activemessagecount.aspx)&nbsp;property,  while the second number displays the value of the [ MessageCountDetails.DeadletterMessageCount](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.messagecountdetails.deadlettermessagecount.aspx) property.&nbsp;

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/114916/1/messagecount.png)

To enable or disable this feature, you can use the&nbsp;**showMessageCount** setting in the configuration file, or use the new **Show Message Count **checkbox in the **Options Form ** as shown in the picture below.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/114917/1/optionsform.png)

*   As shown in the figure below, numbers in the property list of queues, topics, subscriptions, and notification hubs are now formatted to make it easier to read their value.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/114918/1/formatting.png)

*   Replaced the logo to reflect the recent change of name from **Windows Azure** to **Microsoft Azure**.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/114984/1/logo.png)

**Update**: 20 June 2014

This version introduces the following updates:

*   Added license.txt file containing the license for the Service Bus Explorer tool*   Updated the Microsoft.ServiceBus.dll from version 2.3.0.0 to 2.3.2.0

&nbsp;

**Update**: 18 July 2014

This version introduces the following updates:

*   Connect Form now allows to select the type of entities that you want to load and manage in the selected namespace:

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/120275/1/connectform01.png)

&nbsp;![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/120276/1/connectform02.png)

*   You can specify a default value of the entities that you want to manage in the **selectedEntities** appSettings in the configuration file:
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;appSettings&gt;
    ...
   &lt;add key="selectedEntities" value="Queues,Topics,Relay Services,Event Hubs,Notification Hubs" /&gt;
    ...
 &lt;/appSettings&gt;</pre>
<div class="preview">
<pre class="xml"><span class="xml__tag_start">&lt;appSettings</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;...&nbsp;
&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;add</span>&nbsp;<span class="xml__attr_name">key</span>=<span class="xml__attr_value">"selectedEntities"</span>&nbsp;<span class="xml__attr_name">value</span>=<span class="xml__attr_value">"Queues,Topics,Relay&nbsp;Services,Event&nbsp;Hubs,Notification&nbsp;Hubs"</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;...&nbsp;
&nbsp;<span class="xml__tag_end">&lt;/appSettings&gt;</span></pre>
</div>
</div>
</div>

*   You can also set default selected entities in the Options dialog:

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/120278/1/optionsform01.png)

*   **Event Hubs**: this version of the tool introduces the support for a new entity called **Event Hub**&nbsp;and related entities, **Consumer Groups** and **Partitions**.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/120279/1/eventhubs01.png)

*   The tool allows to perform the following actions on an **Event Hub**:
    *   Create Event Hub*   Read Event Hub*   Update Event Hub*   Delete Event Hub*   Disable / Enable Event Hub*   Refresh Event Hub*   Copy Event Hub Url to Clipboard*   Export / Import event hub to / from XML file*   Send &nbsp;Event Data to an Event Hubs (more on this below)

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/120280/1/eventhubs02.png)

*   The tool allows to manage the **Consumer Groups** of a given ** Event Hub**:*   Create Consumer Group
    *   Read Consumer Group*   Update Consumer Group*   Delete Consumer Group*   Enable / Disable Consumer Group*   Refresh Consumer Group*   Copy Consumer Group Url to Clipboard*   Create a Listener for the Consumer Group. A receiver is created for each Partition of the Event Hub.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/120281/1/consumergroups01.png)

*   The tool allows to manage the **Partitions **of a given ** Event Hub**. Note: **Partitions **are located under **Consumer Groups** in the hierarchy of the entity TreeView:
    *   Read Partition*   Refresh Partition*   Copy Partition Url to Clipboard*   Create a Listener for the Partition. A receiver is created only for the selected Partition under the Consumer Group.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/120282/1/partitions01.png)

*   Added support for **Notification Hubs** and **Event Hubs** to entity **Import &nbsp;&amp; Export** functionalities. You can now import / export the definition of Notification Hubs and Event Hubs from / to XML file.*   The JSON serializer helper class is now based on **Newtonsoft Json.Net** library.*   <span style="font-size: 10px;">Added support for the new </span>**BrokeredMessage.ForcePersistence**<span style="font-size: 10px;"> property to the testing UI of queues and &nbsp;topics.</span>

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/120283/1/forcepersistence01.png)

*   **File Templates**: This version introduces the possibility to send binary files or messages based on JSON or XML Templates. This feature is available for queues, topics and event hubs.*   For example, when you select **Send Messages **menu item from the context menu of a queue and you select the **Files** tab, now you have 4 options available:

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/120284/1/filetemplates01.png)

**Options**:

*   **Text File**: this option allows to select one or more text files. These files specify the payload of the outgoing messages (BrokeredMessage for queues and topics, EventData for event hubs), while the custom properties are defined in the grid  under the Message Properties control group.*   **Binary File**: this option allows to select one or more binary files. These files specify the payload of the outgoing messages (BrokeredMessage for queues and topics, EventData for event hubs), while the custom properties are defined in the  grid under the Message Properties control group.*   **Json Template**: this option allows to select one or more Json templates. Each template defines the payload for a BrokeredMessage or EventData and the value of the system and the custom properties:

**BrokeredMessage Json Template**:

<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">{
  "messageId": "1",
  "sessionId": "",
  "correlationId": "",
  "contentType": "",
  "label": "Service Bus Explorer",
  "partitionKey": "",
  "to": "",
  "replyTo": "",
  "replyToSessionId": "",
  "timeToLive": "",
  "scheduledEnqueueTimeUtc": "",
  "forcePersistence": "false",
  "message": "{\"deviceId\":\"1\", \"value\":\"25\"}",
  "properties": [
    {
      "key": "deviceId",
      "type": "Int64",
      "value": "1"
    },
    {
      "key": "location",
      "type": "String",
      "value": "Room 1"
    },
    {
      "key": "city",
      "type": "String",
      "value": "Milan"
    },
    {
      "key": "country",
      "type": "String",
      "value": "Italy"
    },
    {
      "key": "value",
      "type": "Int32",
      "value": "25"
    }
  ]
}</pre>
<div class="preview">
<pre class="js"><span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;<span class="js__string">"messageId"</span>:&nbsp;<span class="js__string">"1"</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"sessionId"</span>:&nbsp;<span class="js__string">""</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"correlationId"</span>:&nbsp;<span class="js__string">""</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"contentType"</span>:&nbsp;<span class="js__string">""</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"label"</span>:&nbsp;<span class="js__string">"Service&nbsp;Bus&nbsp;Explorer"</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"partitionKey"</span>:&nbsp;<span class="js__string">""</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"to"</span>:&nbsp;<span class="js__string">""</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"replyTo"</span>:&nbsp;<span class="js__string">""</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"replyToSessionId"</span>:&nbsp;<span class="js__string">""</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"timeToLive"</span>:&nbsp;<span class="js__string">""</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"scheduledEnqueueTimeUtc"</span>:&nbsp;<span class="js__string">""</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"forcePersistence"</span>:&nbsp;<span class="js__string">"false"</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"message"</span>:&nbsp;<span class="js__string">"{\"deviceId\":\"1\",&nbsp;\"value\":\"25\"}"</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"properties"</span>:&nbsp;[&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"key"</span>:&nbsp;<span class="js__string">"deviceId"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"type"</span>:&nbsp;<span class="js__string">"Int64"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"value"</span>:&nbsp;<span class="js__string">"1"</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"key"</span>:&nbsp;<span class="js__string">"location"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"type"</span>:&nbsp;<span class="js__string">"String"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"value"</span>:&nbsp;<span class="js__string">"Room&nbsp;1"</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"key"</span>:&nbsp;<span class="js__string">"city"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"type"</span>:&nbsp;<span class="js__string">"String"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"value"</span>:&nbsp;<span class="js__string">"Milan"</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"key"</span>:&nbsp;<span class="js__string">"country"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"type"</span>:&nbsp;<span class="js__string">"String"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"value"</span>:&nbsp;<span class="js__string">"Italy"</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"key"</span>:&nbsp;<span class="js__string">"value"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"type"</span>:&nbsp;<span class="js__string">"Int32"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"value"</span>:&nbsp;<span class="js__string">"25"</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;]&nbsp;
<span class="js__brace">}</span></pre>
</div>
</div>
</div>

**EventData Json Template**:

<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">{
  "partitionKey": "1",
  "message": "{\"deviceId\":\"1\", \"value\":\"25\"}",
  "properties": [
    {
      "key": "deviceId",
      "type": "Int64",
      "value": "1"
    },
    {
      "key": "location",
      "type": "String",
      "value": "Room 1"
    },
    {
      "key": "city",
      "type": "String",
      "value": "Milan"
    },
    {
      "key": "country",
      "type": "String",
      "value": "Italy"
    },
    {
      "key": "value",
      "type": "Int32",
      "value": "25"
    }
  ]
}</pre>
<div class="preview">
<pre class="js"><span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;<span class="js__string">"partitionKey"</span>:&nbsp;<span class="js__string">"1"</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"message"</span>:&nbsp;<span class="js__string">"{\"deviceId\":\"1\",&nbsp;\"value\":\"25\"}"</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"properties"</span>:&nbsp;[&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"key"</span>:&nbsp;<span class="js__string">"deviceId"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"type"</span>:&nbsp;<span class="js__string">"Int64"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"value"</span>:&nbsp;<span class="js__string">"1"</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"key"</span>:&nbsp;<span class="js__string">"location"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"type"</span>:&nbsp;<span class="js__string">"String"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"value"</span>:&nbsp;<span class="js__string">"Room&nbsp;1"</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"key"</span>:&nbsp;<span class="js__string">"city"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"type"</span>:&nbsp;<span class="js__string">"String"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"value"</span>:&nbsp;<span class="js__string">"Milan"</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"key"</span>:&nbsp;<span class="js__string">"country"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"type"</span>:&nbsp;<span class="js__string">"String"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"value"</span>:&nbsp;<span class="js__string">"Italy"</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"key"</span>:&nbsp;<span class="js__string">"value"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"type"</span>:&nbsp;<span class="js__string">"Int32"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"value"</span>:&nbsp;<span class="js__string">"25"</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;]&nbsp;
<span class="js__brace">}</span></pre>
</div>
</div>
</div>

*   **Xml Template**: this option allows to select one or more Xml templates. Each template defines the payload for a BrokeredMessage or EventData and the value of the system and the custom properties. Note: the payload is contained in CDATA section.

**BrokeredMessage Xml Template**:

&nbsp;

<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;?xml version="1.0" encoding="us-ascii"?&gt;
&lt;brokeredMessage xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
                 xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
                 xmlns="http://schemas.microsoft.com/servicebusexplorer"&gt;
  &lt;label&gt;Service Bus Explorer&lt;/label&gt;
  &lt;forcePersistence&gt;false&lt;/forcePersistence&gt;
  &lt;message&gt;&lt;![CDATA[{"deviceId":"1", "value":"18"}]]&gt;&lt;/message&gt;
  &lt;properties&gt;
    &lt;property&gt;
      &lt;key&gt;deviceid&lt;/key&gt;
      &lt;type&gt;String&lt;/type&gt;
      &lt;value&gt;1&lt;/value&gt;
    &lt;/property&gt;
    &lt;property&gt;
      &lt;key&gt;location&lt;/key&gt;
      &lt;type&gt;String&lt;/type&gt;
      &lt;value&gt;Room 1&lt;/value&gt;
    &lt;/property&gt;
    &lt;property&gt;
      &lt;key&gt;city&lt;/key&gt;
      &lt;type&gt;String&lt;/type&gt;
      &lt;value&gt;Milan&lt;/value&gt;
    &lt;/property&gt;
    &lt;property&gt;
      &lt;key&gt;country&lt;/key&gt;
      &lt;type&gt;String&lt;/type&gt;
      &lt;value&gt;Italy&lt;/value&gt;
    &lt;/property&gt;
    &lt;property&gt;
      &lt;key&gt;country&lt;/key&gt;
      &lt;type&gt;Int32&lt;/type&gt;
      &lt;value&gt;18&lt;/value&gt;
    &lt;/property&gt;
  &lt;/properties&gt;
&lt;/brokeredMessage&gt;</pre>
<div class="preview">
<pre class="xml"><span class="xml__tag_start">&lt;?xml</span>&nbsp;<span class="xml__attr_name">version</span>=<span class="xml__attr_value">"1.0"</span>&nbsp;<span class="xml__attr_name">encoding</span>=<span class="xml__attr_value">"us-ascii"</span><span class="xml__tag_start">?&gt;</span>&nbsp;
<span class="xml__tag_start">&lt;brokeredMessage</span>&nbsp;<span class="xml__keyword">xmlns</span>:<span class="xml__attr_name">xsi</span>=<span class="xml__attr_value">"http://www.w3.org/2001/XMLSchema-instance"</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__keyword">xmlns</span>:<span class="xml__attr_name">xsd</span>=<span class="xml__attr_value">"http://www.w3.org/2001/XMLSchema"</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__attr_name">xmlns</span>=<span class="xml__attr_value">"http://schemas.microsoft.com/servicebusexplorer"</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;<span class="xml__tag_start">&lt;label</span><span class="xml__tag_start">&gt;</span>Service&nbsp;Bus&nbsp;Explorer<span class="xml__tag_end">&lt;/label&gt;</span>&nbsp;
&nbsp;&nbsp;<span class="xml__tag_start">&lt;forcePersistence</span><span class="xml__tag_start">&gt;</span>false<span class="xml__tag_end">&lt;/forcePersistence&gt;</span>&nbsp;
&nbsp;&nbsp;<span class="xml__tag_start">&lt;message</span><span class="xml__tag_start">&gt;</span><span class="xml__unParsedSection">&lt;![CDATA[{"deviceId":"1",&nbsp;"value":"18"}]]&gt;</span><span class="xml__tag_end">&lt;/message&gt;</span>&nbsp;
&nbsp;&nbsp;<span class="xml__tag_start">&lt;properties</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;property</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;key</span><span class="xml__tag_start">&gt;</span>deviceid<span class="xml__tag_end">&lt;/key&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;type</span><span class="xml__tag_start">&gt;</span>String<span class="xml__tag_end">&lt;/type&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;value</span><span class="xml__tag_start">&gt;</span>1<span class="xml__tag_end">&lt;/value&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_end">&lt;/property&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;property</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;key</span><span class="xml__tag_start">&gt;</span>location<span class="xml__tag_end">&lt;/key&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;type</span><span class="xml__tag_start">&gt;</span>String<span class="xml__tag_end">&lt;/type&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;value</span><span class="xml__tag_start">&gt;</span>Room&nbsp;1<span class="xml__tag_end">&lt;/value&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_end">&lt;/property&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;property</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;key</span><span class="xml__tag_start">&gt;</span>city<span class="xml__tag_end">&lt;/key&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;type</span><span class="xml__tag_start">&gt;</span>String<span class="xml__tag_end">&lt;/type&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;value</span><span class="xml__tag_start">&gt;</span>Milan<span class="xml__tag_end">&lt;/value&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_end">&lt;/property&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;property</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;key</span><span class="xml__tag_start">&gt;</span>country<span class="xml__tag_end">&lt;/key&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;type</span><span class="xml__tag_start">&gt;</span>String<span class="xml__tag_end">&lt;/type&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;value</span><span class="xml__tag_start">&gt;</span>Italy<span class="xml__tag_end">&lt;/value&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_end">&lt;/property&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;property</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;key</span><span class="xml__tag_start">&gt;</span>country<span class="xml__tag_end">&lt;/key&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;type</span><span class="xml__tag_start">&gt;</span>Int32<span class="xml__tag_end">&lt;/type&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;value</span><span class="xml__tag_start">&gt;</span>18<span class="xml__tag_end">&lt;/value&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_end">&lt;/property&gt;</span>&nbsp;
&nbsp;&nbsp;<span class="xml__tag_end">&lt;/properties&gt;</span>&nbsp;
<span class="xml__tag_end">&lt;/brokeredMessage&gt;</span></pre>
</div>
</div>
</div>

**EventData Xml Template**:

<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;?xml version="1.0" encoding="us-ascii"?&gt;

&lt;eventData xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
           xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
           xmlns="http://schemas.microsoft.com/servicebusexplorer"&gt;
  &lt;partitionKey&gt;1&lt;/partitionKey&gt;
  &lt;message&gt;&lt;![CDATA[{"deviceId":"1", "value":"18"}]]&gt;&lt;/message&gt;
  &lt;properties&gt;
    &lt;property&gt;
      &lt;key&gt;deviceId&lt;/key&gt;
      &lt;type&gt;String&lt;/type&gt;
      &lt;value&gt;1&lt;/value&gt;
    &lt;/property&gt;
    &lt;property&gt;
      &lt;key&gt;location&lt;/key&gt;
      &lt;type&gt;String&lt;/type&gt;
      &lt;value&gt;Room 1&lt;/value&gt;
    &lt;/property&gt;
    &lt;property&gt;
      &lt;key&gt;city&lt;/key&gt;
      &lt;type&gt;String&lt;/type&gt;
      &lt;value&gt;Milan&lt;/value&gt;
    &lt;/property&gt;
    &lt;property&gt;
      &lt;key&gt;country&lt;/key&gt;
      &lt;type&gt;String&lt;/type&gt;
      &lt;value&gt;Italy&lt;/value&gt;
    &lt;/property&gt;
    &lt;property&gt;
      &lt;key&gt;value&lt;/key&gt;
      &lt;type&gt;Int32&lt;/type&gt;
      &lt;value&gt;18&lt;/value&gt;
    &lt;/property&gt;
  &lt;/properties&gt;
&lt;/eventData&gt;</pre>
<div class="preview">
<pre class="xml"><span class="xml__tag_start">&lt;?xml</span>&nbsp;<span class="xml__attr_name">version</span>=<span class="xml__attr_value">"1.0"</span>&nbsp;<span class="xml__attr_name">encoding</span>=<span class="xml__attr_value">"us-ascii"</span><span class="xml__tag_start">?&gt;</span>&nbsp;
&nbsp;
<span class="xml__tag_start">&lt;eventData</span>&nbsp;<span class="xml__keyword">xmlns</span>:<span class="xml__attr_name">xsi</span>=<span class="xml__attr_value">"http://www.w3.org/2001/XMLSchema-instance"</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__keyword">xmlns</span>:<span class="xml__attr_name">xsd</span>=<span class="xml__attr_value">"http://www.w3.org/2001/XMLSchema"</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__attr_name">xmlns</span>=<span class="xml__attr_value">"http://schemas.microsoft.com/servicebusexplorer"</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;<span class="xml__tag_start">&lt;partitionKey</span><span class="xml__tag_start">&gt;</span>1<span class="xml__tag_end">&lt;/partitionKey&gt;</span>&nbsp;
&nbsp;&nbsp;<span class="xml__tag_start">&lt;message</span><span class="xml__tag_start">&gt;</span><span class="xml__unParsedSection">&lt;![CDATA[{"deviceId":"1",&nbsp;"value":"18"}]]&gt;</span><span class="xml__tag_end">&lt;/message&gt;</span>&nbsp;
&nbsp;&nbsp;<span class="xml__tag_start">&lt;properties</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;property</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;key</span><span class="xml__tag_start">&gt;</span>deviceId<span class="xml__tag_end">&lt;/key&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;type</span><span class="xml__tag_start">&gt;</span>String<span class="xml__tag_end">&lt;/type&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;value</span><span class="xml__tag_start">&gt;</span>1<span class="xml__tag_end">&lt;/value&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_end">&lt;/property&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;property</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;key</span><span class="xml__tag_start">&gt;</span>location<span class="xml__tag_end">&lt;/key&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;type</span><span class="xml__tag_start">&gt;</span>String<span class="xml__tag_end">&lt;/type&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;value</span><span class="xml__tag_start">&gt;</span>Room&nbsp;1<span class="xml__tag_end">&lt;/value&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_end">&lt;/property&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;property</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;key</span><span class="xml__tag_start">&gt;</span>city<span class="xml__tag_end">&lt;/key&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;type</span><span class="xml__tag_start">&gt;</span>String<span class="xml__tag_end">&lt;/type&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;value</span><span class="xml__tag_start">&gt;</span>Milan<span class="xml__tag_end">&lt;/value&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_end">&lt;/property&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;property</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;key</span><span class="xml__tag_start">&gt;</span>country<span class="xml__tag_end">&lt;/key&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;type</span><span class="xml__tag_start">&gt;</span>String<span class="xml__tag_end">&lt;/type&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;value</span><span class="xml__tag_start">&gt;</span>Italy<span class="xml__tag_end">&lt;/value&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_end">&lt;/property&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;property</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;key</span><span class="xml__tag_start">&gt;</span>value<span class="xml__tag_end">&lt;/key&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;type</span><span class="xml__tag_start">&gt;</span>Int32<span class="xml__tag_end">&lt;/type&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;value</span><span class="xml__tag_start">&gt;</span>18<span class="xml__tag_end">&lt;/value&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_end">&lt;/property&gt;</span>&nbsp;
&nbsp;&nbsp;<span class="xml__tag_end">&lt;/properties&gt;</span>&nbsp;
<span class="xml__tag_end">&lt;/eventData&gt;</span></pre>
</div>
</div>
</div>

*   **Note**: both Json and Xml templates for **BrokeredMessage** objects are defined by the **BrokeredMessageTemplate** class. Likewise, both Json and Xml templates for **EventData** objects are defined by the **EventaDataTemplate** class.*   **Note**: when using Files, the number of messages to send is still determined by the value of the **Message Count** textbox under the **Sender** tab. If the number of messages to send is higher than the number of selected files or templates, these are treated as a circular list.

*   **Brokered Message Generators: **this release introduces the possibility to extend the tool with extension components. In particular, BrokeredMessage generators are components that allows to create a configurable amount of messages to send  to a queue or topic.*   These components needs to implement the **IBrokeredMessageGenerator** interface defined by the tool:
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public interface IBrokeredMessageGenerator
    {
        IEnumerable&lt;BrokeredMessage&gt; GenerateBrokeredMessageCollection(int messageCount,
                                                                       WriteToLogDelegate writeToLog = null);
    }
}</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">namespace</span>&nbsp;Microsoft.WindowsAzure.CAT.ServiceBusExplorer&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">interface</span>&nbsp;IBrokeredMessageGenerator&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IEnumerable&lt;BrokeredMessage&gt;&nbsp;GenerateBrokeredMessageCollection(<span class="cs__keyword">int</span>&nbsp;messageCount,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;WriteToLogDelegate&nbsp;writeToLog&nbsp;=&nbsp;<span class="cs__keyword">null</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode">

*   &nbsp;The dll containing these components needs to be copied in the directory containing the tool. Finally, you need to specify the fully qualified name of the class in the **brokeredMessageGenerators** section of the configuration file as shown below:
</div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;configuration&gt;
  ...
  &lt;brokeredMessageGenerators&gt;
    &lt;add key="OnOffDeviceBrokeredMessageGenerator" 
         value="Microsoft.WindowsAzure.CAT.ServiceBusExplorer.OnOffDeviceBrokeredMessageGenerator,ServiceBusExplorer" /&gt;
    &lt;add key="ThresholdDeviceBrokeredMessageGenerator" 
         value="Microsoft.WindowsAzure.CAT.ServiceBusExplorer.ThresholdDeviceBrokeredMessageGenerator,ServiceBusExplorer" /&gt;
  &lt;/brokeredMessageGenerators&gt;
  ...
&lt;/configuration&gt;</pre>
<div class="preview">
<pre class="xml"><span class="xml__tag_start">&lt;?xml</span>&nbsp;<span class="xml__attr_name">version</span>=<span class="xml__attr_value">"1.0"</span>&nbsp;<span class="xml__attr_name">encoding</span>=<span class="xml__attr_value">"utf-8"</span><span class="xml__tag_start">?&gt;</span>&nbsp;
<span class="xml__tag_start">&lt;configuration</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;...&nbsp;
&nbsp;&nbsp;<span class="xml__tag_start">&lt;brokeredMessageGenerators</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;add</span>&nbsp;<span class="xml__attr_name">key</span>=<span class="xml__attr_value">"OnOffDeviceBrokeredMessageGenerator"</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__attr_name">value</span>=<span class="xml__attr_value">"Microsoft.WindowsAzure.CAT.ServiceBusExplorer.OnOffDeviceBrokeredMessageGenerator,ServiceBusExplorer"</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;add</span>&nbsp;<span class="xml__attr_name">key</span>=<span class="xml__attr_value">"ThresholdDeviceBrokeredMessageGenerator"</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__attr_name">value</span>=<span class="xml__attr_value">"Microsoft.WindowsAzure.CAT.ServiceBusExplorer.ThresholdDeviceBrokeredMessageGenerator,ServiceBusExplorer"</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;<span class="xml__tag_end">&lt;/brokeredMessageGenerators&gt;</span>&nbsp;
&nbsp;&nbsp;...&nbsp;
<span class="xml__tag_end">&lt;/configuration&gt;</span></pre>
</div>
</div>
</div>

*   &nbsp;The tool provides two brokered message generators out of the box:
    *   **OnOffDeviceBrokeredMessageGenerator**: this components allows to simulate signals coming from a configurable amount of devices with can assume of two values: On (1) and Off (0). The component can be configured to send the payload in JSON  or XML format.&nbsp;

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/120286/1/onoffdevicebrokeredmessagegenerator.png)

Messages generated by the component have the following payload and properties.

&nbsp;

<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">Payload:
{"deviceid":93,"value":0}
Properties:
- Key=[deviceId] Value=[93]
- Key=[value] Value=[0]
- Key=[time] Value=[635398156544407695]
- Key=[city] Value=[Milan]
- Key=[country] Value=[Italy]</pre>
<div class="preview">
<pre class="js">Payload:&nbsp;
<span class="js__brace">{</span><span class="js__string">"deviceid"</span>:<span class="js__num">93</span>,<span class="js__string">"value"</span>:<span class="js__num">0</span><span class="js__brace">}</span>&nbsp;
Properties:&nbsp;
-&nbsp;Key=[deviceId]&nbsp;Value=[<span class="js__num">93</span>]&nbsp;
-&nbsp;Key=[value]&nbsp;Value=[<span class="js__num">0</span>]&nbsp;
-&nbsp;Key=[time]&nbsp;Value=[<span class="js__num">635398156544407695</span>]&nbsp;
-&nbsp;Key=[city]&nbsp;Value=[Milan]&nbsp;
-&nbsp;Key=[country]&nbsp;Value=[Italy]</pre>
</div>
</div>
</div>

&nbsp;

*   &nbsp;
    *   **ThresholdDeviceBrokeredMessageGenerator**: this components allows to simulate signals coming from a configurable amount of devices with can assume values within a range defined by . The component can be configured to send the payload in JSON  or XML format.&nbsp;

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/120287/1/thesholddevicebrokeredmessagegenerator.png)

Messages generated by the component have the following payload and properties.

&nbsp;

<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">Payload:
{"deviceid":93,"value":97}
Properties:
- Key=[deviceId] Value=[93]
- Key=[value] Value=[97]
- Key=[time] Value=[635398156544407695]
- Key=[city] Value=[Milan]
- Key=[country] Value=[Italy]</pre>
<div class="preview">
<pre class="js">Payload:&nbsp;
<span class="js__brace">{</span><span class="js__string">"deviceid"</span>:<span class="js__num">93</span>,<span class="js__string">"value"</span>:<span class="js__num">97</span><span class="js__brace">}</span>&nbsp;
Properties:&nbsp;
-&nbsp;Key=[deviceId]&nbsp;Value=[<span class="js__num">93</span>]&nbsp;
-&nbsp;Key=[value]&nbsp;Value=[<span class="js__num">97</span>]&nbsp;
-&nbsp;Key=[time]&nbsp;Value=[<span class="js__num">635398156544407695</span>]&nbsp;
-&nbsp;Key=[city]&nbsp;Value=[Milan]&nbsp;
-&nbsp;Key=[country]&nbsp;Value=[Italy]</pre>
</div>
</div>
</div>

&nbsp;

*   BrokeredMessage generators can be selected in the **Send Messages** dialog or in the **Test queue / topic in SDI / MDI** control as selecting the ** Generator** tab as shown in the figure below:

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/120288/1/sendmessagestoeventhub.png)

*   **Note**: when this option is selected, the properties defined in the grid under the Message Properties control group are ignored.

*   **Brokered Message Inspectors:** the tool now allows to create a message inspector that can be used to intercept and eventually modify any outgoing or upcoming message sent to a queue or topic or received from a queue or a subscription.*   These components needs to implement the **IBrokeredMessageGenerator** interface defined by the tool:
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public interface IBrokeredMessageInspector
    {
        BrokeredMessage BeforeSendMessage(BrokeredMessage message, 
                                          WriteToLogDelegate writeToLog = null);
        BrokeredMessage AfterReceiveMessage(BrokeredMessage message, 
                                            WriteToLogDelegate writeToLog = null);
    }
}</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">namespace</span>&nbsp;Microsoft.WindowsAzure.CAT.ServiceBusExplorer&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">interface</span>&nbsp;IBrokeredMessageInspector&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BrokeredMessage&nbsp;BeforeSendMessage(BrokeredMessage&nbsp;message,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;WriteToLogDelegate&nbsp;writeToLog&nbsp;=&nbsp;<span class="cs__keyword">null</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BrokeredMessage&nbsp;AfterReceiveMessage(BrokeredMessage&nbsp;message,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;WriteToLogDelegate&nbsp;writeToLog&nbsp;=&nbsp;<span class="cs__keyword">null</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode">

*   The dll containing these components needs to be copied in the directory containing the tool. Finally, you need to specify the fully qualified name of the class in the **brokeredMessageGenerators** section of the configuration file as shown below:
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;configuration&gt;
  ...
  &lt;brokeredMessageInspectors&gt;
    &lt;add key="LogBrokeredMessageInspector" 
         value="Microsoft.WindowsAzure.CAT.ServiceBusExplorer.LogBrokeredMessageInspector,ServiceBusExplorer" /&gt;
    &lt;add key="ZipBrokeredMessageInspector" 
         value="Microsoft.WindowsAzure.CAT.ServiceBusExplorer.ZipBrokeredMessageInspector,ServiceBusExplorer" /&gt;
  &lt;/brokeredMessageInspectors&gt;
 ...
&lt;/configuration&gt;</pre>
<div class="preview">
<pre class="xml"><span class="xml__tag_start">&lt;?xml</span>&nbsp;<span class="xml__attr_name">version</span>=<span class="xml__attr_value">"1.0"</span>&nbsp;<span class="xml__attr_name">encoding</span>=<span class="xml__attr_value">"utf-8"</span><span class="xml__tag_start">?&gt;</span>&nbsp;
<span class="xml__tag_start">&lt;configuration</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;...&nbsp;
&nbsp;&nbsp;<span class="xml__tag_start">&lt;brokeredMessageInspectors</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;add</span>&nbsp;<span class="xml__attr_name">key</span>=<span class="xml__attr_value">"LogBrokeredMessageInspector"</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__attr_name">value</span>=<span class="xml__attr_value">"Microsoft.WindowsAzure.CAT.ServiceBusExplorer.LogBrokeredMessageInspector,ServiceBusExplorer"</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;add</span>&nbsp;<span class="xml__attr_name">key</span>=<span class="xml__attr_value">"ZipBrokeredMessageInspector"</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__attr_name">value</span>=<span class="xml__attr_value">"Microsoft.WindowsAzure.CAT.ServiceBusExplorer.ZipBrokeredMessageInspector,ServiceBusExplorer"</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;<span class="xml__tag_end">&lt;/brokeredMessageInspectors&gt;</span>&nbsp;
&nbsp;...&nbsp;
<span class="xml__tag_end">&lt;/configuration&gt;</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;The tool provides two BrokeredMessage generators out of the box:</div>
</div>

*   **ZipBrokeredMessageInspector**: this component can be used to zip the body of outgoing messages sent to queue or topic and/or to unzip the content of messages received from a queue or subscription.*   **LogBrokeredMessageInspector**: this component can be used to log the body and custom properties of outgoing messages sent to queue or topic and/or incoming messages read from a queue or subscription. The log is saved in the same directory  of the tool.

BrokeredMessage inspectors can be selected under the following dialogs:

*   In the **Sender** tab** **of the **Send Messages** dialog

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/120290/1/brokeredmessageinspector01.png)

*   Under the **Sender** and **Receiver** tab in the ** Test queue / topic in SDI / MDI** dialog:

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/120291/1/brokeredmessageinspector02.png)

*   In the **Listener** dialog for a queue or subscription.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/120292/1/brokeredmessageinspector03.png)

*   **Note**: when resubmitting multiple messages, it's not possible to select a brokered message inspector*   **Listener Dialog for Queues and Subscriptions**: the following features have been added to the Listener dialog for queues and subscriptions:
    *   The possibility to select a BrokeredMessage inspector (see point 1 in the figure below)*   The Average Time performance counter (see point 3 in the figure below)*   The KB/Sec performance counter (see point 4 in the figure below)*   The possibility to select a scale factor (see points 2, 3 and 4 in the figure below)*   messages in the listener forms are now tracked in an asynchronous way by a dedicated Task T. Receiver tasks just add messages to a BlockingCollection, while T reads messages out of the collection and writes them to the messages Grid control.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/120294/1/listener01.png)

*   <span style="font-size: 10px;">**EventData Generators**:&nbsp;this release introduces the possibility to extend the tool with extension components. In particular, EventData generators are components that allows to create a configurable amount  of EventData objects to send to an event hub.</span>*   These components needs to implement the **IEventDataGenerator** interface defined by the tool:

&nbsp;

<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public interface IEventDataGenerator
    {
        IEnumerable&lt;EventData&gt; GenerateEventDataCollection(int eventDataCount, 
                                                           WriteToLogDelegate writeToLog = null);
    }
}</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">namespace</span>&nbsp;Microsoft.WindowsAzure.CAT.ServiceBusExplorer&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">interface</span>&nbsp;IEventDataGenerator&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IEnumerable&lt;EventData&gt;&nbsp;GenerateEventDataCollection(<span class="cs__keyword">int</span>&nbsp;eventDataCount,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;WriteToLogDelegate&nbsp;writeToLog&nbsp;=&nbsp;<span class="cs__keyword">null</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode">

*   The dll containing these components needs to be copied in the directory containing the tool. Finally, you need to specify the fully qualified name of the class in the **eventDataGenerators** section of the configuration file as shown below:
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;configuration&gt;
  ...
  &lt;eventDataInspectors&gt;
    &lt;add key="LogEventDataInspector" 
         value="Microsoft.WindowsAzure.CAT.ServiceBusExplorer.LogEventDataInspector,ServiceBusExplorer" /&gt;
    &lt;add key="ZipEventDataInspector" 
         value="Microsoft.WindowsAzure.CAT.ServiceBusExplorer.ZipEventDataInspector,ServiceBusExplorer" /&gt;
  &lt;/eventDataInspectors&gt;
  ...
&lt;/configuration&gt;</pre>
<div class="preview">
<pre class="xml"><span class="xml__tag_start">&lt;?xml</span>&nbsp;<span class="xml__attr_name">version</span>=<span class="xml__attr_value">"1.0"</span>&nbsp;<span class="xml__attr_name">encoding</span>=<span class="xml__attr_value">"utf-8"</span><span class="xml__tag_start">?&gt;</span>&nbsp;
<span class="xml__tag_start">&lt;configuration</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;...&nbsp;
&nbsp;&nbsp;<span class="xml__tag_start">&lt;eventDataInspectors</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;add</span>&nbsp;<span class="xml__attr_name">key</span>=<span class="xml__attr_value">"LogEventDataInspector"</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__attr_name">value</span>=<span class="xml__attr_value">"Microsoft.WindowsAzure.CAT.ServiceBusExplorer.LogEventDataInspector,ServiceBusExplorer"</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;add</span>&nbsp;<span class="xml__attr_name">key</span>=<span class="xml__attr_value">"ZipEventDataInspector"</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__attr_name">value</span>=<span class="xml__attr_value">"Microsoft.WindowsAzure.CAT.ServiceBusExplorer.ZipEventDataInspector,ServiceBusExplorer"</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;<span class="xml__tag_end">&lt;/eventDataInspectors&gt;</span>&nbsp;
&nbsp;&nbsp;...&nbsp;
<span class="xml__tag_end">&lt;/configuration&gt;</span></pre>
</div>
</div>
</div>
</div>

*   &nbsp;The tool provides two brokered message generators out of the box:
    *   **OnOffDeviceEventDataGenerator**: this components allows to simulate signals coming from a configurable amount of devices with can assume of two values: On (1) and Off (0). The component can be configured to send the payload in JSON or XML  format.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/120295/1/onoffdeviceeventdatagenerator.png)

Messages generated by the component have the following payload and properties.

&nbsp;

<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">Payload:
{"deviceid":93,"value":0}
Properties:
- Key=[deviceId] Value=[93]
- Key=[value] Value=[0]
- Key=[time] Value=[635398156544407695]
- Key=[city] Value=[Milan]
- Key=[country] Value=[Italy]</pre>
<div class="preview">
<pre class="js">Payload:&nbsp;
<span class="js__brace">{</span><span class="js__string">"deviceid"</span>:<span class="js__num">93</span>,<span class="js__string">"value"</span>:<span class="js__num">0</span><span class="js__brace">}</span>&nbsp;
Properties:&nbsp;
-&nbsp;Key=[deviceId]&nbsp;Value=[<span class="js__num">93</span>]&nbsp;
-&nbsp;Key=[value]&nbsp;Value=[<span class="js__num">0</span>]&nbsp;
-&nbsp;Key=[time]&nbsp;Value=[<span class="js__num">635398156544407695</span>]&nbsp;
-&nbsp;Key=[city]&nbsp;Value=[Milan]&nbsp;
-&nbsp;Key=[country]&nbsp;Value=[Italy]</pre>
</div>
</div>
</div>

&nbsp;

*   *   **ThresholdDeviceEventDataGenerator**: this components allows to simulate signals coming from a configurable amount of devices with can assume values within a range defined by . The component can be configured to send the payload in JSON or  XML format.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/120297/1/thresholdoffdeviceeventdatagenerator.png)

Messages generated by the component have the following payload and properties.

&nbsp;

<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">Payload:
{"deviceid":93,"value":97}
Properties:
- Key=[deviceId] Value=[93]
- Key=[value] Value=[97]
- Key=[time] Value=[635398156544407695]
- Key=[city] Value=[Milan]
- Key=[country] Value=[Italy]</pre>
<div class="preview">
<pre class="js">Payload:&nbsp;
<span class="js__brace">{</span><span class="js__string">"deviceid"</span>:<span class="js__num">93</span>,<span class="js__string">"value"</span>:<span class="js__num">97</span><span class="js__brace">}</span>&nbsp;
Properties:&nbsp;
-&nbsp;Key=[deviceId]&nbsp;Value=[<span class="js__num">93</span>]&nbsp;
-&nbsp;Key=[value]&nbsp;Value=[<span class="js__num">97</span>]&nbsp;
-&nbsp;Key=[time]&nbsp;Value=[<span class="js__num">635398156544407695</span>]&nbsp;
-&nbsp;Key=[city]&nbsp;Value=[Milan]&nbsp;
-&nbsp;Key=[country]&nbsp;Value=[Italy]</pre>
</div>
</div>
</div>

&nbsp;

*   EventData generators can be selected in the **Send Messages** dialog or in the **Test queue / topic in SDI / MDI** control as selecting the ** Generator** tab as shown in the figure below:

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/120299/1/sendmessagestoeventhub.png)

*   **Note**: when this option is selected, the properties defined in the grid under the Message Properties control group are ignored.*   **EventData Inspectors**:&nbsp;this release of the tool now allows to create an EventData inspector that can be used to intercept and eventually modify any outgoing or upcoming EventData sent to an event hub or received from an event hub partition.*   These components needs to implement the **IEventDataGenerator** interface defined by the tool:

&nbsp;

<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public interface IEventDataInspector
    {
        EventData BeforeSendMessage(EventData eventData, 
                                    WriteToLogDelegate writeToLog = null);
        EventData AfterReceiveMessage(EventData eventData, 
                                      WriteToLogDelegate writeToLog = null);
    }
}</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">namespace</span>&nbsp;Microsoft.WindowsAzure.CAT.ServiceBusExplorer&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">interface</span>&nbsp;IEventDataInspector&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;EventData&nbsp;BeforeSendMessage(EventData&nbsp;eventData,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;WriteToLogDelegate&nbsp;writeToLog&nbsp;=&nbsp;<span class="cs__keyword">null</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;EventData&nbsp;AfterReceiveMessage(EventData&nbsp;eventData,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;WriteToLogDelegate&nbsp;writeToLog&nbsp;=&nbsp;<span class="cs__keyword">null</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode">

*   The dll containing these components needs to be copied in the directory containing the tool. Finally, you need to specify the fully qualified name of the class in the **eventDataGenerators** section of the configuration file as shown below:
</div>

&nbsp;

&nbsp;

<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;configuration&gt;
  ...
  &lt;eventDataGenerators&gt;
    &lt;add key="OnOffDeviceEventDataGenerator" 
         value="Microsoft.WindowsAzure.CAT.ServiceBusExplorer.OnOffDeviceEventDataGenerator,ServiceBusExplorer" /&gt;
    &lt;add key="ThresholdDeviceEventDataGenerator" 
         value="Microsoft.WindowsAzure.CAT.ServiceBusExplorer.ThresholdDeviceEventDataGenerator,ServiceBusExplorer" /&gt;
  &lt;/eventDataGenerators&gt;
  ...
&lt;/configuration&gt;</pre>
<div class="preview">
<pre class="xml"><span class="xml__tag_start">&lt;?xml</span>&nbsp;<span class="xml__attr_name">version</span>=<span class="xml__attr_value">"1.0"</span>&nbsp;<span class="xml__attr_name">encoding</span>=<span class="xml__attr_value">"utf-8"</span><span class="xml__tag_start">?&gt;</span>&nbsp;
<span class="xml__tag_start">&lt;configuration</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;...&nbsp;
&nbsp;&nbsp;<span class="xml__tag_start">&lt;eventDataGenerators</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;add</span>&nbsp;<span class="xml__attr_name">key</span>=<span class="xml__attr_value">"OnOffDeviceEventDataGenerator"</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__attr_name">value</span>=<span class="xml__attr_value">"Microsoft.WindowsAzure.CAT.ServiceBusExplorer.OnOffDeviceEventDataGenerator,ServiceBusExplorer"</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;add</span>&nbsp;<span class="xml__attr_name">key</span>=<span class="xml__attr_value">"ThresholdDeviceEventDataGenerator"</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__attr_name">value</span>=<span class="xml__attr_value">"Microsoft.WindowsAzure.CAT.ServiceBusExplorer.ThresholdDeviceEventDataGenerator,ServiceBusExplorer"</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;<span class="xml__tag_end">&lt;/eventDataGenerators&gt;</span>&nbsp;
&nbsp;&nbsp;...&nbsp;
<span class="xml__tag_end">&lt;/configuration&gt;</span></pre>
</div>
</div>
</div>

&nbsp;

*   &nbsp;The tool provides two EventData generators out of the box:
    *   **ZipEventDataInspector**: this component can be used to zip the body of outgoing event data objects sent to an event hub and/or to unzip the content of event data objects received from an event hub partition.*   **LogEventDataInspector**: this component can be used to log the body and custom properties of event data objects sent to an event hub and/or incoming event data objects read from an event hub partition. The log is saved in the same directory  of the tool.

EventData inspectors can be selected under the following dialogs:

*   In the **Sender** tab** **of the **Send Messages** dialog

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/120300/1/eventdatainspector01.png)

*   In the **Listener** dialog for a queue or subscription.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/120301/1/eventdatainspector02.png)

&nbsp;

*   **Listener Dialog for Consumer Groups or individual Partitions**:&nbsp;this release of the tool allows to create a listener to receive even data messages from all the partitions of an event hub or just from an single partition:
    *   The possibility to select an EventData inspector (see point 1 in the figure below)*   The Listener dialog exposes the following performance counters:
        *   Events Total (see point 2 in the figure below)*   Events/sec (see point 3 in the figure below)*   The Average Time (see point 4 in the figure below)*   The KB/Sec (see point 5 in the figure below)

        *   The possibility to select a scale factor (see points 2, 3 , 4, 5 in the figure below)*   You can select a Partition from the dropdown list to see its information in the panel below (see point 6)*   When message tracking is enabled using the corresponding checkbox, event data messages are saved in the grid available in the Events tab (see point 7)*   In the Options panel you can specify options:
        *   Refresh Interval: the amount of time expressed in seconds after which partition information is refreshed from the server.*   Receive Timeout: the timeout used by the ReceiveAsync method used by receivers, one for each partition. Note: when the Checkpoint option is enabled, a checkpoint is executed at every timeout.*   Prefetch Count: defines a value for the PrefetchCount property of receivers.*   Offset: specify the offset at which each receiver will start receiving event data from a partition.*   Checkpoint Count: when Checkpoint option is enabled, specifies after how many event data each receiver performs a checkpoint. **Note**: in the current version this option is disabled because is not supported by the current version of the Microsoft.ServiceBus.dll.*   Logging: enable or disable logging.*   Verbose: enable or disable verbose logging.*   Tracking: enable of disable event data tracking.*   Graph: enable or disable the graph.*   Checkpoint: enable or disable checkpoints.&nbsp;**Note**: in the current version this option is disabled because is not supported by the current version of the Microsoft.ServiceBus.dll.

        *   Messages in the listener forms are now tracked in an asynchronous way by a dedicated Task T. Receiver tasks just add messages to a BlockingCollection, while T reads messages out of the collection and writes them to the messages Grid control.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/120302/1/partitionlisterner01.png)

*   **Message Payload Visualization**:&nbsp;The font used to show the payload of messages has been hanged from **Microsoft Sans Serif** to **Consolas**.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/120303/1/consolas.png)

*   **Binary Messages support**:&nbsp;The tool now supports the possibility to send binary files (more on this below in the file template section). Likewise, the tool adds support to display binary files in hexadecimal format. The empiric rule  to establish if the payload of a BrokeredMessage or EventData object is binay is the presence of control characters other than line feed, carriage return and horizontal tab.

![](http://i1.code.msdn.s-msft.com/windowsazure/service-bus-explorer-f2abca5a/image/file/120304/1/binaryfiles01.png)

*   Other improvements
    *   Greatly improved the Dispose of user controls.*   Increased the performance and stability of logging.

**Update**: 22 July 2014

This version introduces the following updates:

*   Changed logging: now when you stop a queue, subscription, consumer group or partition listener, the log stops immediately.*   Updated Microsoft.ServiceBus.dll to version 2.4.1.1.*   Minor UI changes.
