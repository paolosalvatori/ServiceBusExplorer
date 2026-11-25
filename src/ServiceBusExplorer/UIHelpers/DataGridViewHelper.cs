using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Helpers;

namespace ServiceBusExplorer.UIHelpers
{
    internal static class DataGridViewHelper
    {
        internal static List<long?> GetSourceSequenceNumbers(IEnumerable<BrokeredMessage> messages) =>
            messages.Select(message => message == null ? (long?)null : ServiceBusReceivedMessageAdapter.GetSourceSequenceNumber(message)).ToList();

        static public void RemoveDataGridRowsUsingSequenceNumbers(DataGridView dataGridView,
            IEnumerable<long> sequenceNumbersToRemove)
        {
            var rowsToRemove = new List<DataGridViewRow>(sequenceNumbersToRemove.Count());

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                var message = (BrokeredMessage)row.DataBoundItem;

                if (message != null && sequenceNumbersToRemove.Contains(ServiceBusReceivedMessageAdapter.GetSourceSequenceNumber(message)))
                {
                    rowsToRemove.Add(row);

                    if (rowsToRemove.Count >= sequenceNumbersToRemove.Count())
                    {
                        break;
                    }
                }
            }

            for (var rowIndex = rowsToRemove.Count - 1; rowIndex >= 0; --rowIndex)
            {
                var row = rowsToRemove[rowIndex];
                dataGridView.Rows.Remove(row);
            }

            dataGridView.ClearSelection();
        }
    }
}
