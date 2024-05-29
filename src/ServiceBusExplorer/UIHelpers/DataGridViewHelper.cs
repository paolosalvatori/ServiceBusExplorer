using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using Microsoft.ServiceBus.Messaging;

namespace ServiceBusExplorer.UIHelpers
{
    internal static class DataGridViewHelper
    {
        static public void RemoveDataGridRowsUsingSequenceNumbers(DataGridView dataGridView,
            IEnumerable<long> sequenceNumbersToRemove)
        {
            var rowsToRemove = new List<DataGridViewRow>(sequenceNumbersToRemove.Count());

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                var message = (BrokeredMessage)row.DataBoundItem;

                if (sequenceNumbersToRemove.Contains(message.SequenceNumber))
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
