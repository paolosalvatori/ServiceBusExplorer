using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Helpers
{
    public class EventHubClientHelper
    {
        private MessagingFactory factory;

        public EventHubClientHelper(string connectionString, string hubPath, bool ownSocket)
        {
            if (ownSocket)
            {
                this.factory = MessagingFactory.CreateFromConnectionString(connectionString);
                this.Client = this.factory.CreateEventHubClient(hubPath);
            }
            else
            {
                this.Client = EventHubClient.CreateFromConnectionString(connectionString, hubPath);
            }
        }

        public EventHubClient Client { get; private set; }

        public void Close()
        {
            if (this.Client != null)
            {
                this.Client.Close();
            }

            if (this.factory != null)
            {
                this.factory.Close();
            }
        }
    }
}
