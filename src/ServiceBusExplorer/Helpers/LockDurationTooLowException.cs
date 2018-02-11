using System;
using System.Runtime.Serialization;

namespace Microsoft.Azure.ServiceBusExplorer.Helpers
{
    [Serializable]
    internal class LockDurationTooLowException : Exception
    {
        public LockDurationTooLowException()
        {
        }

        public LockDurationTooLowException(string message) : base(message)
        {
        }

        public LockDurationTooLowException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LockDurationTooLowException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}