using System;
using System.Collections.Generic;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBusExplorer.Helpers
{
    public sealed class PartialMessageReadException : Exception
    {
        public IReadOnlyList<BrokeredMessage> Messages { get; }
        public PartialMessageReadException(List<BrokeredMessage> messages, Exception failure)
            : base($"Message read stopped after {messages.Count} messages. Already received rows are retained. {failure.Message}", failure)
        { Messages = messages.AsReadOnly(); }
    }
}
