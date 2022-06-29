
#region Using Directives
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Helpers;
#endregion

namespace ServiceBusExplorer.Common.Helpers
{
    public class FilteredBrokeredMessageInspector : IBrokeredMessageInspector
    {
        #region Private instance fields
        private readonly string _filter;
        private IBrokeredMessageInspector _messageInspector;
        #endregion

        public FilteredBrokeredMessageInspector(string filter, IBrokeredMessageInspector messageInspector)
        {
            _filter = filter;
            _messageInspector = messageInspector;
        }

        public BrokeredMessage BeforeSendMessage(BrokeredMessage message)
        {
            return _messageInspector == null ? message : _messageInspector.BeforeSendMessage(message);
        }

        public BrokeredMessage AfterReceiveMessage(BrokeredMessage message)
        {
            // TODO: Should we be before/after the other message inspector?
            _messageInspector?.AfterReceiveMessage(message);

            return Filter(message);
        }

        private BrokeredMessage Filter(BrokeredMessage message)
        {
            var contents = message.GetMessageText();

            if (contents == null || contents == BrokeredMessageExtensions.UnableToReadMessageBody)
            {
                // Fail the filter if we can't read
                return null;
            }

            // Better filter mechanism e.g. regex etc - switch?
            return contents.Contains(_filter) ? message : null;
        }
    }
}
