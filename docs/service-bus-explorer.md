# Service Bus Explorer

The following picture illustrates the high-level architecture of the Service Bus Explorer tool. The application has been written in C# using [Visual Studio 2010](http://www.microsoft.com/visualstudio/en-us) and requires the installation of the [.NET Framework 4.0](http://www.microsoft.com/download/en/details.aspx?id=17718) and [Windows Azure SDK for .NET](http://www.microsoft.com/download/en/details.aspx?id=28045). The tool can be copied and used on any workstation that satisfies the prerequisites mentioned above to manage and test  the Brokered and Relay messaging services defined in a given Service Bus namespace.

<div style="text-align: center;"><img id="143452" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143452/1/servicebusexplorer.jpg" alt="" /></div>

**NOTE:** I'll continue to develop the tool and add new functionalities. So I strongly recommend you to visit this page from time to time for a new version.

## Update 8 October 2015
This version introduces the following updates:

- The Service Bus product group (thanks Binzy!) extended the TestQueueControl and TestTopicControl with the possibility to create a separate MessagingFactory for each sender or receiver task as shown in the picture below. This should improve performance as  senders and receivers can use a different connection to Azure Service Bus message broker.

<img id="143375" style="display: block; margin-left: auto; margin-right: auto;" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143375/1/sendernewmessagingfactory.png" alt="" width="800" />

<img id="143376" style="display: block; margin-left: auto; margin-right: auto;" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143376/1/receivernewmessagingfactory.png" alt="" width="800" />

## Update 6 October 2015
This version introduces the following updates:

