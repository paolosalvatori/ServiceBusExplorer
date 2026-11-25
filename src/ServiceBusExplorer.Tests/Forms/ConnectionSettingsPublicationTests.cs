using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.ServiceBus;
using ServiceBusExplorer.Forms;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Forms
{
    [CollectionDefinition("Connection settings publication", DisableParallelization = true)]
    public class ConnectionSettingsPublicationCollection { }

    [Collection("Connection settings publication")]
    public class ConnectionSettingsPublicationTests
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void RejectedConnectionPreservesSettingsAndSelectedEntities(bool throws)
        {
            var oldMode = ServiceBusHelper.ConnectivityMode;
            var oldWebSockets = ServiceBusHelper.UseAmqpWebSockets;
            try
            {
                ServiceBusHelper.ConnectivityMode = ConnectivityMode.Tcp;
                ServiceBusHelper.UseAmqpWebSockets = false;
                var main = (MainForm)FormatterServices.GetUninitializedObject(typeof(MainForm));
                var original = new List<string> { "original" };
                typeof(MainForm).GetProperty(nameof(MainForm.SelectedEntities)).SetValue(main, original);
                Func<bool> reject = () =>
                {
                    Assert.True(ServiceBusHelper.UseAmqpWebSockets);
                    if (throws) throw new InvalidOperationException("synthetic rejection");
                    return false;
                };
                if (throws) Assert.Throws<InvalidOperationException>(() => main.ConnectWithSettings(reject, ConnectivityMode.Http, true, new List<string> { "replacement" }));
                else Assert.False(main.ConnectWithSettings(reject, ConnectivityMode.Http, true, new List<string> { "replacement" }));
                Assert.False(ServiceBusHelper.UseAmqpWebSockets);
                Assert.Equal(ConnectivityMode.Tcp, ServiceBusHelper.ConnectivityMode);
                Assert.Same(original, main.SelectedEntities);
            }
            finally { ServiceBusHelper.ConnectivityMode = oldMode; ServiceBusHelper.UseAmqpWebSockets = oldWebSockets; }
        }

        [Fact]
        public void NamedSettingUpdatePreservesMatchingValuesAndEqualsSigns()
        {
            const string original = "Endpoint=sb://NetMessaging9354/;SharedAccessKey=NetMessaging9354==;EntityPath=NetMessaging; TransportType=NetMessaging;RuntimePort=9354;";
            var result = ConnectForm.ReplaceConnectionStringSetting(original, "transporttype", "Amqp", true);
            result = ConnectForm.ReplaceConnectionStringSetting(result, "runtimeport", "5671", false);
            Assert.Equal("Endpoint=sb://NetMessaging9354/;SharedAccessKey=NetMessaging9354==;EntityPath=NetMessaging; TransportType=Amqp;RuntimePort=5671;", result);
        }
    }
}
