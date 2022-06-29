using System;

using ServiceBusExplorer.Common.Helpers;
using ServiceBusExplorer.Forms;
using ServiceBusExplorer.Helpers;

namespace ServiceBusExplorer
{
    internal static class FormExtensions
    {
        public static IBrokeredMessageInspector ToBrokeredMessageInspector(this ReceiveModeForm receiveModeForm, ServiceBusHelper serviceBusHelper)
        {

            var messageInspector = !string.IsNullOrEmpty(receiveModeForm.Inspector) &&
                                   serviceBusHelper.BrokeredMessageInspectors.ContainsKey(
                                       receiveModeForm.Inspector)
                ? Activator.CreateInstance(serviceBusHelper.BrokeredMessageInspectors[receiveModeForm.Inspector])
                    as IBrokeredMessageInspector
                : null;

            if (!string.IsNullOrEmpty(receiveModeForm.Filter))
            {
                // Wrap the current inspector in a filter
                messageInspector = new FilteredBrokeredMessageInspector(receiveModeForm.Filter, messageInspector);
            }

            return messageInspector;
        }
    }
}
