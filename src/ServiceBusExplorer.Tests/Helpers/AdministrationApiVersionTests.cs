using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ServiceBusExplorer.ServiceBus.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class AdministrationApiVersionTests
    {
        [Fact]
        public async Task EntityProbeKeepsPreviousAdministrationApiWithoutABroker()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            try
            {
                var port = ((IPEndPoint)listener.LocalEndpoint).Port;
                var helper = new ServiceBusHelper2((_, __) => { })
                {
                    AdministrationConnectionString = $"Endpoint=sb://127.0.0.1:{port};SharedAccessKeyName=name;SharedAccessKey=synthetic;UseDevelopmentEmulator=true"
                };
                var accept = listener.AcceptTcpClientAsync();
                var probe = helper.IsQueue("queue");
                Assert.Same(accept, await Task.WhenAny(accept, Task.Delay(TimeSpan.FromSeconds(10))));
                using var connection = await accept;
                using var stream = connection.GetStream();
                using var reader = new StreamReader(stream, Encoding.ASCII, false, 1024, true);
                var read = reader.ReadLineAsync();
                Assert.Same(read, await Task.WhenAny(read, Task.Delay(TimeSpan.FromSeconds(10))));
                var request = await read;
                Assert.Contains("api-version=2021-05", request);
                while (!string.IsNullOrEmpty(await reader.ReadLineAsync())) { }
                var response = Encoding.ASCII.GetBytes("HTTP/1.1 404 Not Found\r\nContent-Length: 0\r\nConnection: close\r\n\r\n");
                await stream.WriteAsync(response, 0, response.Length);
                Assert.Same(probe, await Task.WhenAny(probe, Task.Delay(TimeSpan.FromSeconds(10))));
                Assert.False(await probe);
            }
            finally { listener.Stop(); }
        }
    }
}
