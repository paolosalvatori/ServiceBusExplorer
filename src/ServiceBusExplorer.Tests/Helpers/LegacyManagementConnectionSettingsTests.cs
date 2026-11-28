using System;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class LegacyManagementConnectionSettingsTests
    {
        [Theory]
        [InlineData("Endpoint=sb://localhost/;SharedAccessKeyName=name;SharedAccessKey=abc+/=;TransportType=Amqp")]
        [InlineData("endpoint=sb://localhost/;SharedAccessSignature=SharedAccessSignature sr=scope&sig=abc%3D&se=1&skn=name")]
        [InlineData("Endpoint=sb://localhost/;RuntimePort=5672;ManagementPort=443;OperationTimeout=00:01:00;EntityPath=hub")]
        public void KnownSettingsPreservePaddedKeysAndScopedTokens(string connection)
        {
            LegacyManagementConnectionSettings.Validate(connection);
        }

        [Theory]
        [InlineData("UnsupportedSetting=secret-sentinel")]
        [InlineData("secret-sentinel")]
        [InlineData("=secret-sentinel")]
        public void UnsupportedSettingsRejectWithoutDisclosingValues(string setting)
        {
            var exception = Assert.Throws<ArgumentException>(() =>
                LegacyManagementConnectionSettings.Validate("Endpoint=sb://localhost/;" + setting));
            Assert.DoesNotContain("secret-sentinel", exception.ToString());
        }
    }
}
