# Documentation
<div>The following picture illustrates the high-level architecture of the Service Bus Explorer tool. The application has been written in C# using <a href="http://www.microsoft.com/visualstudio/en-us">Visual Studio 2010</a> and requires the installation of the .<a href="http://www.microsoft.com/download/en/details.aspx?id=17718">NET Framework 4.0</a> and <a href="http://www.microsoft.com/download/en/details.aspx?displaylang=en&amp;id=27421"> </a><a title="Windows Azure SDK for .NET" href="http://www.microsoft.com/download/en/details.aspx?id=28045">Windows Azure SDK for .NET</a>. The tool can be copied and used on any workstation that satisfies the prerequisites mentioned above to manage and test  the Brokered and Relay messaging services defined in a given Service Bus namespace.</div>
<div style="text-align: center;">&nbsp;</div>
<div style="text-align: center;"><img id="143452" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143452/1/servicebusexplorer.jpg" alt="" /></div>
<div></div>
<div><strong>NOTE</strong>: I'll continue to develop the tool and add new functionalities. So I strongly recommend you to visit this page from time to time for a new version.</div>
<div><strong>Author: </strong>Paolo Salvatori</div>
<div><strong>Update</strong>: 28 August 2012</div>
<div>This version introduces the following updates:</div>
<ul>
<li>A new flat UI. This change has been done primarily to match Windows 8 and Windows Server 2012 new UI style. </li>
<li>Two new options in the configuration file and Options form that allow to save respectively the message text and user-defined properties of a BrokeredMessage between 2 runs. This way, you don't have to re-enter user-defined properties when you start a new  session. </li>
<li>The possibility to define the body of BrokeredMessage as a stream. Now in the Sender tab of a queue and topic you can select 3 different formats for the payload: string, stream and WCF message. </li>
</ul>
<div><strong>Update</strong>: 18&nbsp;December 2012</div>
<div>This version introduces the following updates:</div>
<ul>
<li>Implemented Disable/Enable operations for queues and topics. </li>
</ul>
<div style="text-align: center;"><img id="143380" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143380/1/disableentity.png" alt="" width="385" height="599" /></div>
<div>&nbsp;</div>
<ul>
<li>Implemented Update operation for queues, topics and subscriptions. </li>
<li>Added OData filter support for queues, topics and subscriptions in the ConnectForm. </li>
<li>Added OData filter support on context menu of queues, topics and subscriptions. </li>
<li>Added FilterForm that allows to compose a valid OData filter both as free-text or using the UI. The 2 mechanisms are synchronized. </li>
</ul>
<div style="text-align: center;"><img id="143381" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143381/1/filterform.png" alt="" width="459" height="562" /></div>
<div>&nbsp;</div>
<ul>
<li>Added support for Windows Azure Service Bus connection strings in the Connect Form. </li>
</ul>
<div style="text-align: center;"><img id="143382" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143382/1/connectionstring.png" alt="" width="469" height="668" /></div>
<div>&nbsp;</div>
<ul>
<li>Added support for Service Bus for Windows Server and Windows Azure Service Bus connection strings in the configuration file. </li>
<li>Added support for F5 shortcut to refresh current entity/entities </li>
<li>Added connection string textbox in the Connect form. </li>
<li>Added tooltip with samples for cloud &amp; server connection strings in the Connect form. </li>
<li>Added checkbox for IsAnonymousAccessible property to topic and queue management control. </li>
<li>Added checkbox for EnableFilteringMessagesBeforePublishing property to topic management control. </li>
<li>Added TextBox for UserMetadata property to queue, topic and subscription management control. </li>
<li>Added TextBox for ForwardTo property to queue and subscription management control. </li>
<li>Added visualization of IsReadOnly property to queue, topic and subscription management control. </li>
<li>Added ForwardToForm to select a target queue or topic from a treeview as value for the ForwardTo property. </li>
</ul>
<div style="text-align: center;"><img id="143383" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143383/1/forwardform.png" alt="" width="562" height="563" /></div>
<div>&nbsp;</div>
<ul>
<li>Added support for SendBatch to the Sender tab of queue/topic test controls. </li>
<li>Added support for ReceiveBatch to the Receiver tab of queue/topic/subscription test controls. </li>
<li>Added support for Get Message Sessions to queue and subscription context menus for session-aware queues and subscriptions. </li>
</ul>
<div style="text-align: center;"><img id="143384" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143384/1/getmessagesessions.png" alt="" width="376" height="705" /></div>
<div>&nbsp;</div>
<ul>
<li>Replaced the absolute Uri of a target queue or topic with the value of the Path property (queues) and name property (subscriptions) when loading the value of the ForwardTo property. </li>
<li>Added ListView for entity information (AccessedAt, UpdatedAt, Status, MessageCountDetail, etc.) to queue, topic and subscription management control. </li>
<li>Added User Metadata fields. </li>
<li>Replaced TextBox with TrackBar for Maximum Size of Queues and Topics. </li>
<li>Differentiated possible and maximum values for max size of messaging entities for cloud and on-premises Service Bus namespaces. </li>
<li>Fixed Refresh operation for Subscriptions and Rules nodes. </li>
<li>Changed the style of DataGridViews in all controls. </li>
<li>Changed the style of ListViews in all controls.<br /> Adjusted the width of the last column of DataGridViews and ListViews based on control width and vertical scroll bar visibility. </li>
<li>The main panel is cleared when connecting to a new namespace. </li>
<li>Fixed minor issues in the Rule control. </li>
<li>Significantly improved the performance of graph drawing. </li>
<li>Significantly improved the performance of logging. </li>
<li>Fixed problems with graphs when stopping load test. </li>
<li>Fixed problems with graphs when using PrefetchCount &gt; 0 </li>
<li>Hidden the Relay Services node for Service Bus for Windows Server namespaces. </li>
<li>Hidden the Is Anonymous Accessible settings for cloud Service Bus namespaces. </li>
<li>The ConnectForm remembers the last connectionstring opened. </li>
<li>Improved the visualization of the namespace treeview. </li>
<li>Change icon of the namespace treeview. </li>
<li>Changed the About form. </li>
<li>Changed the Windows Azure Logo. </li>
</ul>
<div><strong>Update</strong>:&nbsp;7&nbsp;January 2013</div>
<div>This version introduces the following updates:</div>
<ul>
<li>Fixed&nbsp;Copy &lt;Queue|Topic|Subscription&gt;&nbsp;URL and Copy Deadletter Queue URL for Service Bus for Windows Server namespaces. </li>
</ul>
<div><strong>Update</strong>:&nbsp;8&nbsp;January 2013</div>
<div>This version introduces the following updates:</div>
<ul>
<li>Fixed the address&nbsp;format&nbsp;of the&nbsp;To header when sending WCF messages to queues and topics of a Service Bus for Windows Server namespace. </li>
</ul>
<div><strong>Update</strong>: 12&nbsp;April 2013</div>
<div>This version introduces the following updates:</div>
<ul>
<li>This version uses the Microsoft.ServiceBus.dll 2.0 Beta available on NuGet at <a href="http://nuget.org/packages/WindowsAzure.ServiceBus">http://nuget.org/packages/WindowsAzure.ServiceBus</a>. </li>
<li><strong>SAS and Authorization Rules</strong>: So far, the Windows Azure Service Bus had a strong dependency on ACS for authentication. As you know, Shared Secret is the most commonly used authentication mechanism, but SAML is a good alternative.This generated  some problems, because when ACS is unavailable, applications cannot authenticate and acquire the token necessary to access the Service Bus namespace. That&rsquo;s why the Service Bus team decided to implement Shared Access Signatures, like in storage services:  you will be able to create authorization rules at the queue and topic level to access them directly with a Primary or Secondary key without the need to authenticate against ACS. This should also speed up the access to messaging entities because it cuts the  hand shacking with ACS. In addition, the Windows Azure management portal will soon provide the ability to create multiple signatures with different rights. These signatures will be available to be used in a Service Bus connection string. The most restrictive  scope always win when you have two SAS, one at the namespace level and one at the entity level, with the same key and different access rights.&nbsp;The Service Bus Explorer 2.0 introduces the possibility to define authorization rules at the queue/topic level  as shown in the picture below: </li>
</ul>
<div style="text-align: center;"><img id="143385" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143385/1/authorizationrulestab.png" alt="" width="800" /></div>
<ul>
<li><strong>Metrics</strong>: Windows Azure Service Bus 2.0 introduces the possibility to invoke metrics RESTful services to retrieve useful key performance indicators at the entity level. </li>
<li><strong>Supported Metrics:&nbsp; </strong>Telemetry and usage data include the following metrics: 
<ul>
<li>Length </li>
<li>Size </li>
<li>Incoming Messages </li>
<li>Outgoing Messages </li>
<li>Successful operations </li>
<li>Failed operations </li>
<li>Internal Server Errors </li>
<li>Server Busy Errors </li>
<li>Other Errors </li>
</ul>
</li>
<li>Metrics support the following granularities: 
<ul>
<li>PT5M&nbsp;5 Minutes rollup </li>
<li>PT1H&nbsp;1 Hour rollup </li>
<li>P1D&nbsp;1 Hour rollup </li>
<li>P7D&nbsp;1 Hour rollup </li>
</ul>
</li>
<li>The Service Bus Explorer 2.0 introduces the ability to query telemetry and metrics data. Right-click the namespace root node and choose <strong>Open Metrics in SDI </strong>or <strong>MDI mode </strong>as shown in the following picture: </li>
</ul>
<div style="text-align: center;"><img id="143388" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143388/1/metricrootnode.png" alt="" width="800" /></div>
<ul>
<li>The tool gives the user the ability to define one or more Metrics rules to retrieve metrics data by invokingthe RESTful services exposed by the Service Bus. Note: these are the same services invoked by the Windows Azure Management Portal to show Service  Bus counter in the Dashboard tab. The user can define multiple rules to compare metrics from different entities (e.g. the incoming messages of request queue with the outgoing messages of a response queue in a given timeframe). Metrics from different entities  can be visualized together in the Main Graph, while data and charts for individual metrics can be analyzed on separate tabs. See the following screenshots: </li>
</ul>
<div style="text-align: center;"><img id="143389" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143389/1/metricsquerytab.png" alt="" width="800" /></div>
<div style="text-align: center;"><br /> <img id="143390" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143390/1/metricsmaingraph.png" alt="" width="800" /><br /> <img id="143391" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143391/1/singlemetricstab.png" alt="" width="800" /></div>
<ul>
<li>The rules can be exported/imported to/from an XML file. </li>
<li><strong>Important Note</strong>: to&nbsp;access Metrics data is mandatory to indicate in the configuration file or in the Options form the following values: 
<ul>
<li>Windows Azure Subscription Id </li>
<li>Thumbprint of a Windows Azure Management Certificate </li>
</ul>
</li>
<li>Metrics can also be access at the entity level from the Metrics tab as shown in the following screenshots: </li>
</ul>
<div style="text-align: center;"><img id="143392" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143392/1/metricsonentitytab.png" alt="" width="800" /></div>
<div style="text-align: center;"><img id="143393" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143393/1/metricsonentitytab2.png" alt="" width="800" /></div>
<ul>
<li><strong>Monitor</strong>: the Service Bus Explorer 2.0 introduces the possibility to monitor in real time and define warning and critical thrshold values for the following properties of for queues, topics and subscriptions: 
<ul>
<li>Active Message Count </li>
<li>Deadletter Message Count </li>
<li>Size </li>
</ul>
</li>
<li>The current state of a&nbsp;performance counter can be visualized in the Monitor Rules datagrid or in the chart. </li>
<li>The state is represented by a different color: 
<ul>
<li>Green: Normal </li>
<li>Yellow: Warning </li>
<li>Red: Critical </li>
</ul>
</li>
<li>State transition events are logged in the Monitor Events listbox. </li>
<li>The rules can be exported/imported to/from an XML file. </li>
</ul>
<div style="text-align: center;"><img id="143394" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143394/1/monitortab.png" alt="" width="800" /></div>
<div>&nbsp;</div>
<ul>
<li><strong>Sessions</strong>: the Service Bus Explorer 2.0 introduces the possibility to retrieve the current sessions for a sessionful queue or subscriptions. You can access this functionality from the new <strong>Sessions</strong> button or from the <strong>Get Message Sessions</strong>&nbsp;context menu of a sessionful entity: </li>
</ul>
<p style="text-align: center;"><img id="143406" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143406/1/sessions.png" alt="" width="800" /></p>
<div style="text-align: center;"></div>
<ul>
<li><strong>Peek and Receive Active Messages</strong>: the Service Bus Explorer 2.0 introduces the possibility to peek or receive a configurable amount&nbsp;of messages from a queue or subscription. You can access this functionality from the new <strong>Messages</strong> button or from the following items in the context menu item of a queue or subscription: 
<ul>
<li>Receive All Messages </li>
<li>Receive Top k Messages </li>
<li>Peek Top k Messages </li>
</ul>
</li>
</ul>
<div style="text-align: center;"><img id="143404" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143404/1/readmessages2.png" alt="" width="800" /></div>
<ul>
<li>When you click the <strong>Ok</strong> button in the <strong>Retrieve messages from queue </strong>(or subscription) dialog, messages are retrieved and showned in the following tab. </li>
</ul>
<div style="text-align: center;"><img id="143403" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143403/1/messages2.png" alt="" width="800" /></div>
<ul>
<li>You can browse messages by selecting the corresponding row in the grid. The <strong> Messages</strong> tab shows the following information for the selected message: 
<ul>
<li>Message Text </li>
<li>Message System Properties </li>
<li>Message Custom Properties </li>
</ul>
</li>
<li><strong>Peek and Receive Deadletter Messages</strong>: the Service Bus Explorer 2.0 introduces the possibility to peek or receive a configurable amount&nbsp;of messages from the deadletter queue of queues and subscriptions. You can access this functionality  from the new <strong>Deadletter </strong>button or from the following items in the context menu item of a queue or subscription: </li>
</ul>
<ul>
<li>&nbsp; 
<ul>
<li>Receive All Deadletter Queue Messages </li>
<li>Receive Top k Deadletter Queue Messages </li>
<li>Peek Top k Deadletter Queue Messages </li>
</ul>
</li>
</ul>
<div style="text-align: center;"><img id="143402" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143402/1/deadletter2.png" alt="" width="800" /></div>
<ul>
<li>When you click the <strong>Ok</strong> button in the <strong>Retrieve messages from deadletter&nbsp;queue&nbsp;</strong>dialog, messages are retrieved and showned in the following tab. </li>
</ul>
<div style="text-align: center;"><img id="143401" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143401/1/deadletter3.png" alt="" width="800" /></div>
<div>&nbsp;</div>
<ul>
<li>You can browse messages by selecting the corresponding row in the grid. The <strong> Deadletter</strong> tab shows the following information for the selected message: </li>
</ul>
<ul>
<li>&nbsp; 
<ul>
<li>Message Text </li>
<li>Message System Properties </li>
<li>Message Custom Properties </li>
</ul>
</li>
<li><strong>Repair and Resubmit Message</strong>: the Service Bus Explorer 2.0 introduces the possibility to repair and resubmit a message read or peeked from a queue, subscription or deadletter queue. To perform this operation, just double click a message  in the <strong>Messages </strong>or <strong>Deadletter </strong>tab. This action opens a modal dialog from which you can edit the text, system properties and custom properties of the current message and select a queue or topic in the current namespace to which  send the modified message. </li>
</ul>
<div style="text-align: center;"><img id="143400" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143400/1/repairandresubmit.png" alt="" width="800" /></div>
<ul>
<li><strong>Sender Think Time</strong>: the test queue and test topic controls introduce the ability to define a think time in the <strong>Sender</strong> tab. </li>
</ul>
<div style="text-align: center;"><img id="143407" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143407/1/senderthinktime.png" alt="" width="800" /></div>
<div>&nbsp;</div>
<ul>
<li><strong>Receive Think Time</strong>: the test queue and test topic controls introduce the ability to define a think time in the <strong>Receiver </strong>tab. </li>
</ul>
<div style="text-align: center;"><img id="143408" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143408/1/receiverthinktime.png" alt="" width="800" /></div>
<ul>
<li><strong>Use multiple&nbsp;files as message template</strong>: the Service Bus Explorer 2.0 introduces the possibility to select multiple files from different directories to be used as message templates when testing queues, topics and subscriptions.&nbsp;As  shown in the picture below, in the&nbsp;<strong>Message</strong> tab select the Files sub-tab and use the <strong>Select Files</strong> button to add files to the list of message templates. Select the files that you want to use as message templates during a test. Sender tasks will use the content of these&nbsp;files in a round-robin fashion when sending messages  to queues and topics. All messages will share the custom and system properties defined on the <strong>Message</strong> and <strong>Sender</strong> tabs. </li>
</ul>
<div style="text-align: center;"><img id="143409" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143409/1/messagefiles.png" alt="" width="800" /></div>
<ul>
<li><strong>New configuration settings and Options form</strong>: the following settings have been introduced in the Options form and in the configuration file. In addition, the <strong>Options</strong> form now provides the ability to persist settings to the configuration file by clicking the <strong>Save</strong> button. 
<ul>
<li>Monitor Refresh Interval </li>
<li>Subscription Id </li>
<li>Management Certificate Thumbprint </li>
<li>Message Path </li>
<li>Message Text </li>
<li>Sender Think Time </li>
<li>Receive Think Time </li>
</ul>
</li>
</ul>
<div style="text-align: center;"><img id="143410" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143410/1/optionsform.png" alt="" width="588" height="540" /></div>
<ul>
<li><strong>New About form</strong>: you can access my email address and twitter account in the new about form. </li>
</ul>
<div style="text-align: center;"><img id="143411" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143411/1/aboutform.png" alt="" width="652" height="532" /></div>
<ul>
<li><strong>Auto Delete On Idle</strong>: the Service Bus 2.0 introduced for queues, topics and subscriptions the new TimeSpan property <strong>AutoDeleteOnIdle</strong>&nbsp;that defines the TimeSpan idle interval after which the topic is automatically deleted. The minimum duration is 5 minutes. The Service Bus Explorer 2.0 has been extended to support this property. </li>
</ul>
<div style="text-align: center;"><img id="143412" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143412/1/autodelete2.png" alt="" width="800" /></div>
<ul>
<li><strong>Minor changes</strong> 
<ul>
<li>Added check to make sure that non-numeric values can be entered in textboxes that expect a number </li>
<li>Added links to blog, email and twitter in the About form </li>
<li>Changed the CreateFileName method of the MainForm class that is invoked when exporting entities </li>
<li>Changed Session Timeout with Server Timeout in test queue/topic/subscription controls </li>
<li>Deleted Expand and Collapse node from queue node context menu. </li>
<li>Fixed a problem with in the ForwardTo form. </li>
<li>The message text in the test queue, topic and relay service controls is now indented when is Xml. </li>
<li>Added a border to flat comboboxes. </li>
<li>Changed look &amp; feel of forms </li>
<li>Dispose dialogs and forms after use. </li>
<li>Changed Docking properties of chart Legends: moved from right to top. </li>
<li>Fixed problem with MaxSizeInMegabytes and ForwardTo properties when importing/exporting entities. </li>
</ul>
</li>
</ul>
<ul>
<li>&nbsp;<strong>Service Bus for Windows Server </strong>support: the Service Bus 2.0 temporarily interrupts the symmetry between the cloud and on-premises version of the Service Bus. In other words, the Microsoft.ServiceBus.dll 2.0 client is not compatible  with the Service Bus for Windows Server 1.0. For this reason, I included the old version of the Service Bus Explorer in a zip file called 1.8 which in turn is contained in the zip file of the current version. The old version of the Service Bus Explorer uses  the Microsoft.ServiceBus.dll 1.8 which is compatible with the Service Bus for Windows Server. </li>
</ul>
<div><strong>Update</strong>: 19 April 2013</div>
<div>This version introduces the following updates:</div>
<ul>
<li>Fixed display problems when Control Panel -&gt; Display -&gt; Change the size of all items is set to 125%. </li>
</ul>
<div>&nbsp;</div>
<div><strong>Update</strong>: 2 May 2013</div>
<div>This version introduces the following updates:</div>
<ul>
<li>RTM version of the Microsoft.ServiceBus.dll </li>
</ul>
<div><strong>Update</strong>:&nbsp;10 May 2013</div>
<div>This version introduces the following updates:</div>
<ul>
<li>Fixed a regression bug affecting the bindingTreeView control in the TestRelayServiceControl. </li>
</ul>
<p><strong>Update</strong>:&nbsp;30 September 2013</p>
<p>This version introduces the following updates:</p>
<ul>
<li>The <strong>Connect Form</strong>&nbsp;provides the possibility to choose the <a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.connectivitysettings.mode.aspx"> ConnectivityMode</a> and <a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.messagingfactorysettings.transporttype.aspx"> TransportType</a>: </li>
</ul>
<p style="text-align: center;"><img id="143413" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143413/1/connectivitymodetransporttype.png" alt="" width="452" height="746" /></p>
<ul>
<li>See the following resources for more information on these properties: 
<ul>
<li><a href="http://msdn.microsoft.com/en-us/library/windowsazure/ee706755.aspx">How to: Modify the Service Bus Connectivity Setting</a> </li>
<li><a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.servicebusenvironment.systemconnectivity.aspx">ServiceBusEnvironment.SystemConnectivity Property</a> </li>
<li><a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.connectivitysettings.mode.aspx">ConnectivitySettings.Mode Property</a> </li>
<li><a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.connectivitymode.aspx">ConnectivityMode Enumeration</a> </li>
<li><a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.messagingfactorysettings.transporttype.aspx">MessagingFactorySettings.TransportType Property</a> </li>
<li><a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.transporttype.aspx">TransportType Enumeration</a> </li>
</ul>
</li>
<li><strong>Important Note</strong>: when using <a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.messagingfactorysettings.transporttype.aspx"> TransportType</a> = Amqp, some features are not supported. So expect to see error messages such as "the method or operation is not supported": e.g. transactions and Batch-based APIs are not actually supported. For more information, please see the following  topic: 
<ul>
<li><a href="http://msdn.microsoft.com/en-us/library/windowsazure/jj841075.aspx">Using Service Bus from .NET with AMQP 1.0</a> </li>
</ul>
</li>
</ul>
<ul>
<li>The <strong>Options Form </strong>allows to select the default <a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.connectivitysettings.mode.aspx"> ConnectivityMode</a>. This information is saved in the <strong>appSettings </strong> section of the configuration file. </li>
</ul>
<p style="text-align: center;"><img id="143414" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143414/1/connectivitymodeoptionsform.png" alt="" width="610" height="522" /></p>
<ul>
<li>Added <a href="http://msdn.microsoft.com/en-us/library/system.windows.forms.listbox.selectionmode.aspx"> SelectionMode </a>= <a href="http://msdn.microsoft.com/en-us/library/system.windows.forms.selectionmode.aspx"> MultiExtended</a> to the Log listbox. This feature allows the user to&nbsp;select multiple lines and use the SHIFT, CTRL, and arrow keys to make selections. </li>
<li>Implemented copy &amp; paste of messages from the <strong>Log </strong>listbox (Ctrl + C). </li>
<li>Added a context menu to the <strong>Log </strong>listbox. </li>
</ul>
<p style="text-align: center;"><img id="143415" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143415/1/log.png" alt="" width="800" /></p>
<ul>
<li>The context menu allows the user to select the perform actions: &nbsp; 
<ul>
<li><strong>Copy All</strong>: copy all of the lines in the clipboard. </li>
<li><strong>Copy Selected</strong>: copy only the selected lines in the clipboard. </li>
<li><strong>Clear All</strong>: clear all of the lines in the log. </li>
<li>C<strong>lear Selected</strong>:&nbsp;clear only the selected lines in the log. </li>
<li><strong>Save all</strong>: save all of the lines in a text file. </li>
<li><strong>Save Selected</strong>: save only the selected lines in a text file. </li>
</ul>
</li>
</ul>
<ul>
<li>The <strong>Service Bus Explorer 2.1</strong> supports&nbsp;for <strong>Service Bus for Windows Server 1.1</strong>. In particular, this version introduces the possibility to visualize the information comtained in the <a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.messagecountdetails.aspx"> MessageCountDetails </a>property of a <a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.queuedescription_properties.aspx"> QueueDescription</a>, <a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.topicdescription.aspx"> TopicDescription</a>, <a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.subscriptiondescription.aspx"> SubscriptionDescription</a> object. </li>
</ul>
<p style="text-align: center;"><img id="143417" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143417/1/messagecountdetails.png" alt="" width="800" /></p>
<ul>
<li>This functionality allows to clone and send the selected messages to a the same or alternative queue or topic in the <strong>Service Bus </strong>namespace. If you want to edit the payload, system properties or user-defined properties, you have to select, edit and send messages one at a time. In order to do so, double click a message in the DataGridView or right click the  message and click <strong>Repair and Resubmit Selected Message</strong> from the context menu. This opens up the following dialog that allows to modify and resubmit the message or to save the payload to a text file. </li>
</ul>
<p style="padding-left: 30px; text-align: center;"><img id="143418" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143418/1/repairsinglemessage.png" alt="" width="800" /></p>
<p style="padding-left: 30px;"><strong>Important Note</strong>: the Service Bus does not allow to receive and delete a peeked <a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.brokeredmessage.aspx"> BrokeredMessage</a> by <a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.brokeredmessage.sequencenumber.aspx"> SequenceNumber</a>. Only deferred messages can by received by <a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.brokeredmessage.sequencenumber.aspx"> SequenceNumber</a>.&nbsp;As a consequence, when editing and resubmitting a peeked message, there's no way to receive and delete the original copy.</p>
<ul>
<li>The <strong>Service Bus Explorer 2.1</strong> introduces support for <a href="http://www.windowsazure.com/en-us/documentation/services/notification-hubs/"> Notification Hubs</a>. See the following resources for more information on this topic: 
<ul>
<li><a href="http://www.windowsazure.com/en-us/documentation/services/notification-hubs/">Notification Hubs</a> </li>
<li><a href="http://www.windowsazure.com/en-us/develop/net/how-to-guides/service-bus-notification-hubs/">What are Notification Hubs?</a> </li>
<li><a href="http://www.windowsazure.com/en-us/manage/services/notification-hubs/getting-started-windows-dotnet/">Getting Started with Notification Hubs</a> </li>
</ul>
</li>
<li>The <strong>Notification Hubs</strong> node under the namespace root node allows to manage notification hubs in defined in <strong>Windows Azue Service Bus </strong>namespace. </li>
</ul>
<p style="padding-left: 30px; text-align: center;"><img id="143419" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143419/1/notificationhubs.png" alt="" width="448" height="243" /></p>
<ul>
<li>The context menu allows to perform the following actions: 
<ul>
<li><strong>Create Notification Hub</strong>: create a new notification hub </li>
<li><strong>Delete Notification Hubs</strong>: delete all the notification hubs defined in the current namespace. </li>
<li><strong>Refresh Notification Hubs</strong>: refreshed the list of notification hubs. </li>
<li><strong>Export Notification Hubs</strong>: exports the definition of all the notification hubs to a XML file. </li>
<li><strong>Expand Subtree</strong>: expands the tree under Notification Hubs node. </li>
</ul>
<ul>
<li><strong>Collapse Subtree</strong>: collapse the tree under Notification Hubs node. </li>
</ul>
</li>
<li>The&nbsp;<strong>Create Notification Hub </strong>allows to define the path, credentials, and metadata for a new notification hub: </li>
</ul>
<p style="text-align: center;"><img id="143420" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143420/1/newhub.png" alt="" width="800" /></p>
<ul>
<li>If you click an existing notification hub, you can view and edit credentials and metadata: </li>
</ul>
<p style="text-align: center;"><img id="143421" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143421/1/notificationhub01.png" alt="" width="800" /></p>
<ul>
<li>The <strong>Authorization Rules</strong> tab allows to review or edit the<strong> Shared Access Policies</strong> for the selected notification hub. </li>
</ul>
<p style="text-align: center;"><img id="143422" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143422/1/authorizationrules.png" alt="" width="800" /></p>
<ul>
<li>The <strong>Registrations </strong>buttons opens a a dialof that allows the registrations to query: </li>
</ul>
<p style="text-align: center;"><img id="143423" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143423/1/getregistrations.png" alt="" width="450" height="226" /></p>
<ul>
<li>You can select one of the following options: 
<ul>
<li><strong>PNS Handle</strong>: this option allows to retrieve registrations by <strong> ChannelUri </strong>(Windows Phone 8 and Windows Store Apps registrations), <strong> DevieToken </strong>(Apple registrations), <strong>GcmRegistrationId </strong>(Google registrations) </li>
<li><strong>Tag</strong>: this options allows to find all the registrations sharing the specified tag. </li>
<li><strong>All</strong>: this options allows to receive <strong>n</strong> registrations where <strong>n</strong> is specified by the <strong>Top Count</strong> parameter. This value specifies also the page size. In fact, the tool supports registration data paging and allows to retrieve more data using the continuation mechanism. </li>
</ul>
</li>
<li>The <strong>Registrations </strong>tab allows to select one or multiple registrations from the <strong>DataGridView</strong>. </li>
</ul>
<p style="text-align: center;"><img id="143424" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143424/1/registrations.png" alt="" width="800" /></p>
<ul>
<li>The navigation control in the bottom of the registrations control allows to navigate through pages. </li>
<li>The <strong>DataGridView&nbsp;</strong>context menu provides access to the following actions: 
<ul>
<li><strong>Update Selected Registrations</strong>: update the selected registrations. </li>
<li><strong>Delete Selected Registrations</strong>: delete the selected registrations. </li>
</ul>
</li>
<li>The <strong>PropertyGrid </strong>on the right-hand side allows to edit the properties (e.g. <strong>Tags</strong> or <strong>BodyTemplate</strong>) of an existing registration. </li>
<li>The <strong>Create </strong>button allows to create a new registration. Select the registration type from the dropdownlist and enter mandatory and optional (e.g. <strong>Tags</strong>, <strong>Headers</strong>) information and click the <strong> Ok</strong> button to confirm. </li>
</ul>
<p style="text-align: center;"><img id="143425" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143425/1/createnewregistration.png" alt="" width="482" height="354" /></p>
<ul>
<li>The <strong>Template </strong>tab allows to send template notifications: 
<ul>
<li>The <strong>Notification Payload</strong> read-only texbox shows the payload in JSON format. </li>
<li>The <strong>Notification Properties</strong> datagridview allows to define the template properties.&nbsp; </li>
<li>The&nbsp;<strong>Notification Tags&nbsp;</strong>datagridview allows to define one or multiple tags. A separate notification is sent for each tag. </li>
<li>The <strong>Additional Headers </strong>datagridview allows to define additional custom headers for the notification. </li>
</ul>
</li>
</ul>
<p style="text-align: center;"><img id="143426" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143426/1/template.png" alt="" width="800" /></p>
<ul>
<li>The&nbsp;<strong>Windows Phone&nbsp;</strong>tab allows to send native notifications to <strong>Windows Phone 8</strong> devices. 
<ul>
<li>The&nbsp;<strong>Notification Payload</strong>&nbsp;texbox shows the payload in JSON format. When the <strong>Manual </strong>option is selected in the dropdownmenu under <strong>Notification Template</strong>, you can edit or paste the payload direcly in the control. When any of the other options (<strong>Tile</strong>, <strong>Toast</strong>, <strong>Raw</strong>) is selected, this field is read-only. </li>
<li>The&nbsp;<strong>Notification Template </strong>dropdownlist allows to select between the following types of notification: 
<ul>
<li>Manual </li>
<li>Toast </li>
<li>Tile </li>
<li>Raw </li>
</ul>
</li>
<li>When <strong>Toast</strong>,&nbsp;<strong>Tile</strong>, or&nbsp;<strong>Raw</strong>&nbsp;is selected, the datagridview under the <strong>Notification Template </strong>section&nbsp;allows to define the properties for the notification, as shown in the figures below. </li>
<li>The&nbsp;<strong>Notification Tags&nbsp;</strong>datagridview allows to define one or multiple tags. A separate notification is sent for each tag. </li>
<li>The&nbsp;<strong>Additional Headers&nbsp;</strong>datagridview allows to define additional custom headers for the notification. </li>
</ul>
</li>
</ul>
<p style="text-align: center;"><img id="143427" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143427/1/windowsphone.png" alt="" width="800" /></p>
<p style="text-align: center;"><img id="143428" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143428/1/windowsphonetile.png" alt="" width="800" /></p>
<ul>
<li>The&nbsp;<strong>Windows </strong>tab allows to send native notifications to&nbsp;<strong>Windows Store Apps</strong>&nbsp;running on<strong> Windows 8</strong> and <strong>Windows 8.1</strong>. 
<ul>
<li>The&nbsp;<strong>Notification Payload</strong>&nbsp;texbox shows the payload in JSON format. When the <strong>Manual </strong>option is selected in the dropdownmenu under&nbsp;<strong>Notification Template</strong>, you can edit or paste the payload direcly in the control. When any of the other options (<strong>Tile</strong>,&nbsp;<strong>Toast</strong>,&nbsp;<strong>Raw</strong>)  is selected, this field is read-only. </li>
<li>The&nbsp;<strong>Notification Template&nbsp;</strong>dropdownlist allows to select between the following types of notification: 
<ul>
<li>Manual </li>
<li>Toast templates. For more information, see <a href="http://msdn.microsoft.com/en-us/library/windows/apps/hh761494.aspx"> The toast template catalog (Windows store apps)</a>. </li>
<li>Tile templates. For more information, see <a href="http://msdn.microsoft.com/en-us/library/windows/apps/hh761491.aspx"> The tile template catalog (Windows store apps)</a>.&nbsp; </li>
</ul>
</li>
<li>When&nbsp;<strong>Toast</strong>,&nbsp;<strong>Tile</strong>, or&nbsp;<strong>Raw</strong>&nbsp;is selected, the datagridview under the&nbsp;<strong>Notification Template</strong>section&nbsp;allows to define the properties for the notification, as shown  in the figures below. </li>
<li>The&nbsp;<strong>Notification Tags&nbsp;</strong>datagridview allows to define one or multiple tags. A separate notification is sent for each tag. </li>
<li>The&nbsp;<strong>Additional Headers&nbsp;</strong>datagridview allows to define additional custom headers for the notification. </li>
</ul>
</li>
</ul>
<p style="text-align: center;"><img id="143429" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143429/1/windows2.png" alt="" width="800" /></p>
<p style="text-align: center;"><img id="143430" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143430/1/windows.png" alt="" width="800" /></p>
<ul>
<li>The&nbsp;<strong>Apple&nbsp;</strong>and&nbsp;<strong>Google&nbsp;</strong>tabs provides the ability to send, respectively,&nbsp;<strong>Apple&nbsp;</strong>and&nbsp;<strong>Gcm&nbsp;</strong>native notifications. For brevity, I omit the description of  the <strong>Apple </strong>tab as it works the same way as the <strong>Google</strong> one. 
<ul>
<li>&nbsp; The&nbsp;<strong>Json Payload</strong>&nbsp;texbox allows to enter the payload in JSON format. </li>
<li>The&nbsp;<strong>Notification Tags&nbsp;</strong>datagridview allows to define one or multiple tags. A separate notification is sent for each tag. </li>
<li>The&nbsp;<strong>Additional Headers&nbsp;</strong>datagridview allows to define additional custom headers for the notification. </li>
</ul>
</li>
</ul>
<p style="text-align: center;"><img id="143431" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143431/1/gcm.png" alt="" width="800" /></p>
<ul>
<li>Added the possibility to select and resubmit multiple messages in a batch mode from the <strong>Messages</strong> and <strong>Deadletter</strong> tabs of queues and subscriptions. It's sufficient to select messages in the <strong>DataGridView</strong> as shown in the following picture, right click to show the context menu and choose <strong>Resubmit Selected Messages in Batch Mode</strong>. </li>
</ul>
<p style="text-align: center;"><img id="143433" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143433/1/resubmitmultiplemessages.png" alt="" width="800" /></p>
<ul>
<li><strong>Minor changes</strong> 
<ul>
<li>Fixed code of the <strong>Click </strong>event handler for the&nbsp;<strong>Default </strong>button in the <strong>Options Form</strong>.<strong><br /> </strong></li>
<li>Replaced the <a href="http://msdn.microsoft.com/en-us/library/system.runtime.serialization.json.datacontractjsonserializer.aspx"> DataContractJsonSerializer</a> with the <a href="http://msdn.microsoft.com/en-us/library/system.web.script.serialization.javascriptserializer.aspx"> JavaScriptSerializer</a> class in the <strong>JsonSerializerHelper</strong> class. </li>
<li>Fixed a problem when reading <strong>Metrics</strong>&nbsp;data from the&nbsp;RESTul services exposed by&nbsp;a <strong>Windows Azure Service Bus</strong> namespace. </li>
<li>Changes the look and feel of messages in the <strong>Messages </strong>and <strong> Deadletter </strong>tabs of queues and subscriptions. </li>
<li>Introduced indent formatting when showing and editing <strong>XML </strong>messages. </li>
</ul>
</li>
</ul>
<p><strong>Update</strong>: 9 October 2013</p>
<p>This version introduces the following updates:</p>
<ul>
<li>Fixed a bug when accessing a notification hub with no registrations. </li>
<li>Fixed a bug that prevented the tool to connect to a Windows Azure Service Bus namespace when using a SAS connection string. </li>
</ul>
<p><strong>Update</strong>: 14 October 2013</p>
<p>This version introduces the following updates:</p>
<ul>
<li>Fixed a problem affecting Import functionality. When importing a subscription with no default rule ($Default), the code erroneously created the subscription with a default rule. The problem is now fixed. </li>
</ul>
<p><strong>Update</strong>: 29 November 2013</p>
<p>This version introduces the following updates for both the 2.1 and 2.2 version:</p>
<ul>
<li>Added support to read the body of a WCF message when the payload is in JSON format. </li>
<li>Added support to send the body of a WCF message when the payload is in JSON format. </li>
<li>Implemented the possibility to pass command line arguments for both the 2.1 and 2.2 version: </li>
</ul>
<p style="padding-left: 60px;">ServiceBusExplorer.exe&nbsp; [-c|/c] [connectionstring]<br /> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; [-q|/q] [queue odata filter expression]<br /> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; [-t|/t] [topic odata filter expression]<br /> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; [-s|/s] [subscription odata filter expression]</p>
<p>&nbsp;</p>
<p style="padding-left: 60px;">ServiceBusExplorer.exe&nbsp; [-n|/n] [namespace key in the configuration file]<br /> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; [-q|/q] [queue odata filter expression]<br /> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; [-t|/t] [topic odata filter expression]<br /> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; [-s|/s] [subscription odata filter expression]</p>
<p style="padding-left: 60px;"><strong>Example</strong>: ServiceBusExplorer.exe -n paolosalvatori -q "Startswith(Path, 'request') Eq true" -t "Startswith(Path, 'request') Eq true"</p>
<ul>
<li>Improved check when settings properties for Topics and Subscriptions. </li>
<li>Fixed an error that added columns to message and deadletter datagridview every time the Update button was pressed.Fixed a error on CellDoubleClick for messages and deadletter datagridview that happened when double clicking a header cell.Improved the visualization  of sessions and added the possibility to sort sessions by column. </li>
<li>Added sorting capability to messages and deadletter messages datagridview for queues and subscriptions.&nbsp;Click the column header to sort rows by the corresponfing property value in ASC or DESC order. </li>
</ul>
<p style="text-align: center;"><img id="143434" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143434/1/messages.png" alt="" width="800" /></p>
<ul>
<li>Added sorting capability to sessions datagridview for queues and subscriptions.&nbsp;Click the column header to sort rows by the corresponfing property value in ASC or DESC order. </li>
</ul>
<p style="text-align: center;"><img id="143435" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143435/1/sessions.png" alt="" width="800" /></p>
<ul>
<li>Added sorting capability to registrations datagridview for notification hubs.&nbsp;Click the column header to sort rows by the corresponfing property value in ASC or DESC order. </li>
</ul>
<p style="text-align: center;"><img id="143436" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143436/1/registrations3.png" alt="" width="800" /></p>
<ul>
<li>Introduced the possibility to define filter expression for peeked/received messages/deadletter messages. Click the button highlighted in the picture below to open a dialog and define a filtter expression using a SQL Expression (e.g. sys.Size &gt; 300 and  sys.Label='Service Bus Explorer' and City='Pisa'). For more information, see&nbsp;<a href="http://msdn.microsoft.com/en-us/library/windowsazure/microsoft.servicebus.messaging.sqlfilter.sqlexpression.aspx">SqlFilter.SqlExpression Property</a>. </li>
</ul>
<p style="text-align: center;"><img id="143437" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143437/1/messages2.png" alt="" width="800" /></p>
<ul>
<li>Introduced the possibility to define filter expression for peeked/received messages/deadletter messages. Click the button highlighted in the picture below to open a dialog and define a filtter expression using a SQL Expression on public and n on public  properties of RegistrationDescription class (e.g.&nbsp;PlatformType contains 'windows' and ExpirationTime &gt; '2014-2-5' and TagsString contains 'productservice'). The filter engine supports the following predicates: 
<ul>
<li>= </li>
<li>!= </li>
<li>&gt; </li>
<li>&gt;= </li>
<li>&lt; </li>
<li>&lt;= </li>
<li>StartsWith </li>
<li>EndsWith </li>
<li>Contains </li>
</ul>
</li>
</ul>
<p style="text-align: center;"><img id="143438" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143438/1/registrations4.png" alt="" width="800" /></p>
<ul>
</ul>
<ul>
<li>Introduced support for TagExpressions introduced by Service Bus 2.2. When sending a notification, you can select the Tag Expression or Notification Tags to define, respectively, a tag expression (e.g. productservice &amp;&amp; (Italy || UK)) or a list of  tags. This feature is available only in the Service Bus Explorer 2.2. </li>
</ul>
<p style="text-align: center;"><img id="143439" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143439/1/tagexpression.png" alt="" width="800" /></p>
<ul>
<li>Introduced support for partitioned queues. For more information on partitioned entities, read<a href="http://blogs.msdn.com/b/windowsazure/archive/2013/10/29/partitioned-service-bus-queues-and-topics.aspx">Partitioned Service Bus Queues and Topics</a>.&nbsp;This  feature is available only in the Service Bus Explorer 2.2. </li>
</ul>
<p style="text-align: center;"><img id="143440" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143440/1/partitionedqueue.png" alt="" width="800" /></p>
<ul>
<li>Introduced support for partitioned topics. For more information on partitioned entities, read <a href="http://blogs.msdn.com/b/windowsazure/archive/2013/10/29/partitioned-service-bus-queues-and-topics.aspx"> Partitioned Service Bus Queues and Topics</a>.&nbsp;This feature is available only in the Service Bus Explorer 2.2. </li>
</ul>
<p style="text-align: center;"><img id="143441" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143441/1/partitionedtopic.png" alt="" width="800" /></p>
<p><strong>Update</strong>: 2 December 2013</p>
<p>This version introduces the following updates:</p>
<ul>
<li>Support for the&nbsp;<strong>Microsoft.ServiceBus.ConnectionString</strong> appSetting. The connection string defined in this setting will be added to the list of connection strings in the corresponding drop-down-list in the <strong>Connect </strong>form.&nbsp; </li>
</ul>
<p style="text-align: center;"><img id="143442" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143442/1/microsoft.servicebus.connectionstring.png" alt="" width="452" height="746" /></p>
<p><strong>Update</strong>: 18 December 2013</p>
<p>This version introduces the following updates:</p>
<ul>
<li>Introduced a new command to start one or more listeners for a given queue or subscription. Right click the queue or subscription and select, respectively, <strong>Create Queue Listerner</strong> or <strong>Create Subscription Listener</strong> from the context menu as shown in the following picture. </li>
</ul>
<p style="text-align: center;"><img id="143443" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143443/1/listener04.png" alt="" width="800" /></p>
<ul>
<li>In the listener dialog you can set the following properties: <ol>
<li><a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.onmessageoptions.maxconcurrentcalls.aspx">MaxConcurrentCalls</a>: gets or sets the maximum number of concurrent calls to the callback the message pump should initiate. </li>
<li>Refresh Interval (sec): sets a refresh interval in seconds for queue or subscription message count information. </li>
<li>Graph: enable or disable the graph. </li>
<li><a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.onmessageoptions.autocomplete.aspx">AutoComplete</a>: gets or sets a value that indicates whether the message-pump should call&nbsp;<a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.queueclient.complete.aspx">Complete(Guid)</a>&nbsp;or&nbsp;<a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.subscriptionclient.complete.aspx">Complete(Guid)</a>&nbsp;on  messages after the callback has completed processing </li>
<li>Tracking: enable or disable message tracking. When enabled, you can use the Messages tab to access messages. </li>
<li>Logging: enable or disable logging. </li>
<li>Verbose: enable or disable verbose mode when logging is enabled. </li>
</ol> </li>
</ul>
<p style="text-align: center;"><img id="143444" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143444/1/listener05.png" alt="" width="800" /></p>
<ul>
<li>Press the <strong>Start </strong>button to start the listener.&nbsp; </li>
</ul>
<p style="text-align: center;"><img id="143445" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143445/1/listener01.png" alt="" width="800" /></p>
<ul>
<li>The <strong>Start </strong>button turns into the <strong>Stop </strong>button.&nbsp; </li>
<li>Press the <strong>Stop </strong>button to stop the listener.&nbsp; </li>
<li>Press the <strong>Close </strong>the listener dialog. If the listener is open, this operation closes the listener. </li>
<li>Press the <strong>Clear</strong> button to clear tracked messages and log content. </li>
<li>Press the <strong>Messages</strong> tab to access messages.&nbsp; </li>
<li>You can click s message row to access its payload, custom and system properties. </li>
</ul>
<p style="text-align: center;"><img id="143446" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143446/1/listener02.png" alt="" width="800" /></p>
<ul>
<li>Click the the magnifying glass button&nbsp;to define a filter expression for received messages using a SQL Expression (e.g. sys.Size &gt; 300 and sys.Label='Service Bus Explorer' and City='Pisa'). For more information, see&nbsp;<a href="http://msdn.microsoft.com/en-us/library/windowsazure/microsoft.servicebus.messaging.sqlfilter.sqlexpression.aspx">SqlFilter.SqlExpression  Property</a>. </li>
</ul>
<p style="text-align: center;"><img id="143447" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143447/1/listener06.png" alt="" width="800" /></p>
<ul>
<li style="text-align: justify;">Double click a row or click <strong>Repair and Resubmit Message</strong> from the context menu to open a message in the a separate dialog. This functionality allows to clone and send the selected messages to a the same or alternative  queue or topic in the&nbsp;<strong>Service Bus&nbsp;</strong>namespace. If you want to edit the payload, system properties or user-defined properties, you have to select, edit and send messages one at a time. </li>
</ul>
<p style="text-align: center;"><img id="143448" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143448/1/listener03.png" alt="" width="800" /></p>
<ul>
<li>Minor changes: 
<ul>
<li>Fixed ListView control visualization when the vertical bar is visible </li>
<li>Increased ReaderQuotas when reading WCF messages. This adds support for large messages. </li>
</ul>
</li>
</ul>
<p><strong>Update</strong>: 10 February 2014</p>
<p>This version introduces the following updates:</p>
<ul>
<li>Johann Cooper (@JohannCooper) extended the Import/Export functionality to support SAS rules for Windows Server Service Bus. I included the code in the 2.1 and 2.2 versions of the tool. </li>
</ul>
<p><strong>Update</strong>: 21 May 2014</p>
<p>This version introduces the following updates:</p>
<ul>
<li>Improved support for <strong>AMQP </strong>transport protocol in the <strong> Connect Form</strong>. For example, when using the Service Bus Explorer to connect to an on-premises namespace, if you select AMQP as Transport Type in the Connect Form, the tool automatically changes the value of the&nbsp;TransportType and RuntimePort parameters: 
<ul>
<li><strong>NetMessaging</strong>:&nbsp;RuntimePort=9354;TransportType=NetMessaging </li>
<li><strong>AMQP</strong>:&nbsp;RuntimePort=5671;TransportType=Amqp </li>
</ul>
</li>
<li>Added support for the <strong>stsEndpoint </strong>parameter in the connection string for cloud namespaces. This feature was specifically requested by the Service Bus team. </li>
<li>The tool can now use the&nbsp;<strong>AMQP&nbsp;</strong>transport protocol&nbsp;to read messages from queues, subscriptions and deadletter queues. </li>
<li>Added full support to namespace-level <strong>SAS connection strings</strong>, both in the configuration file and <strong>Connect Form</strong>. </li>
<li>Changed Receive label into Receive and Delete in the ReceiveModeForm to make it clear that the receive operation deletes messages from the underlying <strong>queue</strong>, <strong>subscription</strong> or <strong>deadletter queue</strong>. </li>
<li>Fixed a bug when reading messages from the deadletter queue of a queue or subscription. </li>
<li>Fixed a bug in the <strong>SelectEntityForm</strong>: replaced <strong>TreeView.TopNode</strong> with <strong>TreeView.Nodes[0]</strong> </li>
<li>Implemented support for the new&nbsp;<a href="http://msdn.microsoft.com/en-us/library/azure/microsoft.servicebus.messaging.queuedescription.forwarddeadletteredmessagesto.aspx">ForwardDeadLetteredMessagesTo</a> property of the <a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.queuedescription.aspx"> QueueDescription</a> and&nbsp;<a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.subscriptiondescription.aspx">SubscriptionDescription</a>&nbsp;classes. </li>
</ul>
<ul>
<li>Implemented batching support in the listener for queues and subscriptions. You can now specify a value for the <a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.messagereceiver.prefetchcount.aspx"> PrefetchCount</a> property used by the <a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.messagereceiver.aspx"> MessageReceiver</a> object used by the listener to prefetch multiple messages from a queue or subscription. This greatly improves the overall performance of the listener. Now you can also specify the value for the <a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.messagereceiver.mode.aspx"> Mode</a>&nbsp;property of the the&nbsp;<a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.messagereceiver.aspx">MessageReceiver</a>&nbsp;object used by the listener. </li>
</ul>
<ul>
<li>Added the possibility to create a listener for <strong>session-aware queues</strong> and <strong>subscriptions </strong>(<a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.queuedescription.requiressession.aspx">RequiresSession</a> = true). The tool uses the&nbsp;<a href="http://msdn.microsoft.com/en-us/library/dn642578.aspx">RegisterSessionHandlerFactoryAsync</a>&nbsp;method  of a <a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.queueclient.aspx"> QueueClient</a> or <a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.subscriptionclient.aspx"> SubscriptionClient</a> object to register an asynchronous Session Handler Factory. For more information, see&nbsp;<a href="http://msdn.microsoft.com/en-us/library/dn632585.aspx">What's New in the Azure SDK 2.3 Release (April 2014)</a>. </li>
</ul>
<ul>
<li>Added the possibility to visualize the number of active and deadletter messages directly in the main treeview (see 1 and 2 in the figure below). The first number displays the valut of the&nbsp;<a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.messagecountdetails.activemessagecount.aspx">MessageCountDetails.ActiveMessageCount</a>&nbsp;property,  while the second number displays the value of the <a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.messagecountdetails.deadlettermessagecount.aspx"> MessageCountDetails.DeadletterMessageCount</a> property.&nbsp; </li>
</ul>
<p style="padding-left: 30px;">To enable or disable this feature, you can use the&nbsp;<strong>showMessageCount</strong> setting in the configuration file, or use the new <strong>Show Message Count </strong>checkbox in the <strong>Options Form </strong> as shown in the picture below.</p>
<ul>
<li>As shown in the figure below, numbers in the property list of queues, topics, subscriptions, and notification hubs are now formatted to make it easier to read their value. </li>
</ul>
<ul>
<li>Replaced the logo to reflect the recent change of name from <strong>Windows Azure</strong> to <strong>Microsoft Azure</strong>. </li>
</ul>
<p>&nbsp;</p>
<p><strong>Update</strong>: 20 June 2014</p>
<p>This version introduces the following updates:</p>
<ul>
<li>Added license.txt file containing the license for the Service Bus Explorer tool </li>
<li>Updated the Microsoft.ServiceBus.dll from version 2.3.0.0 to 2.3.2.0 </li>
</ul>
<p>&nbsp;</p>
<p><strong>Update</strong>: 18 July 2014</p>
<p>This version introduces the following updates:</p>
<ul>
<li>Connect Form now allows to select the type of entities that you want to load and manage in the selected namespace. </li>
</ul>
<p style="text-align: center;">&nbsp;</p>
<ul>
<li>You can specify a default value of the entities that you want to manage in the <strong>selectedEntities</strong> appSettings in the configuration file: </li>
</ul>
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
<ul>
<li>You can also set default selected entities in the Options dialog. </li>
</ul>
<ul>
<li><strong>Event Hubs</strong>: this version of the tool introduces the support for a new entity called <strong>Event Hub</strong>&nbsp;and related entities, <strong>Consumer Groups</strong> and <strong>Partitions</strong>. </li>
</ul>
<ul>
<li>The tool allows to perform the following actions on an <strong>Event Hub</strong>: 
<ul>
<li>Create Event Hub </li>
<li>Read Event Hub </li>
<li>Update Event Hub </li>
<li>Delete Event Hub </li>
<li>Disable / Enable Event Hub </li>
<li>Refresh Event Hub </li>
<li>Copy Event Hub Url to Clipboard </li>
<li>Export / Import event hub to / from XML file </li>
<li>Send &nbsp;Event Data to an Event Hubs (more on this below) </li>
</ul>
</li>
</ul>
<ul>
<li>The tool allows to manage the <strong>Consumer Groups</strong> of a given <strong> Event Hub</strong>: </li>
<li>Create Consumer Group 
<ul>
<li>Read Consumer Group </li>
<li>Update Consumer Group </li>
<li>Delete Consumer Group </li>
<li>Enable / Disable Consumer Group </li>
<li>Refresh Consumer Group </li>
<li>Copy Consumer Group Url to Clipboard </li>
<li>Create a Listener for the Consumer Group. A receiver is created for each Partition of the Event Hub. </li>
</ul>
</li>
</ul>
<ul>
<li>The tool allows to manage the <strong>Partitions </strong>of a given <strong> Event Hub</strong>. Note: <strong>Partitions </strong>are located under <strong>Consumer Groups</strong> in the hierarchy of the entity TreeView: 
<ul>
<li>Read Partition </li>
<li>Refresh Partition </li>
<li>Copy Partition Url to Clipboard </li>
<li>Create a Listener for the Partition. A receiver is created only for the selected Partition under the Consumer Group. </li>
</ul>
</li>
</ul>
<ul>
<li>Added support for <strong>Notification Hubs</strong> and <strong>Event Hubs</strong> to entity <strong>Import &nbsp;&amp; Export</strong> functionalities. You can now import / export the definition of Notification Hubs and Event Hubs from / to XML file. </li>
<li>The JSON serializer helper class is now based on <strong>Newtonsoft Json.Net</strong> library. </li>
<li><span style="font-size: 10px;">Added support for the new </span><strong style="font-size: 10px;">BrokeredMessage.ForcePersistence</strong><span style="font-size: 10px;"> property to the testing UI of queues and &nbsp;topics.</span> </li>
</ul>
<ul>
<li><strong>File Templates</strong>: This version introduces the possibility to send binary files or messages based on JSON or XML Templates. This feature is available for queues, topics and event hubs. </li>
<li>For example, when you select <strong>Send Messages </strong>menu item from the context menu of a queue and you select the <strong>Files</strong> tab, now you have 4 options available: </li>
</ul>
<p><strong>Options</strong>:</p>
<ul>
<li><strong>Text File</strong>: this option allows to select one or more text files. These files specify the payload of the outgoing messages (BrokeredMessage for queues and topics, EventData for event hubs), while the custom properties are defined in the grid  under the Message Properties control group. </li>
<li><strong>Binary File</strong>: this option allows to select one or more binary files. These files specify the payload of the outgoing messages (BrokeredMessage for queues and topics, EventData for event hubs), while the custom properties are defined in the  grid under the Message Properties control group. </li>
<li><strong>Json Template</strong>: this option allows to select one or more Json templates. Each template defines the payload for a BrokeredMessage or EventData and the value of the system and the custom properties: </li>
</ul>
<p><strong>BrokeredMessage Json Template</strong>:</p>
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
<p><strong>EventData Json Template</strong>:</p>
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
<ul>
<li><strong>Xml Template</strong>: this option allows to select one or more Xml templates. Each template defines the payload for a BrokeredMessage or EventData and the value of the system and the custom properties. Note: the payload is contained in CDATA section. </li>
</ul>
<ul>
</ul>
<p><strong>BrokeredMessage Xml Template</strong>:</p>
<p>&nbsp;</p>
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
<p><strong>EventData Xml Template</strong>:</p>
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
<ul>
<li><strong>Note</strong>: both Json and Xml templates for <strong>BrokeredMessage</strong> objects are defined by the <strong>BrokeredMessageTemplate</strong> class. Likewise, both Json and Xml templates for <strong>EventData</strong> objects are defined by the <strong>EventaDataTemplate</strong> class. </li>
<li><strong>Note</strong>: when using Files, the number of messages to send is still determined by the value of the <strong>Message Count</strong> textbox under the <strong>Sender</strong> tab. If the number of messages to send is higher than the number of selected files or templates, these are treated as a circular list. </li>
</ul>
<ul>
<li><strong>Brokered Message Generators: </strong>this release introduces the possibility to extend the tool with extension components. In particular, BrokeredMessage generators are components that allows to create a configurable amount of messages to send  to a queue or topic. </li>
<li>These components needs to implement the <strong>IBrokeredMessageGenerator</strong> interface defined by the tool: </li>
</ul>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">namespace Microsoft.Azure.ServiceBusExplorer
{
    public interface IBrokeredMessageGenerator
    {
        IEnumerable&lt;BrokeredMessage&gt; GenerateBrokeredMessageCollection(int messageCount,
                                                                       WriteToLogDelegate writeToLog = null);
    }
}</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">namespace</span>&nbsp;Microsoft.Azure.ServiceBusExplorer&nbsp;
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
<ul>
<li>&nbsp;The dll containing these components needs to be copied in the directory containing the tool. Finally, you need to specify the fully qualified name of the class in the <strong>brokeredMessageGenerators</strong> section of the configuration file as shown below: </li>
</ul>
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
         value="Microsoft.Azure.ServiceBusExplorer.OnOffDeviceBrokeredMessageGenerator,ServiceBusExplorer" /&gt;
    &lt;add key="ThresholdDeviceBrokeredMessageGenerator" 
         value="Microsoft.Azure.ServiceBusExplorer.ThresholdDeviceBrokeredMessageGenerator,ServiceBusExplorer" /&gt;
  &lt;/brokeredMessageGenerators&gt;
  ...
&lt;/configuration&gt;</pre>
<div class="preview">
<pre class="xml"><span class="xml__tag_start">&lt;?xml</span>&nbsp;<span class="xml__attr_name">version</span>=<span class="xml__attr_value">"1.0"</span>&nbsp;<span class="xml__attr_name">encoding</span>=<span class="xml__attr_value">"utf-8"</span><span class="xml__tag_start">?&gt;</span>&nbsp;
<span class="xml__tag_start">&lt;configuration</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;...&nbsp;
&nbsp;&nbsp;<span class="xml__tag_start">&lt;brokeredMessageGenerators</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;add</span>&nbsp;<span class="xml__attr_name">key</span>=<span class="xml__attr_value">"OnOffDeviceBrokeredMessageGenerator"</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__attr_name">value</span>=<span class="xml__attr_value">"Microsoft.Azure.ServiceBusExplorer.OnOffDeviceBrokeredMessageGenerator,ServiceBusExplorer"</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;add</span>&nbsp;<span class="xml__attr_name">key</span>=<span class="xml__attr_value">"ThresholdDeviceBrokeredMessageGenerator"</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__attr_name">value</span>=<span class="xml__attr_value">"Microsoft.Azure.ServiceBusExplorer.ThresholdDeviceBrokeredMessageGenerator,ServiceBusExplorer"</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;<span class="xml__tag_end">&lt;/brokeredMessageGenerators&gt;</span>&nbsp;
&nbsp;&nbsp;...&nbsp;
<span class="xml__tag_end">&lt;/configuration&gt;</span></pre>
</div>
</div>
</div>
<ul>
<li>&nbsp;The tool provides two brokered message generators out of the box: 
<ul>
<li><strong>OnOffDeviceBrokeredMessageGenerator</strong>: this components allows to simulate signals coming from a configurable amount of devices with can assume of two values: On (1) and Off (0). The component can be configured to send the payload in JSON  or XML format.&nbsp; </li>
</ul>
</li>
</ul>
<p style="padding-left: 90px;">Messages generated by the component have the following payload and properties.</p>
<p style="padding-left: 90px;">&nbsp;</p>
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
<ul>
<li><strong>ThresholdDeviceBrokeredMessageGenerator</strong>: this components allows to simulate signals coming from a configurable amount of devices with can assume values within a range defined by . The component can be configured to send the payload in JSON  or XML format. Messages generated by the component have the following payload and properties. </li>
</ul>
<p style="padding-left: 90px;">&nbsp;</p>
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
<ul>
<li>BrokeredMessage generators can be selected in the <strong>Send Messages</strong> dialog or in the <strong>Test queue / topic in SDI / MDI</strong> control as selecting the <strong> Generator</strong> tab as shown in the figure below: </li>
</ul>
<ul>
<li><strong>Note</strong>: when this option is selected, the properties defined in the grid under the Message Properties control group are ignored. </li>
</ul>
<ul>
<li><strong>Brokered Message Inspectors:</strong> the tool now allows to create a message inspector that can be used to intercept and eventually modify any outgoing or upcoming message sent to a queue or topic or received from a queue or a subscription. </li>
<li>These components needs to implement the <strong>IBrokeredMessageGenerator</strong> interface defined by the tool: </li>
</ul>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">namespace Microsoft.Azure.ServiceBusExplorer
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
<pre class="csharp"><span class="cs__keyword">namespace</span>&nbsp;Microsoft.Azure.ServiceBusExplorer&nbsp;
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
<ul>
<li>The dll containing these components needs to be copied in the directory containing the tool. Finally, you need to specify the fully qualified name of the class in the <strong>brokeredMessageGenerators</strong> section of the configuration file as shown below: </li>
</ul>
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
         value="Microsoft.Azure.ServiceBusExplorer.LogBrokeredMessageInspector,ServiceBusExplorer" /&gt;
    &lt;add key="ZipBrokeredMessageInspector" 
         value="Microsoft.Azure.ServiceBusExplorer.ZipBrokeredMessageInspector,ServiceBusExplorer" /&gt;
  &lt;/brokeredMessageInspectors&gt;
 ...
&lt;/configuration&gt;</pre>
<div class="preview">
<pre class="xml"><span class="xml__tag_start">&lt;?xml</span>&nbsp;<span class="xml__attr_name">version</span>=<span class="xml__attr_value">"1.0"</span>&nbsp;<span class="xml__attr_name">encoding</span>=<span class="xml__attr_value">"utf-8"</span><span class="xml__tag_start">?&gt;</span>&nbsp;
<span class="xml__tag_start">&lt;configuration</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;...&nbsp;
&nbsp;&nbsp;<span class="xml__tag_start">&lt;brokeredMessageInspectors</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;add</span>&nbsp;<span class="xml__attr_name">key</span>=<span class="xml__attr_value">"LogBrokeredMessageInspector"</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__attr_name">value</span>=<span class="xml__attr_value">"Microsoft.Azure.ServiceBusExplorer.LogBrokeredMessageInspector,ServiceBusExplorer"</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;add</span>&nbsp;<span class="xml__attr_name">key</span>=<span class="xml__attr_value">"ZipBrokeredMessageInspector"</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__attr_name">value</span>=<span class="xml__attr_value">"Microsoft.Azure.ServiceBusExplorer.ZipBrokeredMessageInspector,ServiceBusExplorer"</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;<span class="xml__tag_end">&lt;/brokeredMessageInspectors&gt;</span>&nbsp;
&nbsp;...&nbsp;
<span class="xml__tag_end">&lt;/configuration&gt;</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;The tool provides two BrokeredMessage generators out of the box:</div>
</div>
<ul>
<li><strong>ZipBrokeredMessageInspector</strong>: this component can be used to zip the body of outgoing messages sent to queue or topic and/or to unzip the content of messages received from a queue or subscription. </li>
<li><strong>LogBrokeredMessageInspector</strong>: this component can be used to log the body and custom properties of outgoing messages sent to queue or topic and/or incoming messages read from a queue or subscription. The log is saved in the same directory  of the tool. </li>
</ul>
<p>BrokeredMessage inspectors can be selected under the following dialogs:</p>
<ul>
<li>In the <strong>Sender</strong> tab<strong> </strong>of the <strong>Send Messages</strong> dialog </li>
</ul>
<ul>
<li>Under the <strong>Sender</strong> and <strong>Receiver</strong> tab in the <strong> Test queue / topic in SDI / MDI</strong> dialog: </li>
</ul>
<ul>
<li>In the <strong>Listener</strong> dialog for a queue or subscription. </li>
</ul>
<ul>
<li><strong>Note</strong>: when resubmitting multiple messages, it's not possible to select a brokered message inspector </li>
<li><strong>Listener Dialog for Queues and Subscriptions</strong>: the following features have been added to the Listener dialog for queues and subscriptions: 
<ul>
<li>The possibility to select a BrokeredMessage inspector (see point 1 in the figure below) </li>
<li>The Average Time performance counter (see point 3 in the figure below) </li>
<li>The KB/Sec performance counter (see point 4 in the figure below) </li>
<li>The possibility to select a scale factor (see points 2, 3 and 4 in the figure below) </li>
<li>messages in the listener forms are now tracked in an asynchronous way by a dedicated Task T. Receiver tasks just add messages to a BlockingCollection, while T reads messages out of the collection and writes them to the messages Grid control. </li>
</ul>
</li>
</ul>
<ul>
<li><span style="font-size: 10px;"><strong>EventData Generators</strong>:&nbsp;this release introduces the possibility to extend the tool with extension components. In particular, EventData generators are components that allows to create a configurable amount  of EventData objects to send to an event hub.</span> </li>
<li>These components needs to implement the <strong>IEventDataGenerator</strong> interface defined by the tool: </li>
</ul>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">namespace Microsoft.Azure.ServiceBusExplorer
{
    public interface IEventDataGenerator
    {
        IEnumerable&lt;EventData&gt; GenerateEventDataCollection(int eventDataCount, 
                                                           WriteToLogDelegate writeToLog = null);
    }
}</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">namespace</span>&nbsp;Microsoft.Azure.ServiceBusExplorer&nbsp;
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
<ul>
<li>The dll containing these components needs to be copied in the directory containing the tool. Finally, you need to specify the fully qualified name of the class in the <strong>eventDataGenerators</strong> section of the configuration file as shown below: </li>
</ul>
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
         value="Microsoft.Azure.ServiceBusExplorer.LogEventDataInspector,ServiceBusExplorer" /&gt;
    &lt;add key="ZipEventDataInspector" 
         value="Microsoft.Azure.ServiceBusExplorer.ZipEventDataInspector,ServiceBusExplorer" /&gt;
  &lt;/eventDataInspectors&gt;
  ...
&lt;/configuration&gt;</pre>
<div class="preview">
<pre class="xml"><span class="xml__tag_start">&lt;?xml</span>&nbsp;<span class="xml__attr_name">version</span>=<span class="xml__attr_value">"1.0"</span>&nbsp;<span class="xml__attr_name">encoding</span>=<span class="xml__attr_value">"utf-8"</span><span class="xml__tag_start">?&gt;</span>&nbsp;
<span class="xml__tag_start">&lt;configuration</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;...&nbsp;
&nbsp;&nbsp;<span class="xml__tag_start">&lt;eventDataInspectors</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;add</span>&nbsp;<span class="xml__attr_name">key</span>=<span class="xml__attr_value">"LogEventDataInspector"</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__attr_name">value</span>=<span class="xml__attr_value">"Microsoft.Azure.ServiceBusExplorer.LogEventDataInspector,ServiceBusExplorer"</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;add</span>&nbsp;<span class="xml__attr_name">key</span>=<span class="xml__attr_value">"ZipEventDataInspector"</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__attr_name">value</span>=<span class="xml__attr_value">"Microsoft.Azure.ServiceBusExplorer.ZipEventDataInspector,ServiceBusExplorer"</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;<span class="xml__tag_end">&lt;/eventDataInspectors&gt;</span>&nbsp;
&nbsp;&nbsp;...&nbsp;
<span class="xml__tag_end">&lt;/configuration&gt;</span></pre>
</div>
</div>
</div>
</div>
<ul>
<li>&nbsp;The tool provides two brokered message generators out of the box:&nbsp;<strong>OnOffDeviceEventDataGenerator</strong>: this components allows to simulate signals coming from a configurable amount of devices with can assume of two values: On (1) and  Off (0). The component can be configured to send the payload in JSON or XML format. Messages generated by the component have the following payload and properties. </li>
</ul>
<p style="padding-left: 90px;">&nbsp;</p>
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
<ul>
<li><strong>ThresholdDeviceEventDataGenerator</strong>: this components allows to simulate signals coming from a configurable amount of devices with can assume values within a range defined by . The component can be configured to send the payload in JSON or  XML format. Messages generated by the component have the following payload and properties. </li>
</ul>
<p style="padding-left: 90px;">&nbsp;</p>
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
<p>&nbsp;</p>
<ul>
<li>EventData generators can be selected in the <strong>Send Messages</strong> dialog or in the <strong>Test queue / topic in SDI / MDI</strong> control as selecting the <strong> Generator</strong> tab. </li>
</ul>
<ul>
<li><strong>Note</strong>: when this option is selected, the properties defined in the grid under the Message Properties control group are ignored. </li>
<li><strong>EventData Inspectors</strong>:&nbsp;this release of the tool now allows to create an EventData inspector that can be used to intercept and eventually modify any outgoing or upcoming EventData sent to an event hub or received from an event hub partition. </li>
<li>These components needs to implement the <strong>IEventDataGenerator</strong> interface defined by the tool: </li>
</ul>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">namespace Microsoft.Azure.ServiceBusExplorer
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
<pre class="csharp"><span class="cs__keyword">namespace</span>&nbsp;Microsoft.Azure.ServiceBusExplorer&nbsp;
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
<ul>
<li>The dll containing these components needs to be copied in the directory containing the tool. Finally, you need to specify the fully qualified name of the class in the <strong>eventDataGenerators</strong> section of the configuration file as shown below: </li>
</ul>
</div>
<p>&nbsp;</p>
<p>&nbsp;</p>
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
         value="Microsoft.Azure.ServiceBusExplorer.OnOffDeviceEventDataGenerator,ServiceBusExplorer" /&gt;
    &lt;add key="ThresholdDeviceEventDataGenerator" 
         value="Microsoft.Azure.ServiceBusExplorer.ThresholdDeviceEventDataGenerator,ServiceBusExplorer" /&gt;
  &lt;/eventDataGenerators&gt;
  ...
&lt;/configuration&gt;</pre>
<div class="preview">
<pre class="xml"><span class="xml__tag_start">&lt;?xml</span>&nbsp;<span class="xml__attr_name">version</span>=<span class="xml__attr_value">"1.0"</span>&nbsp;<span class="xml__attr_name">encoding</span>=<span class="xml__attr_value">"utf-8"</span><span class="xml__tag_start">?&gt;</span>&nbsp;
<span class="xml__tag_start">&lt;configuration</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;...&nbsp;
&nbsp;&nbsp;<span class="xml__tag_start">&lt;eventDataGenerators</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;add</span>&nbsp;<span class="xml__attr_name">key</span>=<span class="xml__attr_value">"OnOffDeviceEventDataGenerator"</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__attr_name">value</span>=<span class="xml__attr_value">"Microsoft.Azure.ServiceBusExplorer.OnOffDeviceEventDataGenerator,ServiceBusExplorer"</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;add</span>&nbsp;<span class="xml__attr_name">key</span>=<span class="xml__attr_value">"ThresholdDeviceEventDataGenerator"</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__attr_name">value</span>=<span class="xml__attr_value">"Microsoft.Azure.ServiceBusExplorer.ThresholdDeviceEventDataGenerator,ServiceBusExplorer"</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;<span class="xml__tag_end">&lt;/eventDataGenerators&gt;</span>&nbsp;
&nbsp;&nbsp;...&nbsp;
<span class="xml__tag_end">&lt;/configuration&gt;</span></pre>
</div>
</div>
</div>
<p>&nbsp;</p>
<ul>
<li>&nbsp;The tool provides two EventData generators out of the box: 
<ul>
<li><strong>ZipEventDataInspector</strong>: this component can be used to zip the body of outgoing event data objects sent to an event hub and/or to unzip the content of event data objects received from an event hub partition. </li>
<li><strong>LogEventDataInspector</strong>: this component can be used to log the body and custom properties of event data objects sent to an event hub and/or incoming event data objects read from an event hub partition. The log is saved in the same directory  of the tool. </li>
</ul>
</li>
</ul>
<p>EventData inspectors can be selected under the following dialogs:</p>
<ul>
<li>In the <strong>Sender</strong> tab<strong> </strong>of the <strong>Send Messages</strong> dialog </li>
</ul>
<ul>
<li>In the <strong>Listener</strong> dialog for a queue or subscription. </li>
</ul>
<ul>
<li><strong>Listener Dialog for Consumer Groups or individual Partitions</strong>:&nbsp;this release of the tool allows to create a listener to receive even data messages from all the partitions of an event hub or just from an single partition: 
<ul>
<li>The possibility to select an EventData inspector (see point 1 in the figure below) </li>
<li>The Listener dialog exposes the following performance counters: 
<ul>
<li>Events Total (see point 2 in the figure below) </li>
<li>Events/sec (see point 3 in the figure below) </li>
<li>The Average Time (see point 4 in the figure below) </li>
<li>The KB/Sec (see point 5 in the figure below) </li>
</ul>
</li>
<li>The possibility to select a scale factor (see points 2, 3 , 4, 5 in the figure below) </li>
<li>You can select a Partition from the dropdown list to see its information in the panel below (see point 6) </li>
<li>When message tracking is enabled using the corresponding checkbox, event data messages are saved in the grid available in the Events tab (see point 7) </li>
<li>In the Options panel you can specify options: 
<ul>
<li>Refresh Interval: the amount of time expressed in seconds after which partition information is refreshed from the server. </li>
<li>Receive Timeout: the timeout used by the ReceiveAsync method used by receivers, one for each partition. Note: when the Checkpoint option is enabled, a checkpoint is executed at every timeout. </li>
<li>Prefetch Count: defines a value for the PrefetchCount property of receivers. </li>
<li>Offset: specify the offset at which each receiver will start receiving event data from a partition. </li>
<li>Checkpoint Count: when Checkpoint option is enabled, specifies after how many event data each receiver performs a checkpoint. <strong>Note</strong>: in the current version this option is disabled because is not supported by the current version of the Microsoft.ServiceBus.dll. </li>
<li>Logging: enable or disable logging. </li>
<li>Verbose: enable or disable verbose logging. </li>
<li>Tracking: enable of disable event data tracking. </li>
<li>Graph: enable or disable the graph. </li>
<li>Checkpoint: enable or disable checkpoints.&nbsp;<strong>Note</strong>: in the current version this option is disabled because is not supported by the current version of the Microsoft.ServiceBus.dll. </li>
</ul>
</li>
<li>Messages in the listener forms are now tracked in an asynchronous way by a dedicated Task T. Receiver tasks just add messages to a BlockingCollection, while T reads messages out of the collection and writes them to the messages Grid control. </li>
</ul>
</li>
</ul>
<ul>
<li><strong>Message Payload Visualization</strong>:&nbsp;The font used to show the payload of messages has been hanged from <strong>Microsoft Sans Serif</strong> to <strong>Consolas</strong>. </li>
</ul>
<ul>
<li><strong>Binary Messages support</strong>:&nbsp;The tool now supports the possibility to send binary files (more on this below in the file template section). Likewise, the tool adds support to display binary files in hexadecimal format. The empiric rule  to establish if the payload of a BrokeredMessage or EventData object is binay is the presence of control characters other than line feed, carriage return and horizontal tab. </li>
</ul>
<ul>
<li>Other improvements 
<ul>
<li>Greatly improved the Dispose of user controls. </li>
<li>Increased the performance and stability of logging. </li>
</ul>
</li>
</ul>
<p><strong>Update</strong>: 22 July 2014</p>
<p>This version introduces the following updates:</p>
<ul>
<li>Changed logging: now when you stop a queue, subscription, consumer group or partition listener, the log stops immediately. </li>
<li>Updated Microsoft.ServiceBus.dll to version 2.4.1.1. </li>
<li>Minor UI changes. </li>
</ul>
<p><strong>Update</strong>: 19 September 2014</p>
<p>This version introduces the following updates:</p>
<ul>
<li>The code of the Service Bus Explorer is now available on <a href="https://github.com/paolosalvatori/ServiceBusExplorer"> GitHub</a> as a public project! </li>
<li>Updated Microsoft.ServiceBus.dll to version 2.4.3.0. </li>
<li>Added controls to specify the <strong>epoch</strong> and <strong>offsetInclusive </strong>parameters for the <strong>CreateReceiverAsync </strong>method of the <a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.eventhubconsumergroup.aspx"> EventHubConsumerGroup </a>class. </li>
</ul>
<p>&nbsp;</p>
<p><strong>Update</strong>: 22 September 2014</p>
<p>This version introduces the following updates:</p>
<ul>
<li>Added <strong>SequenceNumber </strong>column to the data grid under the <strong> Events </strong>tab of the <strong>PartitionListenerControl</strong>. </li>
<li>Added <strong>SequenceNumber</strong> to the log control &nbsp;of the <strong> PartitionListenerControl</strong>. </li>
<li>Changed the labels under the&nbsp;he&nbsp;<strong>Events&nbsp;</strong>tab of the&nbsp;<strong>PartitionListenerControl</strong>. </li>
<li>Changed the default value of the <strong>Epoch </strong>textbox from 0 to -1. </li>
</ul>
<p><strong>Update</strong>: 4 December 2014</p>
<p>This version introduces the following updates:</p>
<ul>
<li>Updated <strong>Microsoft.ServiceBus.dll to version 2.5.3.0</strong>. </li>
<li>Added support for the <strong>EnableExpress </strong>property of queues and topics: </li>
</ul>
<ul>
<li>The following bug has been fixed: <ol>
<li>Go to a topic with two or more enabled subscriptions. All the subscription icons show enabled state. </li>
<li>Right click one subscription and click 'Disable Subscription', icon shows disabled state. </li>
<li>Go to parent 'Subscriptions' node and 'Refresh': the disabled subscription shows an *enabled* icon even though it is disabled. </li>
</ol> </li>
<li>The logging mechanism in the <strong>PartitionListernerControl </strong>has been highly improved. Now logging stops as soon as the user clicks the <strong>Stop </strong>or <strong>Close </strong>button in the <strong>Consumer Group </strong>and <strong>Partition Listener </strong>dialog. </li>
</ul>
<ul>
<li>The code of the <strong>Consumer Group </strong>and<strong> Partition Listener</strong> has been dramatically improved: </li>
<li>The tool now uses the <strong>EventProcessorFactory</strong>&lt;T&gt; : <a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.ieventprocessorfactory.aspx"> IEventProcessorFactory</a>&nbsp;class&nbsp;for creating instances of the&nbsp;<strong>EventProcessor </strong>:&nbsp;<a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.ieventprocessor.aspx">IEventProcessor</a>&nbsp;class that are used to process events from event hub partitions. </li>
<li>The tool provides a custom checkpoint manager class:&nbsp;EventProcessorCheckpointManager : <a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.icheckpointmanager.aspx"> ICheckpointManager</a>. In order to use the client-side checkpoint mechanism introduced by the tool, make sure to check the <strong>Checkpoint</strong> control in the <strong>Consumer Group&nbsp;</strong>and&nbsp;<strong>Partition Listener&nbsp;</strong>dialog as shown in the picture below. </li>
</ul>
<ul>
<li>The application uses the checkpoint manager to perform checkpoints in the in-process memory and persists checkpoints to a JSON file at the exit. At the start, the application reads checkpoints from the&nbsp;<strong>EventHubPartitionCheckpoints.json</strong> file located in the same directory of the tool. To remove all checkpoints, it's sufficient to delete the file. You can also open the file and selectively delete checkpoints for specific event hub partitions. </li>
</ul>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">[
  {
    "namespace": "eventhubs",
    "eventHub": "basicsampleeventhub",
    "leases": {
      "0": {
        "PartitionId": "0",
        "Owner": "ServiceBusExplorer",
        "Token": null,
        "Epoch": 0,
        "Offset": "244712",
        "SequenceNumber": 1699
      },
      "1": {
        "PartitionId": "1",
        "Owner": "ServiceBusExplorer",
        "Token": null,
        "Epoch": 0,
        "Offset": "127416",
        "SequenceNumber": 885
      },
      "7": {
        "PartitionId": "7",
        "Owner": "ServiceBusExplorer",
        "Token": null,
        "Epoch": 0,
        "Offset": "335176",
        "SequenceNumber": 2327
      },
      "2": {
        "PartitionId": "2",
        "Owner": "ServiceBusExplorer",
        "Token": null,
        "Epoch": 0,
        "Offset": "86152",
        "SequenceNumber": 599
      },
      "4": {
        "PartitionId": "4",
        "Owner": "ServiceBusExplorer",
        "Token": null,
        "Epoch": 0,
        "Offset": "238264",
        "SequenceNumber": 1654
      },
      "3": {
        "PartitionId": "3",
        "Owner": "ServiceBusExplorer",
        "Token": null,
        "Epoch": 0,
        "Offset": "-1",
        "SequenceNumber": 0
      },
      "5": {
        "PartitionId": "5",
        "Owner": "ServiceBusExplorer",
        "Token": null,
        "Epoch": 0,
        "Offset": "-1",
        "SequenceNumber": 0
      },
      "6": {
        "PartitionId": "6",
        "Owner": "ServiceBusExplorer",
        "Token": null,
        "Epoch": 0,
        "Offset": "-1",
        "SequenceNumber": 0
      }
    }
  }
]</pre>
<div class="preview">
<pre class="js">[&nbsp;
&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"namespace"</span>:&nbsp;<span class="js__string">"eventhubs"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"eventHub"</span>:&nbsp;<span class="js__string">"basicsampleeventhub"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"leases"</span>:&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"0"</span>:&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"PartitionId"</span>:&nbsp;<span class="js__string">"0"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Owner"</span>:&nbsp;<span class="js__string">"ServiceBusExplorer"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Token"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Epoch"</span>:&nbsp;<span class="js__num">0</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Offset"</span>:&nbsp;<span class="js__string">"244712"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"SequenceNumber"</span>:&nbsp;<span class="js__num">1699</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"1"</span>:&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"PartitionId"</span>:&nbsp;<span class="js__string">"1"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Owner"</span>:&nbsp;<span class="js__string">"ServiceBusExplorer"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Token"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Epoch"</span>:&nbsp;<span class="js__num">0</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Offset"</span>:&nbsp;<span class="js__string">"127416"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"SequenceNumber"</span>:&nbsp;<span class="js__num">885</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"7"</span>:&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"PartitionId"</span>:&nbsp;<span class="js__string">"7"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Owner"</span>:&nbsp;<span class="js__string">"ServiceBusExplorer"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Token"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Epoch"</span>:&nbsp;<span class="js__num">0</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Offset"</span>:&nbsp;<span class="js__string">"335176"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"SequenceNumber"</span>:&nbsp;<span class="js__num">2327</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"2"</span>:&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"PartitionId"</span>:&nbsp;<span class="js__string">"2"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Owner"</span>:&nbsp;<span class="js__string">"ServiceBusExplorer"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Token"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Epoch"</span>:&nbsp;<span class="js__num">0</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Offset"</span>:&nbsp;<span class="js__string">"86152"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"SequenceNumber"</span>:&nbsp;<span class="js__num">599</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"4"</span>:&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"PartitionId"</span>:&nbsp;<span class="js__string">"4"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Owner"</span>:&nbsp;<span class="js__string">"ServiceBusExplorer"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Token"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Epoch"</span>:&nbsp;<span class="js__num">0</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Offset"</span>:&nbsp;<span class="js__string">"238264"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"SequenceNumber"</span>:&nbsp;<span class="js__num">1654</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"3"</span>:&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"PartitionId"</span>:&nbsp;<span class="js__string">"3"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Owner"</span>:&nbsp;<span class="js__string">"ServiceBusExplorer"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Token"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Epoch"</span>:&nbsp;<span class="js__num">0</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Offset"</span>:&nbsp;<span class="js__string">"-1"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"SequenceNumber"</span>:&nbsp;<span class="js__num">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"5"</span>:&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"PartitionId"</span>:&nbsp;<span class="js__string">"5"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Owner"</span>:&nbsp;<span class="js__string">"ServiceBusExplorer"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Token"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Epoch"</span>:&nbsp;<span class="js__num">0</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Offset"</span>:&nbsp;<span class="js__string">"-1"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"SequenceNumber"</span>:&nbsp;<span class="js__num">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"6"</span>:&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"PartitionId"</span>:&nbsp;<span class="js__string">"6"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Owner"</span>:&nbsp;<span class="js__string">"ServiceBusExplorer"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Token"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Epoch"</span>:&nbsp;<span class="js__num">0</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Offset"</span>:&nbsp;<span class="js__string">"-1"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"SequenceNumber"</span>:&nbsp;<span class="js__num">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
]</pre>
</div>
</div>
</div>
<ul>
<li>This version of the tool introduces the possibility to send events directly to a specific event hub partition using the <a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.eventhubsender.aspx"> EventHubSender</a> class.&nbsp; </li>
<li>A bug affecting the dimension of the controls in the <strong>Messages </strong> and <strong>Deadletter Messages </strong>dialogs for queues and subscriptions has been fixed. To send messages to a specific event hub partition, right click a partition and choose <strong>Send Events</strong> as shown in the picture below. </li>
</ul>
<ul>
<li>When you send messages to a specific partition, the <a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.eventdata.partitionkey.aspx"> EventData.PartitionKey</a> property is ignored. For this reason, the <strong>Update PartitionKey</strong> and <strong>No PartitionKey</strong> checkboxes are not available when sending messages to a given event hub partition. </li>
</ul>
<ul>
<li>When a queue or subscription is partitioned, now the tool invokes the <a href="http://msdn.microsoft.com/en-us/library/jj908731.aspx"> MessageReceiver.Peek</a> and <a href="http://msdn.microsoft.com/en-us/library/hh181919.aspx"> MessageReceiver.Receive</a> methods respectively, to peek and read messages one by one from the current queue, subscription or deadletter queue, instead of using the <a href="http://msdn.microsoft.com/en-us/library/jj908766.aspx">MessageReceiver.PeekBatch</a> or <a href="http://msdn.microsoft.com/en-us/library/jj673067.aspx">MessageReceiver.ReceiveBatch</a> methods. In fact, when the entity is partitioned, the batch methods read messages only from a single partition, hence they don't return all the messages from the  16 partitions or fragments that form the entity. </li>
<li>Fxed the value shown by the <strong>Max Size in Gigabytes</strong> slider when the queue or topic is partitioned. In fact, when the queue or subscription is partitioned, the <a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.queuedescription.maxsizeinmegabytes.aspx"> QueueDescription.MaxSizeInMegabytes</a>&nbsp;and <a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.topicdescription.maxsizeinmegabytes.aspx"> TopicDescription.MaxSizeInMegabytes</a>&nbsp;property contains the size you specified when you created the entity (1024;2048;3072;4096;5120) multiplied by 16, that is, the number of fragments that compose the partitioned entity. </li>
<li>Fixed a problem that happened when updating the value of the ForwardTo and ForwardDeadLetteredMessagesTo properties of a subscription. </li>
<li>This version of the <strong>Service Bus Explorer </strong>introduces the possibility to save&nbsp;individual or multiple messages to a file in <strong>JSON</strong> format. See below for details. </li>
<li>In the <strong>Messages </strong>or <strong>Deadletter </strong>tab of a queue or subscription you can save a single message&nbsp;to a JSON file: right click the message to open the context menu and click&nbsp;<strong>Save Selected Message </strong>as shown in the picture below.<br /> <br /> <br /> This opens the <strong>Save File As</strong>&nbsp;dialog as shown in the picture below.<br /> The JSON file contains the body and properties of the selected <a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.brokeredmessage.aspx"> BrokeredMessage</a> object. Properties are in camel case.<br />
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">{
  "body": {
    "deviceid": 84,
    "value": 68
  },
  "contentType": null,
  "correlationId": null,
  "deliveryCount": 1,
  "enqueuedSequenceNumber": 1,
  "enqueuedTimeUtc": "2014-12-03T16:39:48.4958476Z",
  "expiresAtUtc": "9999-12-31T23:59:59.9999999",
  "forcePersistence": false,
  "isBodyConsumed": false,
  "isLargeMessage": false,
  "label": "Service Bus Explorer",
  "lockedUntilUtc": null,
  "lockToken": null,
  "messageId": "5cbc08aa-dca5-434b-bc27-afc8ba98307f",
  "partitionKey": "18",
  "properties": {
    "deviceId": 84,
    "value": 68,
    "time": 635532215878943345,
    "city": "Milan",
    "country": "Italy"
  },
  "replyTo": null,
  "replyToSessionId": null,
  "scheduledEnqueueTimeUtc": "0001-01-01T00:00:00",
  "sequenceNumber": 281474976710657,
  "sessionId": null,
  "size": 229,
  "state": 0,
  "timeToLive": "10675199.02:48:05.4775807",
  "to": null,
  "viaPartitionKey": null
}</pre>
<div class="preview">
<pre class="js"><span class="js__brace">{</span><span class="js__string">"body"</span>:&nbsp;<span class="js__brace">{</span><span class="js__string">"deviceid"</span>:&nbsp;<span class="js__num">84</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"value"</span>:&nbsp;<span class="js__num">68</span><span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"contentType"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;<span class="js__string">"correlationId"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;<span class="js__string">"deliveryCount"</span>:&nbsp;<span class="js__num">1</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"enqueuedSequenceNumber"</span>:&nbsp;<span class="js__num">1</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"enqueuedTimeUtc"</span>:&nbsp;<span class="js__string">"2014-12-03T16:39:48.4958476Z"</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"expiresAtUtc"</span>:&nbsp;<span class="js__string">"9999-12-31T23:59:59.9999999"</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"forcePersistence"</span>:&nbsp;false,&nbsp;
&nbsp;&nbsp;<span class="js__string">"isBodyConsumed"</span>:&nbsp;false,&nbsp;
&nbsp;&nbsp;<span class="js__string">"isLargeMessage"</span>:&nbsp;false,&nbsp;
&nbsp;&nbsp;<span class="js__string">"label"</span>:&nbsp;<span class="js__string">"Service&nbsp;Bus&nbsp;Explorer"</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"lockedUntilUtc"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;<span class="js__string">"lockToken"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;<span class="js__string">"messageId"</span>:&nbsp;<span class="js__string">"5cbc08aa-dca5-434b-bc27-afc8ba98307f"</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"partitionKey"</span>:&nbsp;<span class="js__string">"18"</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"properties"</span>:&nbsp;<span class="js__brace">{</span><span class="js__string">"deviceId"</span>:&nbsp;<span class="js__num">84</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"value"</span>:&nbsp;<span class="js__num">68</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"time"</span>:&nbsp;<span class="js__num">635532215878943345</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"city"</span>:&nbsp;<span class="js__string">"Milan"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"country"</span>:&nbsp;<span class="js__string">"Italy"</span><span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"replyTo"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;<span class="js__string">"replyToSessionId"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;<span class="js__string">"scheduledEnqueueTimeUtc"</span>:&nbsp;<span class="js__string">"0001-01-01T00:00:00"</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"sequenceNumber"</span>:&nbsp;<span class="js__num">281474976710657</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"sessionId"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;<span class="js__string">"size"</span>:&nbsp;<span class="js__num">229</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"state"</span>:&nbsp;<span class="js__num">0</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"timeToLive"</span>:&nbsp;<span class="js__string">"10675199.02:48:05.4775807"</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"to"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;<span class="js__string">"viaPartitionKey"</span>:&nbsp;null&nbsp;
<span class="js__brace">}</span></pre>
</div>
</div>
</div>
</li>
<li>As an alternative, you can double click a message in the <strong>Messages </strong> or <strong>Deadletter </strong>tab for queue or subscription to open the <strong> Repair and Resubmit Message</strong> dialog and then click the <strong>Save </strong> button as shown in the picture below. </li>
</ul>
<ul>
<li>In the&nbsp;<strong>Messages&nbsp;</strong>or&nbsp;<strong>Deadletter&nbsp;</strong>tab of a queue or subscription you can save multiple messages&nbsp;to a JSON file: select multiple messages and&nbsp;right click one of them to open the context menu and  then click&nbsp;<strong>Save Selected Messages&nbsp;</strong>as shown in the picture below. <br /> <br /> &nbsp;This opens the&nbsp;<strong>Save File As</strong>&nbsp;dialog as shown in the picture below.<br /> <br /> <br /> The JSON file contains an array of items. Each item contains the body and properties of one of the selected&nbsp;<a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.brokeredmessage.aspx">BrokeredMessage</a> objects. Properties are  in camel case. <br />
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">[
  {
    "body": {
      "deviceid": 36,
      "value": 93
    },
    "contentType": null,
    "correlationId": null,
    "deliveryCount": 1,
    "enqueuedSequenceNumber": 1,
    "enqueuedTimeUtc": "2014-12-03T16:39:48.7794889Z",
    "expiresAtUtc": "9999-12-31T23:59:59.9999999",
    "forcePersistence": false,
    "isBodyConsumed": false,
    "isLargeMessage": false,
    "label": "Service Bus Explorer",
    "lockedUntilUtc": null,
    "lockToken": null,
    "messageId": "197c5f6b-635c-437e-b419-5b346b626c35",
    "partitionKey": "0",
    "properties": {
      "deviceId": 36,
      "value": 93,
      "time": 635532215878943345,
      "city": "Milan",
      "country": "Italy"
    },
    "replyTo": null,
    "replyToSessionId": null,
    "scheduledEnqueueTimeUtc": "0001-01-01T00:00:00",
    "sequenceNumber": 4785074604081153,
    "sessionId": null,
    "size": 228,
    "state": 0,
    "timeToLive": "10675199.02:48:05.4775807",
    "to": null,
    "viaPartitionKey": null
  },
  {
    "body": {
      "deviceid": 85,
      "value": 29
    },
    "contentType": null,
    "correlationId": null,
    "deliveryCount": 1,
    "enqueuedSequenceNumber": 2,
    "enqueuedTimeUtc": "2014-12-03T16:39:56.5925297Z",
    "expiresAtUtc": "9999-12-31T23:59:59.9999999",
    "forcePersistence": false,
    "isBodyConsumed": false,
    "isLargeMessage": false,
    "label": "Service Bus Explorer",
    "lockedUntilUtc": null,
    "lockToken": null,
    "messageId": "fa9482b5-6dff-4268-9a5c-3d9b08bb358c",
    "partitionKey": "942",
    "properties": {
      "deviceId": 85,
      "value": 29,
      "time": 635532215962583503,
      "city": "Milan",
      "country": "Italy"
    },
    "replyTo": null,
    "replyToSessionId": null,
    "scheduledEnqueueTimeUtc": "0001-01-01T00:00:00",
    "sequenceNumber": 562949953421314,
    "sessionId": null,
    "size": 230,
    "state": 0,
    "timeToLive": "10675199.02:48:05.4775807",
    "to": null,
    "viaPartitionKey": null
  },
  {
    "body": {
      "deviceid": 84,
      "value": 68
    },
    "contentType": null,
    "correlationId": null,
    "deliveryCount": 1,
    "enqueuedSequenceNumber": 1,
    "enqueuedTimeUtc": "2014-12-03T16:39:48.4958476Z",
    "expiresAtUtc": "9999-12-31T23:59:59.9999999",
    "forcePersistence": false,
    "isBodyConsumed": false,
    "isLargeMessage": false,
    "label": "Service Bus Explorer",
    "lockedUntilUtc": null,
    "lockToken": null,
    "messageId": "5cbc08aa-dca5-434b-bc27-afc8ba98307f",
    "partitionKey": "18",
    "properties": {
      "deviceId": 84,
      "value": 68,
      "time": 635532215878943345,
      "city": "Milan",
      "country": "Italy"
    },
    "replyTo": null,
    "replyToSessionId": null,
    "scheduledEnqueueTimeUtc": "0001-01-01T00:00:00",
    "sequenceNumber": 281474976710657,
    "sessionId": null,
    "size": 229,
    "state": 0,
    "timeToLive": "10675199.02:48:05.4775807",
    "to": null,
    "viaPartitionKey": null
  }
]</pre>
<div class="preview">
<pre class="js">[&nbsp;
&nbsp;&nbsp;<span class="js__brace">{</span><span class="js__string">"body"</span>:&nbsp;<span class="js__brace">{</span><span class="js__string">"deviceid"</span>:&nbsp;<span class="js__num">36</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"value"</span>:&nbsp;<span class="js__num">93</span><span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"contentType"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"correlationId"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"deliveryCount"</span>:&nbsp;<span class="js__num">1</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"enqueuedSequenceNumber"</span>:&nbsp;<span class="js__num">1</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"enqueuedTimeUtc"</span>:&nbsp;<span class="js__string">"2014-12-03T16:39:48.7794889Z"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"expiresAtUtc"</span>:&nbsp;<span class="js__string">"9999-12-31T23:59:59.9999999"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"forcePersistence"</span>:&nbsp;false,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"isBodyConsumed"</span>:&nbsp;false,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"isLargeMessage"</span>:&nbsp;false,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"label"</span>:&nbsp;<span class="js__string">"Service&nbsp;Bus&nbsp;Explorer"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"lockedUntilUtc"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"lockToken"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"messageId"</span>:&nbsp;<span class="js__string">"197c5f6b-635c-437e-b419-5b346b626c35"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"partitionKey"</span>:&nbsp;<span class="js__string">"0"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"properties"</span>:&nbsp;<span class="js__brace">{</span><span class="js__string">"deviceId"</span>:&nbsp;<span class="js__num">36</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"value"</span>:&nbsp;<span class="js__num">93</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"time"</span>:&nbsp;<span class="js__num">635532215878943345</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"city"</span>:&nbsp;<span class="js__string">"Milan"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"country"</span>:&nbsp;<span class="js__string">"Italy"</span><span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"replyTo"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"replyToSessionId"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"scheduledEnqueueTimeUtc"</span>:&nbsp;<span class="js__string">"0001-01-01T00:00:00"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"sequenceNumber"</span>:&nbsp;<span class="js__num">4785074604081153</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"sessionId"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"size"</span>:&nbsp;<span class="js__num">228</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"state"</span>:&nbsp;<span class="js__num">0</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"timeToLive"</span>:&nbsp;<span class="js__string">"10675199.02:48:05.4775807"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"to"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"viaPartitionKey"</span>:&nbsp;null&nbsp;
&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;<span class="js__brace">{</span><span class="js__string">"body"</span>:&nbsp;<span class="js__brace">{</span><span class="js__string">"deviceid"</span>:&nbsp;<span class="js__num">85</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"value"</span>:&nbsp;<span class="js__num">29</span><span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"contentType"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"correlationId"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"deliveryCount"</span>:&nbsp;<span class="js__num">1</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"enqueuedSequenceNumber"</span>:&nbsp;<span class="js__num">2</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"enqueuedTimeUtc"</span>:&nbsp;<span class="js__string">"2014-12-03T16:39:56.5925297Z"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"expiresAtUtc"</span>:&nbsp;<span class="js__string">"9999-12-31T23:59:59.9999999"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"forcePersistence"</span>:&nbsp;false,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"isBodyConsumed"</span>:&nbsp;false,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"isLargeMessage"</span>:&nbsp;false,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"label"</span>:&nbsp;<span class="js__string">"Service&nbsp;Bus&nbsp;Explorer"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"lockedUntilUtc"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"lockToken"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"messageId"</span>:&nbsp;<span class="js__string">"fa9482b5-6dff-4268-9a5c-3d9b08bb358c"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"partitionKey"</span>:&nbsp;<span class="js__string">"942"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"properties"</span>:&nbsp;<span class="js__brace">{</span><span class="js__string">"deviceId"</span>:&nbsp;<span class="js__num">85</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"value"</span>:&nbsp;<span class="js__num">29</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"time"</span>:&nbsp;<span class="js__num">635532215962583503</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"city"</span>:&nbsp;<span class="js__string">"Milan"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"country"</span>:&nbsp;<span class="js__string">"Italy"</span><span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"replyTo"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"replyToSessionId"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"scheduledEnqueueTimeUtc"</span>:&nbsp;<span class="js__string">"0001-01-01T00:00:00"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"sequenceNumber"</span>:&nbsp;<span class="js__num">562949953421314</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"sessionId"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"size"</span>:&nbsp;<span class="js__num">230</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"state"</span>:&nbsp;<span class="js__num">0</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"timeToLive"</span>:&nbsp;<span class="js__string">"10675199.02:48:05.4775807"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"to"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"viaPartitionKey"</span>:&nbsp;null&nbsp;
&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;<span class="js__brace">{</span><span class="js__string">"body"</span>:&nbsp;<span class="js__brace">{</span><span class="js__string">"deviceid"</span>:&nbsp;<span class="js__num">84</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"value"</span>:&nbsp;<span class="js__num">68</span><span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"contentType"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"correlationId"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"deliveryCount"</span>:&nbsp;<span class="js__num">1</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"enqueuedSequenceNumber"</span>:&nbsp;<span class="js__num">1</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"enqueuedTimeUtc"</span>:&nbsp;<span class="js__string">"2014-12-03T16:39:48.4958476Z"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"expiresAtUtc"</span>:&nbsp;<span class="js__string">"9999-12-31T23:59:59.9999999"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"forcePersistence"</span>:&nbsp;false,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"isBodyConsumed"</span>:&nbsp;false,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"isLargeMessage"</span>:&nbsp;false,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"label"</span>:&nbsp;<span class="js__string">"Service&nbsp;Bus&nbsp;Explorer"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"lockedUntilUtc"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"lockToken"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"messageId"</span>:&nbsp;<span class="js__string">"5cbc08aa-dca5-434b-bc27-afc8ba98307f"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"partitionKey"</span>:&nbsp;<span class="js__string">"18"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"properties"</span>:&nbsp;<span class="js__brace">{</span><span class="js__string">"deviceId"</span>:&nbsp;<span class="js__num">84</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"value"</span>:&nbsp;<span class="js__num">68</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"time"</span>:&nbsp;<span class="js__num">635532215878943345</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"city"</span>:&nbsp;<span class="js__string">"Milan"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"country"</span>:&nbsp;<span class="js__string">"Italy"</span><span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"replyTo"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"replyToSessionId"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"scheduledEnqueueTimeUtc"</span>:&nbsp;<span class="js__string">"0001-01-01T00:00:00"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"sequenceNumber"</span>:&nbsp;<span class="js__num">281474976710657</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"sessionId"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"size"</span>:&nbsp;<span class="js__num">229</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"state"</span>:&nbsp;<span class="js__num">0</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"timeToLive"</span>:&nbsp;<span class="js__string">"10675199.02:48:05.4775807"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"to"</span>:&nbsp;null,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"viaPartitionKey"</span>:&nbsp;null&nbsp;
&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
]</pre>
</div>
</div>
</div>
</li>
</ul>
<ul>
<li>In the&nbsp;<strong>Consumer Group&nbsp;or&nbsp;Partition Listener</strong>&nbsp;dialog you can save a single event to a JSON file: right click the event under the <strong>Events</strong> tab to open the context menu and click&nbsp;<strong>Save Selected Event.</strong><br /> <br /> <br /> &nbsp;This opens the&nbsp;<strong>Save File As</strong>&nbsp;dialog.<br /> <br /> <br /> The JSON file contains the body and properties of the selected <a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.eventdata.aspx"> EventData</a> object. Properties are in camel case.<br />
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">{
  "body": {
    "deviceid": 6,
    "name": "device006",
    "value": 50,
    "location": "Milan"
  },
  "enqueuedTimeUtc": "2014-11-06T17:18:01.057Z",
  "offset": "208",
  "partitionKey": "device006",
  "properties": {
    "id": 6,
    "name": "device006",
    "location": "Milan",
    "value": 50
  },
  "sequenceNumber": 1,
  "systemProperties": {
    "Publisher": "device006",
    "PartitionKey": "device006",
    "EnqueuedTimeUtc": "2014-11-06T17:18:01.057Z",
    "SequenceNumber": 1,
    "Offset": "208"
  }
}</pre>
<div class="preview">
<pre class="js"><span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;<span class="js__string">"body"</span>:&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"deviceid"</span>:&nbsp;<span class="js__num">6</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"name"</span>:&nbsp;<span class="js__string">"device006"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"value"</span>:&nbsp;<span class="js__num">50</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"location"</span>:&nbsp;<span class="js__string">"Milan"</span>&nbsp;
&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"enqueuedTimeUtc"</span>:&nbsp;<span class="js__string">"2014-11-06T17:18:01.057Z"</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"offset"</span>:&nbsp;<span class="js__string">"208"</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"partitionKey"</span>:&nbsp;<span class="js__string">"device006"</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"properties"</span>:&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"id"</span>:&nbsp;<span class="js__num">6</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"name"</span>:&nbsp;<span class="js__string">"device006"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"location"</span>:&nbsp;<span class="js__string">"Milan"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"value"</span>:&nbsp;<span class="js__num">50</span>&nbsp;
&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"sequenceNumber"</span>:&nbsp;<span class="js__num">1</span>,&nbsp;
&nbsp;&nbsp;<span class="js__string">"systemProperties"</span>:&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Publisher"</span>:&nbsp;<span class="js__string">"device006"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"PartitionKey"</span>:&nbsp;<span class="js__string">"device006"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"EnqueuedTimeUtc"</span>:&nbsp;<span class="js__string">"2014-11-06T17:18:01.057Z"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"SequenceNumber"</span>:&nbsp;<span class="js__num">1</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Offset"</span>:&nbsp;<span class="js__string">"208"</span>&nbsp;
&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
<span class="js__brace">}</span>&nbsp;</pre>
</div>
</div>
</div>
</li>
</ul>
<ul>
<li>As an alternative, you can double click an event in the&nbsp;<strong>Consumer Group&nbsp;or&nbsp;Partition Listener</strong>&nbsp;dialog&nbsp;to open the&nbsp;<strong>View Event Data</strong>&nbsp;dialog and then click the <strong>Save </strong>button. </li>
</ul>
<p style="text-align: center;">&nbsp;</p>
<ul>
<li>In the&nbsp;<strong>Consumer Group&nbsp;or&nbsp;Partition Listener</strong>&nbsp;dialog you can save multiple events&nbsp;to a JSON file: select multiple events and&nbsp;right click one of them to open the context menu and then click&nbsp;<strong>Save Selected  Events</strong>.&nbsp;<br /> &nbsp;This opens the&nbsp;<strong>Save File As</strong>&nbsp;dialog.<br /> <br /> <br /> The JSON file contains an array of items. Each item contains the body and properties of one of the selected <a href="http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.eventdata.aspx"> EventData</a> objects. Properties are in camel case.&nbsp;<br />
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">[
  {
    "body": {
      "deviceid": 6,
      "name": "device006",
      "value": 26,
      "location": "Milan"
    },
    "enqueuedTimeUtc": "2014-11-06T17:18:03.267Z",
    "offset": "624",
    "partitionKey": "device006",
    "properties": {
      "id": 6,
      "name": "device006",
      "location": "Milan",
      "value": 26
    },
    "sequenceNumber": 3,
    "systemProperties": {
      "Publisher": "device006",
      "PartitionKey": "device006",
      "EnqueuedTimeUtc": "2014-11-06T17:18:03.267Z",
      "SequenceNumber": 3,
      "Offset": "624"
    }
  },
  {
    "body": {
      "deviceid": 6,
      "name": "device006",
      "value": 23,
      "location": "Milan"
    },
    "enqueuedTimeUtc": "2014-11-06T17:18:02.173Z",
    "offset": "416",
    "partitionKey": "device006",
    "properties": {
      "id": 6,
      "name": "device006",
      "location": "Milan",
      "value": 23
    },
    "sequenceNumber": 2,
    "systemProperties": {
      "Publisher": "device006",
      "PartitionKey": "device006",
      "EnqueuedTimeUtc": "2014-11-06T17:18:02.173Z",
      "SequenceNumber": 2,
      "Offset": "416"
    }
  },
  {
    "body": {
      "deviceid": 6,
      "name": "device006",
      "value": 50,
      "location": "Milan"
    },
    "enqueuedTimeUtc": "2014-11-06T17:18:01.057Z",
    "offset": "208",
    "partitionKey": "device006",
    "properties": {
      "id": 6,
      "name": "device006",
      "location": "Milan",
      "value": 50
    },
    "sequenceNumber": 1,
    "systemProperties": {
      "Publisher": "device006",
      "PartitionKey": "device006",
      "EnqueuedTimeUtc": "2014-11-06T17:18:01.057Z",
      "SequenceNumber": 1,
      "Offset": "208"
    }
  }
]</pre>
<div class="preview">
<pre class="js">[&nbsp;
&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"body"</span>:&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"deviceid"</span>:&nbsp;<span class="js__num">6</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"name"</span>:&nbsp;<span class="js__string">"device006"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"value"</span>:&nbsp;<span class="js__num">26</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"location"</span>:&nbsp;<span class="js__string">"Milan"</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"enqueuedTimeUtc"</span>:&nbsp;<span class="js__string">"2014-11-06T17:18:03.267Z"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"offset"</span>:&nbsp;<span class="js__string">"624"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"partitionKey"</span>:&nbsp;<span class="js__string">"device006"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"properties"</span>:&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"id"</span>:&nbsp;<span class="js__num">6</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"name"</span>:&nbsp;<span class="js__string">"device006"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"location"</span>:&nbsp;<span class="js__string">"Milan"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"value"</span>:&nbsp;<span class="js__num">26</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"sequenceNumber"</span>:&nbsp;<span class="js__num">3</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"systemProperties"</span>:&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Publisher"</span>:&nbsp;<span class="js__string">"device006"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"PartitionKey"</span>:&nbsp;<span class="js__string">"device006"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"EnqueuedTimeUtc"</span>:&nbsp;<span class="js__string">"2014-11-06T17:18:03.267Z"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"SequenceNumber"</span>:&nbsp;<span class="js__num">3</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Offset"</span>:&nbsp;<span class="js__string">"624"</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"body"</span>:&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"deviceid"</span>:&nbsp;<span class="js__num">6</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"name"</span>:&nbsp;<span class="js__string">"device006"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"value"</span>:&nbsp;<span class="js__num">23</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"location"</span>:&nbsp;<span class="js__string">"Milan"</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"enqueuedTimeUtc"</span>:&nbsp;<span class="js__string">"2014-11-06T17:18:02.173Z"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"offset"</span>:&nbsp;<span class="js__string">"416"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"partitionKey"</span>:&nbsp;<span class="js__string">"device006"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"properties"</span>:&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"id"</span>:&nbsp;<span class="js__num">6</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"name"</span>:&nbsp;<span class="js__string">"device006"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"location"</span>:&nbsp;<span class="js__string">"Milan"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"value"</span>:&nbsp;<span class="js__num">23</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"sequenceNumber"</span>:&nbsp;<span class="js__num">2</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"systemProperties"</span>:&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Publisher"</span>:&nbsp;<span class="js__string">"device006"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"PartitionKey"</span>:&nbsp;<span class="js__string">"device006"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"EnqueuedTimeUtc"</span>:&nbsp;<span class="js__string">"2014-11-06T17:18:02.173Z"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"SequenceNumber"</span>:&nbsp;<span class="js__num">2</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Offset"</span>:&nbsp;<span class="js__string">"416"</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"body"</span>:&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"deviceid"</span>:&nbsp;<span class="js__num">6</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"name"</span>:&nbsp;<span class="js__string">"device006"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"value"</span>:&nbsp;<span class="js__num">50</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"location"</span>:&nbsp;<span class="js__string">"Milan"</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"enqueuedTimeUtc"</span>:&nbsp;<span class="js__string">"2014-11-06T17:18:01.057Z"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"offset"</span>:&nbsp;<span class="js__string">"208"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"partitionKey"</span>:&nbsp;<span class="js__string">"device006"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"properties"</span>:&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"id"</span>:&nbsp;<span class="js__num">6</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"name"</span>:&nbsp;<span class="js__string">"device006"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"location"</span>:&nbsp;<span class="js__string">"Milan"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"value"</span>:&nbsp;<span class="js__num">50</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"sequenceNumber"</span>:&nbsp;<span class="js__num">1</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"systemProperties"</span>:&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Publisher"</span>:&nbsp;<span class="js__string">"device006"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"PartitionKey"</span>:&nbsp;<span class="js__string">"device006"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"EnqueuedTimeUtc"</span>:&nbsp;<span class="js__string">"2014-11-06T17:18:01.057Z"</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"SequenceNumber"</span>:&nbsp;<span class="js__num">1</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">"Offset"</span>:&nbsp;<span class="js__string">"208"</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
]&nbsp;</pre>
</div>
</div>
</div>
</li>
</ul>
<p><strong>Update</strong>: 18 December 2014</p>
<p>This version introduces the following updates:</p>
<ul>
<li>Starting on 17 December 2014, Event Hubs are available in public preview in the Microsoft Azure datacenter in China. Hence, the version 2.5.3.1 of Service Bus Explorer introduces support for the uri of Microsoft Azure DC in China. </li>
</ul>
<p><strong>Update</strong>: 2 March 2015</p>
<p>This version introduces the following updates:</p>
<ul>
<li>The tool now uses the <strong>Microsoft.ServiceBus.dll v.2.6.1.0</strong>. </li>
<li>Completely refreshed support for dynamic relay services and added full support for persistent relay services. For more information on persistent relay services, see&nbsp;<a href="https://code.msdn.microsoft.com/windowsazure/How-to-handle-Service-Bus-6d65eca1">How  to handle Service Bus Relay Services in a multi-tenant environment</a>. </li>
<li>You can select dynamic and persistent relay services in the main treeview and view their properties in the main panel. </li>
</ul>
<p style="text-align: center;"><img id="134454" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134454/1/relayservices01.png" alt="" width="372" height="570" /></p>
<ul>
<li><span style="text-align: center;">You can create, delete, update persistent relay services. In particular, you can define the relay type or binding, the transport security and client authorization characteristics of the persistent relay service in the </span><strong style="text-align: center;">Description </strong><span style="text-align: center;">tab of the </span><strong style="text-align: center;">HandleRelayControl</strong><span style="text-align: center;">.&nbsp;</span> </li>
</ul>
<p style="text-align: center;"><img id="134456" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134456/1/relayservices02.png" alt="" width="800" /></p>
<p style="text-align: center;"><strong>Relay service definition</strong></p>
<ul>
<li><span style="text-align: center;">You can create, review, update, delete the authorization rules alias shared access policies at the entity level for persistent relay services&nbsp;</span><span style="text-align: center;">n the&nbsp;</span><strong style="text-align: center;">Authorization  Rules&nbsp;</strong><span style="text-align: center;">tab of the&nbsp;</span><strong style="text-align: center;">HandleRelayControl</strong><span style="text-align: center;">.&nbsp;</span> </li>
</ul>
<p style="text-align: center;"><img id="134457" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134457/1/relayservices03.png" alt="" width="800" /></p>
<p style="text-align: center;"><strong>Relay service authorization rules&nbsp;</strong></p>
<ul>
<li>You can query the metrics of both persistent and dynamic relay services in the&nbsp;<strong>Metrics&nbsp;</strong>tab of the&nbsp;<strong>HandleRelayControl</strong>. See point <strong>3</strong> in the picture below.&nbsp;For more information on this subject, see <a href="https://msdn.microsoft.com/en-us/library/azure/dn163589.aspx">Service Bus Entity Metrics REST APIs</a>. </li>
</ul>
<p style="text-align: center;"><img id="134458" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134458/1/relayservices04.png" alt="" width="800" /></p>
<p style="text-align: center;"><strong>Metric rule definition</strong></p>
<p style="text-align: center;"><strong><img id="134459" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134459/1/relayservices05.png" alt="" width="800" /><br /> </strong></p>
<p style="text-align: center;"><strong>Metrics data and charts</strong></p>
<ul>
<li>You can test both dynamic and persistent relay services in SDI and MDI mode. </li>
</ul>
<p style="text-align: center;"><img id="134460" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134460/1/relayservices06.png" alt="" width="800" /></p>
<ul>
<li>Added support to import/export persistent relay services from/to an XML file. </li>
<li>When the&nbsp;<strong>saveMessageToFile&nbsp;</strong>setting in the configuration file is set to&nbsp;<strong>true</strong>, the message content of the Test Relay form is saved to file on exit. </li>
</ul>
<p><img id="134452" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134452/1/relaymessage.png" alt="" width="800" /></p>
<ul>
<li>Added support for the <a href="https://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.partitiondescription.aspx"> PartitionDescription</a>.<a href="https://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.partitiondescription.lastenqueuedoffset.aspx">LastEnqueuedOffset</a>,&nbsp;<a href="https://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.partitiondescription.aspx">PartitionDescription</a>.<a href="https://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.partitiondescription.lastenqueuedtimeutc.aspx">LastEnqueuedTimeUtc</a>,&nbsp;<a href="https://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.partitiondescription.aspx">PartitionDescription</a>.<a href="https://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.partitiondescription.incomingbytespersecond.aspx">IncomingBytesPerSecond</a>,&nbsp;<a href="https://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.partitiondescription.aspx">PartitionDescription</a>.<a href="https://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.partitiondescription.outgoingbytespersecond.aspx">OutgoingBytesPerSecond</a> in both the <strong>HandlePartitionControl</strong> and <strong>PartitionListenerControl</strong> as shown in the figures below. </li>
</ul>
<p style="text-align: center;"><img id="134463" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134463/1/bytespersecond01.png" alt="" width="800" /></p>
<p style="text-align: center;"><strong>HandlePartitionControl</strong></p>
<p style="text-align: center;"><img id="134464" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134464/1/bytespersecond02.png" alt="" width="800" /></p>
<p style="text-align: center;"><strong>Consumer Group / Partition Listener&nbsp;</strong></p>
<ul>
<li>The <strong>Consumer Group / Partition Listener </strong>control added the possibility to start receiving events from a specific point in time by defining a value for the&nbsp;<a href="https://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.eventhubreceiver.aspx">EventHubReceiver</a>.<a href="https://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.eventhubreceiver.startingdatetimeutc.aspx">StartingDateTimeUtc</a> property.&nbsp;<strong>Note</strong>: you have to specify date and time in UTC format, not in the local date and time format. </li>
</ul>
<p style="text-align: center;"><img id="134480" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134480/1/startingdatetimeutc01.png" alt="" width="800" /></p>
<p style="text-align: center;"><strong>Consumer Group / Partition Listener: Listener Tab</strong></p>
<p style="text-align: center;"><img id="134481" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134481/1/startingdatetimeutc02.png" alt="" width="800" /></p>
<p style="text-align: center;"><strong><strong>Consumer Group / Partition Listener: EventsTab</strong><br /> </strong></p>
<ul>
<li>The value of the&nbsp;<a href="https://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.eventdata.aspx">EventData</a>.<a href="https://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.eventdata.serializedsizeinbytes.aspx">SerializedSizeInBytes</a>&nbsp;property  is now used to calculate KB/sec in the <strong>PartitionListenerControl</strong>. </li>
</ul>
<p style="text-align: center;"><img id="134461" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134461/1/partitionlistenercontrol01.png" alt="" width="800" /></p>
<p style="text-align: center;"><strong>Consumer Group / Partition Listener</strong></p>
<ul>
<li>Fixed visualization of event data properties in the <strong>Consumer Group / Partition Listener </strong>control (<strong>PartitionListenerControl</strong>). </li>
<li>Greatly improved message tracking&nbsp;in the&nbsp;<strong>Consumer Group / Partition Listener&nbsp;</strong>control (<strong>PartitionListenerControl</strong>). </li>
<li>Fixed and extended&nbsp;<strong>Clear </strong>functionality in the&nbsp;<strong>Consumer Group / Partition Listener&nbsp;</strong>control (<strong>PartitionListenerControl</strong>). </li>
<li>Added the <strong>All </strong>item to <strong>Metrics</strong>. When <strong> All </strong>is selected, the tool will retrieve all the metrics for the selected entity. See point <strong>1</strong> in the picture below. </li>
<li>Added the possibility to delete a single metric query by pressing the delete button at the end of the row. See point <strong>2 </strong>in the picture below. </li>
</ul>
<p style="text-align: center;"><img id="134467" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134467/1/allmetric.png" alt="" width="800" /></p>
<ul>
<li>No chart is&nbsp;shown if a metric doesn't return any data. </li>
<li>When no time range is explicitly specified in a metric rule, the tool retrieves metric data of the last 7 days. </li>
<li>Added Metrics support for the&nbsp;<strong>Event Hubs</strong>,&nbsp;<strong>Consumer Groups</strong>,&nbsp;<strong>Notification Hubs</strong>&nbsp;and&nbsp;<strong>Relays</strong>. </li>
</ul>
<p style="text-align: center;"><img id="134468" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134468/1/eventhubmetrics01.png" alt="" width="800" /></p>
<p style="text-align: center;"><strong>Event Hub: metric rule definition</strong></p>
<p style="text-align: center;"><img id="134469" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134469/1/eventhubmetrics02.png" alt="" width="800" /></p>
<p style="text-align: center;"><strong>Event Hub: metric data and charts</strong></p>
<p style="text-align: center;"><strong><img id="134470" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134470/1/consumergroupmetrics01.png" alt="" width="800" /><br /> </strong></p>
<p style="text-align: center;"><strong>Consumer Group: metric rule definition</strong></p>
<p style="text-align: center;"><img id="134471" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134471/1/consumergroupmetrics02.png" alt="" width="800" /></p>
<p style="text-align: center;"><strong>Consumer Group:&nbsp;&nbsp;metric data and charts</strong></p>
<p style="text-align: center;"><img id="134472" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134472/1/notificationhubmetrics01.png" alt="" width="800" /></p>
<p style="text-align: center;"><strong>Notification Hub:&nbsp;<strong>metric rule definition</strong></strong></p>
<p style="text-align: center;"><img id="134473" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134473/1/notificationhubmetrics02.png" alt="" width="800" /></p>
<p style="text-align: center;"><strong><strong><strong><strong>Notification Hub:&nbsp;</strong>metric data and charts</strong><br /> </strong></strong></p>
<p style="text-align: center;"><img id="134474" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134474/1/relaymetrics01.png" alt="" width="800" /></p>
<p style="text-align: center;"><strong>Relay:&nbsp;<strong>metric rule definition</strong></strong></p>
<p style="text-align: center;">&nbsp;<img id="134475" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134475/1/relaymetrics02.png" alt="" width="800" /></p>
<p style="text-align: center;"><strong><strong>Relay:&nbsp;</strong>metric data and charts</strong></p>
<ul>
<li>If you right click the namespace node in treeview and select <strong>Open Metrics in SDI or MDI mode</strong>, you can access a dialog where you can select metrics of different entities. For example, this option allows to compare the throughput of an event  hub with the throughput of one of its consumer groups. </li>
</ul>
<p style="text-align: center;"><img id="134476" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134476/1/namespacemetrics01.png" alt="" width="800" /></p>
<p style="text-align: center;"><strong>Namespace:&nbsp;<strong>metric rule definition</strong></strong></p>
<p style="text-align: center;"><img id="134477" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134477/1/namespacemetrics02.png" alt="" width="800" /></p>
<p style="text-align: center;"><strong><strong><strong>Namespace: metric data and charts</strong><br /> </strong></strong></p>
<ul>
<li>Bug fixed by the developer community on <a href="https://github.com/paolosalvatori/ServiceBusExplorer"> GitHub</a> (thanks guys!): </li>
</ul>
<p><strong>Update</strong>: 3 March 2015</p>
<p>This version introduces the following updates:</p>
<ul>
<li>Fixed regression bug happening when creating a new subscription. </li>
<li>Improved <strong>Get Messages</strong> functionality for queues and subscription: the <strong>Peek </strong>option now retrieves a number of messages equal to the <strong> Top </strong>parameter even if when the batch size is greater than the max message size. In this case, the code just invokes the <strong>PeekBatch </strong>method in a loop until the number of retrieved message is equal to the value of <strong>Top</strong> parameter. </li>
</ul>
<p style="text-align: center;"><img id="134518" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134518/1/peekmessages.png" alt="" width="480" height="256" /></p>
<p><strong>Update</strong>: 10 March 2015</p>
<p>This version introduces the following updates:</p>
<ul>
<li>A warning message is shown if the Azure subscription ID and/or certificate thumbprint are not defined in the configuration file. In this case, the tool cannot be used to access entity metrics. </li>
</ul>
<p><strong>Update</strong>: 27 April 2015</p>
<p>This version introduces the following updates:</p>
<ul>
<li>Fixed a bug: an infinite loop when peeking messages from a queue or subscriptions with no messages. </li>
<li>Updated Microsoft.ServiceBus.dll to version 2.6.5. </li>
</ul>
<p><strong>Update</strong>: 9 September 2015</p>
<p>This version introduces the following updates:</p>
<ul>
<li>Bugs fixed buy the community (thanks guys!). </li>
<li>Updated Microsoft.ServiceBus.dll to version 3.0.1 </li>
<li>Introduced a reference to the new Microsoft.Azure.NotificationHubs.dll </li>
<li>Introduced the possibility to retrieve the data of all the partitions associated to the consumer group of an event hub using the Get Partition Data menu item or the Partitions button as highlighted in the pictures below </li>
</ul>
<p>&nbsp;</p>
<p style="text-align: center;"><img id="143449" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143449/1/menuitem.png" alt="" width="369" height="257" /></p>
<p style="text-align: center;">&nbsp;</p>
<p style="text-align: center;"><img id="143450" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143450/1/button.png" alt="" width="800" /></p>
<p style="text-align: justify;">&nbsp;</p>
<p><strong>Update</strong>: 10 September 2015</p>
<p>This version introduces the following updates:</p>
<ul>
<li>Bug fix to support Azure Service Bus Premium Messaging </li>
</ul>
<p><strong>Update</strong>: 14 September 2015</p>
<p>This version introduces the following updates:</p>
<ul>
<li>Bug fix to support the new Microsoft.Azure.NotificationHubs.dll assembly. </li>
</ul>
<p><strong>Update</strong>: 21 September 2015</p>
<p>This version introduces the following updates:</p>
<ul>
<li>Bug fix to support Notification Hub namespaces. </li>
<li>Microsoft.ServiceBus.dll 3.0.3 </li>
</ul>
<p><strong>Update</strong>: 6 October 2015</p>
<p>This version introduces the following updates:</p>
<ul>
<li>Bug fixes </li>
<li>Microsoft.ServiceBus.dll 3.0.4 </li>
<li>Ability to read messages from an IoT Hub. For more information, read&nbsp;<a href="https://code.msdn.microsoft.com/How-to-read-events-from-an-1641eb1b">How to read events from an IoT Hub with the Service Bus Explorer</a>. </li>
</ul>
<p><img id="143299" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143299/1/iothublistener.png" alt="" width="512" height="205" /></p>
<p><img id="143300" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143300/1/parameters.png" alt="" width="616" height="225" /></p>
<p><img id="143301" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143301/1/sbe02.png" alt="" width="800" /></p>
<p><strong>Update</strong>: 8 October 2015</p>
<p>This version introduces the following updates:</p>
<ul>
<li>The Service Bus product group (thanks Binzy!) extended the TestQueueControl and TestTopicControl with the possibility to create a separate MessagingFactory for each sender or receiver task as shown in the picture below. This should improve performance as  senders and receivers can use a different connection to Azure Service Bus message broker. </li>
</ul>
<p><img id="143375" style="display: block; margin-left: auto; margin-right: auto;" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143375/1/sendernewmessagingfactory.png" alt="" width="800" /></p>
<p><img id="143376" style="display: block; margin-left: auto; margin-right: auto;" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143376/1/receivernewmessagingfactory.png" alt="" width="800" /></p>