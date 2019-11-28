namespace Microsoft.Azure.ServiceBusExplorer.Tests.Helpers
{
    using System.Threading.Tasks;
    using NUnit.Framework;
    using ServiceBusExplorer.Helpers;

    [TestFixture]
    public class ServiceBusNamespaceTests
    {
        [TestCase("endpoint=sb://NAMESPACE.servicebus.windows.net/;clientId=XXX;clientSecret=XXX;tenantId=XXX", true)]
        [TestCase("endpoint=sb://NAMESPACE.servicebus.windows.net/;sharedSecretIssuer=owner;sharedSecretValue=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=", false)]
        public async Task Can_detect_AAD_connection_string(string connectionString, bool expectedResult)
        {
            var result = ServiceBusNamespace.GetServiceBusNamespace("key", connectionString, (message, async) => { });

            Assert.AreEqual(expectedResult, result.UsingAadAuthentication());
        }
    }
}
