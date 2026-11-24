using System;
using FluentAssertions;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class EventHubConnectionValidationTests
    {
        private const string Compatible = "Endpoint=sb://example.servicebus.windows.net/;SharedAccessKeyName=service;SharedAccessKey=c2VjcmV0";

        [Theory]
        [InlineData(Compatible)]
        [InlineData(Compatible + ";EntityPath=iothub-ehub-example")]
        public void AcceptsCompatibleEndpoint(string connectionString)
        {
            Action validate = () => EventHubConnectionValidation.ValidateIoTHubEndpoint(connectionString, "iothub-ehub-example");
            validate.Should().NotThrow();
        }

        [Theory]
        [InlineData("HostName=example.azure-devices.net;SharedAccessKeyName=service;SharedAccessKey=secret", "hub")]
        [InlineData(Compatible, "messages/events")]
        [InlineData(Compatible + ";EntityPath=another-hub", "hub")]
        [InlineData("not-a-connection-string", "hub")]
        [InlineData(Compatible, "")]
        public void RejectsIncompatibleInputsWithoutLeakingCredentials(string connectionString, string hubName)
        {
            Action validate = () => EventHubConnectionValidation.ValidateIoTHubEndpoint(connectionString, hubName);
            var exception = validate.Should().Throw<ArgumentException>().Which;
            exception.Message.Should().Contain("Built-in endpoints").And.NotContain("SharedAccessKey=");
            exception.InnerException.Should().BeNull();
        }
    }
}