- Bug fixes
- Microsoft.ServiceBus.dll 3.0.4
- Ability to read messages from an IoT Hub. For more information, read [How to read events from an IoT Hub with the Service Bus Explorer](https://code.msdn.microsoft.com/How-to-read-events-from-an-1641eb1b).

<img id="143299" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143299/1/iothublistener.png" alt="" width="512" height="205" />

<img id="143300" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143300/1/parameters.png" alt="" width="616" height="225" />

<img id="143301" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143301/1/sbe02.png" alt="" width="800" />

## Update 21 September 2015

This version introduces the following updates:

- Bug fix to support Notification Hub namespaces.
- Microsoft.ServiceBus.dll 3.0.3

## Update 14 September 2015
This version introduces the following updates:

- Bug fix to support the new Microsoft.Azure.NotificationHubs.dll assembly.

## Update 10 September 2015
This version introduces the following updates:

- Bug fix to support Azure Service Bus Premium Messaging

## Update 9 September 2015
This version introduces the following updates:

- Bugs fixed buy the community (thanks guys!).
- Updated Microsoft.ServiceBus.dll to version 3.0.1
- Introduced a reference to the new Microsoft.Azure.NotificationHubs.dll
- Introduced the possibility to retrieve the data of all the partitions associated to the consumer group of an event hub using the Get Partition Data menu item or the Partitions button as highlighted in the pictures below

<p style="text-align: center;"><img id="143449" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143449/1/menuitem.png" alt="" width="369" height="257" /></p>

<p style="text-align: center;"><img id="143450" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143450/1/button.png" alt="" width="800" /></p>

## Update 27 April 2015
This version introduces the following updates:

- Fixed a bug: an infinite loop when peeking messages from a queue or subscriptions with no messages.
- Updated Microsoft.ServiceBus.dll to version 2.6.5.

## Update 10 March 2015
This version introduces the following updates:

- A warning message is shown if the Azure subscription ID and/or certificate thumbprint are not defined in the configuration file. In this case, the tool cannot be used to access entity metrics.

## Update 3 March 2015
This version introduces the following updates:

- Fixed regression bug happening when creating a new subscription.
- Improved **Get Messages** functionality for queues and subscription: the **Peek** option now retrieves a number of messages equal to the **Top** parameter even if when the batch size is greater than the max message size. In this case, the code just invokes the **PeekBatch** method in a loop until the number of retrieved message is equal to the value of **Top** parameter.

<p style="text-align: center;"><img id="134518" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134518/1/peekmessages.png" alt="" width="480" height="256" /></p>

## Update 2 March 2015
This version introduces the following updates:

- The tool now uses the **Microsoft.ServiceBus.dll v.2.6.1.0**.
- Completely refreshed support for dynamic relay services and added full support for persistent relay services. For more information on persistent relay services.
- You can select dynamic and persistent relay services in the main treeview and view their properties in the main panel. 

<p style="text-align: center;"><img id="134454" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134454/1/relayservices01.png" alt="" width="372" height="570" /></p>

- You can create, delete, update persistent relay services. In particular, you can define the relay type or binding, the transport security and client authorization characteristics of the persistent relay service in the **Description** tab of the **HandleRelayControl**. 

<p style="text-align: center;"><img id="134456" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134456/1/relayservices02.png" alt="" width="800" /></p>
<p style="text-align: center;"><strong>Relay service definition</strong></p>

- You can create, review, update, delete the authorization rules alias shared access policies at the entity level for persistent relay services the **Authorization  Rules** tab of the **HandleRelayControl**.

<p style="text-align: center;"><img id="134457" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134457/1/relayservices03.png" alt="" width="800" /></p>
<p style="text-align: center;"><strong>Relay service authorization rules&nbsp;</strong></p>

- You can query the metrics of both persistent and dynamic relay services in the **Metrics** tab of the **HandleRelayControl**. See point **3** in the picture below. For more information on this subject, see [Service Bus Entity Metrics REST APIs](https://docs.microsoft.com/en-us/rest/api/servicebus/service-bus-entity-metrics-rest-apis).

<p style="text-align: center;"><img id="134458" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134458/1/relayservices04.png" alt="" width="800" /></p>
<p style="text-align: center;"><strong>Metric rule definition</strong></p>
<p style="text-align: center;"><strong><img id="134459" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134459/1/relayservices05.png" alt="" width="800" /><br /> </strong></p>
<p style="text-align: center;"><strong>Metrics data and charts</strong></p>

- You can test both dynamic and persistent relay services in SDI and MDI mode.

<p style="text-align: center;"><img id="134460" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134460/1/relayservices06.png" alt="" width="800" /></p>

- Added support to import/export persistent relay services from/to an XML file.
- When the **saveMessageToFile** setting in the configuration file is set to **true**, the message content of the Test Relay form is saved to file on exit.

<p style="text-align: center;"><img id="134452" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134452/1/relaymessage.png" alt="" width="800" /></p>

- Added support for the [PartitionDescription](https://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.partitiondescription.aspx).[LastEnqueuedOffset](https://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.partitiondescription.lastenqueuedoffset.aspx), [PartitionDescription](https://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.partitiondescription.aspx).[LastEnqueuedTimeUtc](https://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.partitiondescription.lastenqueuedtimeutc.aspx), [PartitionDescription](https://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.partitiondescription.aspx). [IncomingBytesPerSecond](https://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.partitiondescription.incomingbytespersecond.aspx), [PartitionDescription](https://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.partitiondescription.aspx).[OutgoingBytesPerSecond](https://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.partitiondescription.outgoingbytespersecond.aspx) in both the **HandlePartitionControl** and **PartitionListenerControl** as shown in the figures below.

<p style="text-align: center;"><img id="134463" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134463/1/bytespersecond01.png" alt="" width="800" /></p>
<p style="text-align: center;"><strong>HandlePartitionControl</strong></p>
<p style="text-align: center;"><img id="134464" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134464/1/bytespersecond02.png" alt="" width="800" /></p>
<p style="text-align: center;"><strong>Consumer Group / Partition Listener&nbsp;</strong></p>

- The **Consumer Group / Partition Listener** control added the possibility to start receiving events from a specific point in time by defining a value for the [EventHubReceiver](https://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.eventhubreceiver.aspx). [StartingDateTimeUtc](https://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.eventhubreceiver.startingdatetimeutc.aspx) property. **Note:** you have to specify date and time in UTC format, not in the local date and time format.

<p style="text-align: center;"><img id="134480" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134480/1/startingdatetimeutc01.png" alt="" width="800" /></p>
<p style="text-align: center;"><strong>Consumer Group / Partition Listener: Listener Tab</strong></p>
<p style="text-align: center;"><img id="134481" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134481/1/startingdatetimeutc02.png" alt="" width="800" /></p>
<p style="text-align: center;"><strong><strong>Consumer Group / Partition Listener: EventsTab</strong><br /> </strong></p>

- The value of the [EventData](https://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.eventdata.aspx). [SerializedSizeInBytes](https://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.eventdata.serializedsizeinbytes.aspx) property is now used to calculate KB/sec in the **PartitionListenerControl**.

<p style="text-align: center;"><img id="134461" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134461/1/partitionlistenercontrol01.png" alt="" width="800" /></p>
<p style="text-align: center;"><strong>Consumer Group / Partition Listener</strong></p>

- Fixed visualization of event data properties in the **Consumer Group / Partition Listener** control (**PartitionListenerControl**).
- Greatly improved message tracking&nbsp;in the **Consumer Group / Partition Listener** control (**PartitionListenerControl**).
- Fixed and extended **Clear** funtionality in the **Consumer Group / Partition Listener** control (**PartitionListenerControl**).
- Added the **All** item to **Metrics**. When **All** is selected, the tool will retrieve all the metrics for the selected entity. See point **1** in the picture below.
- Added the possibility to delete a single metric query by pressing the delete button at the end of the row. See point **2** in the picture below.

<p style="text-align: center;"><img id="134467" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134467/1/allmetric.png" alt="" width="800" /></p>

- No chart is shown if a metric doesn't return any data.
- When no time range is explicitly specified in a metric rule, the tool retrieves metric data of the last 7 days.
- Added Metrics support for the **Event Hubs**, **Consumer Groups**, **Notification Hubs** and **Relays**.

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

- If you right click the namespace node in treeview and select **Open Metrics in SDI or MDI mode**, you can access a dialog where you can select metrics of different entities. For example, this option allows to compare the throughput of an event  hub with the throughput of one of its consumer groups.

<p style="text-align: center;"><img id="134476" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134476/1/namespacemetrics01.png" alt="" width="800" /></p>
<p style="text-align: center;">**Namespace: metric rule definition**</p>
<p style="text-align: center;"><img id="134477" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/134477/1/namespacemetrics02.png" alt="" width="800" /></p>
<p style="text-align: center;">**Namespace: metric data and charts**</p>

- Bug fixed by the developer community on <a href="https://github.com/paolosalvatori/ServiceBusExplorer"> GitHub</a> (thanks guys!)