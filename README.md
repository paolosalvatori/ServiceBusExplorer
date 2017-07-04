[![Build status](https://ci.appveyor.com/api/projects/status/x5niu29yhun36hda/branch/master?svg=true)](https://ci.appveyor.com/project/seanfeldman/servicebusexplorer/branch/master)

**Author:** Paolo Salvatori ([@babosbird](https://twitter.com/babosbird))
**Contributor:**  Sean Feldman ([@sfeldman](https://twitter.com/sfeldman)) and [more](https://github.com/paolosalvatori/ServiceBusExplorer/graphs/contributors)

# Installation
## Using [Chocolatey](https://chocolatey.org/install)
```
chocolatey install ServiceBusExplorer
```

## Using GitHub
```
curl -s https://api.github.com/repos/paolosalvatori/ServiceBusExplorer/releases/latest | grep browser_download_url | cut -d '"' -f 4
```

# Introduction

**Important Note**

The zip file contains:

- The source code for the Service Bus Explorer 3.0.4. This version of the tool uses the [Microsoft.ServiceBus.dll](http://www.nuget.org/packages/WindowsAzure.ServiceBus/) 3.0.4 that is compatible with the current version of the  Azure Service Bus, but not with the Service Bus 1.1, that is, the current version of the on-premises version of the Service Bus.
- The Service Bus Explorer 2.1.3.0. This version can be used with the Service Bus 1.1. The Service Bus Explorer 2.1 uses the [Microsoft.ServiceBus.dll](http://www.nuget.org/packages/WindowsAzure.ServiceBus/) client library which is compatible  with the Service Bus for Windows Server 1.1 RTM version. You can download the source code of the Service Bus Explorer v2.X [here](https://github.com/paolosalvatori/ServiceBusExplorer/releases/tag/2.1.0).

## Azure Service Bus
Queues and topics represent the foundation of a new cloud-based messaging and integration infrastructure that provides reliable message queuing and durable publish/subscribe messaging capabilities to both cloud and on-premises applications based on Microsoft  and non-Microsoft technologies. .NET applications can use the new functionality offered by queues and topics by using the new messaging API ([Microsoft.ServiceBus.Messaging](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.aspx)) or via WCF by using the new [NetMessagingBinding](http://msdn.microsoft.com/en-us/library/microsoft.servicebus.messaging.netmessagingbinding.aspx). Likewise, any Microsoft or non-Microsoft applications can use a [Service Bus REST API](http://msdn.microsoft.com/en-us/library/hh367521.aspx) to manage and access messaging entities over HTTPS. In November 2012, the [Service Bus for Windows Server](http://msdn.microsoft.com/en-us/library/jj193022(v=azure.10).aspx) was released. The Service Bus Exploer can be used with manage namespaces hosted by both Windows Azure Service Bus and Service Bus for Windows Server.

Queues and topics were first introduced by the Community Technology Preview (CTP) of Windows Azure that was released in May 2011. At that time, the Windows Azure Management Portal didn't provide any user interface to administer, create and delete messaging entities and the only way to accomplish this task was using the .NET or REST API. For this  reason, In June 2011 I decided to build a tool called [Service Bus Explorer](http://windowsazurecat.com/2011/07/exploring-topics-and-queues-by-building-a-service-bus-explorer-toolpart-1/) that would allow developers and system administrators to connect to a Service Bus namespace and administer its messaging entities.

<img id="143379" src="https://i1.code.msdn.s-msft.com/windowsapps/service-bus-explorer-f2abca5a/image/file/143379/1/servicebusexplorer.png" alt="" width="800" />

During the last few months I continued to develop this tool and add new features with the intended goal to facilitate the development and administration of new Service Bus-enabled applications. In the meantime, the Windows Azure Management Portal introduced the ability for a user to create, disable, update queues, topics, and subscriptions and define their properties, but not to define or display rules for an existing  subscription. Besides, the Service Bus Explorer enables to accomplish functionalities, such as importing, exporting and testing entities, that are not currently provided by the Windows Azure Management Portal. For this reason, the Service Bus Explorer tool represents the perfect companion for the official Azure portal and it can also be used to explore the features (session-based  correlation, configurable detection of duplicate messages, deferring messages, etc.) provided out-of-the-box by the Service Bus brokered messaging.

For more information on the Service Bus, you can use the following resources:

- ["Windows Azure Service Bus"](http://msdn.microsoft.com/en-us/library/ee732537.aspx) section on the MSDN site.
- ["Service Bus for Windows Server"](http://msdn.microsoft.com/en-us/library/jj193022(v=azure.10).aspx) section on the MSDN site.
- ["Service Bus"](http://msdn.microsoft.com/en-us/library/ee732537.aspx) topic on the MSDN site.
- ["Now Available: The Service Bus September 2011 Release"](http://blogs.msdn.com/b/windowsazure/archive/2011/09/16/the-service-bus-september-2011-release.aspx) article on the Windows Azure Blog.
- ["Queues, Topics, and Subscriptions"](http://msdn.microsoft.com/en-us/library/windowsazure/hh367516.aspx) article on the MSDN site.
- ["Understanding Windows Azure Queues (and Topics)"](http://blogs.msdn.com/b/clemensv/archive/2011/06/10/understanding-windows-azure-appfabric-queues-and-topics.aspx) video on the Clemens Vasters Blog.
- ["Building loosely-coupled apps with Windows Azure Service Bus Topics and Queues"](http://channel9.msdn.com/Events/BUILD/BUILD2011/SAC-862T) video on the channel9 site.
- ["Service Bus Topics And Queues"](http://channel9.msdn.com/posts/ServiceBusTopicsAndQueues) video on the channel9 site.
- ["Securing Service Bus with ACS"](http://channel9.msdn.com/posts/Securing-Service-Bus-with-ACS) video on the channel9 site.

# GitHub
The source code of the tool is now availale on [GitHub](https://github.com/paolosalvatori/ServiceBusExplorer) as a public project. Now you have the opportunity to contribute to the evolution of the tool!

<a href="https://github.com/paolosalvatori/ServiceBusExplorer/releases" >![here](./media/download.png).</a>

# Videos
For more information on how to use the Service Bus Explorer, see the following videos on Channel9:

- [Getting Started with Service Bus. Part 3: Service Bus Explorer](http://www.digitalpodcast.com/items/10765228) by Clemens Vasters
- [Cross Platform Notifications using Windows Azure Notifications Hub](http://channel9.msdn.com/Shows/Cloud+Cover/Episode-116-Cross-Platform-Notifications-using-Windows-Azure-Notifications-Hub) by Elio Damaggio, Nick Harris and Chris Risner.

# Documentation
You can find the documentation for Service Bus Explorer [here](./docs/service-bus-explorer.md)

# Alternative Service Bus Management Tools
Service Bus Explorer is only on of the management tools available for Azure Service Bus.

Here are a couple of alternatives:

- **Microsoft Azure Management Portal** _(SaaS, web based, extremely basic)_
- **ServiceBus360** _(paid with free trial, SaaS, web based)_
- **PowerShell** _([Documentation](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-manage-with-ps))_

# License
Microsoft Corporation ("Microsoft") grants you a nonexclusive, perpetual, royalty-free right to use and modify the software code provided by us for the purposes of illustration ("Sample Code") and to reproduce and distribute the object code form of the Sample Code, provided that you agree: (i) to not use our name, logo, or trademarks to market your software product in which the Sample Code is embedded; (ii) to include a valid copyright notice on your software product in which the Sample  Code is embedded; and (iii) to indemnify, hold harmless, and defend us and our suppliers from and against any claims or lawsuits, whether in an action of contract, tort or otherwise, including attorneys' fees, that arise or result from the use or distribution  of the Sample Code or the use or other dealings in the Sample Code. Unless applicable law gives you more rights, Microsoft reserves all other rights not expressly granted herein, whether by implication, estoppel or otherwise.

THE SAMPLE CODE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL MICROSOFT OR ITS LICENSORS BE LIABLE  FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THE SAMPLE CODE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.