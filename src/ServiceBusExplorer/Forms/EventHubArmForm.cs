using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Azure;
using Azure.ResourceManager.EventHubs;
using ServiceBusExplorer.Helpers;

namespace ServiceBusExplorer.Forms
{
    internal sealed class EventHubArmForm : ThemedForm
    {
        private readonly EventHubArmManagement management;
        private readonly Label hubSummary = new Label { AutoSize = true };
        private readonly Label status = new Label { AutoSize = true };
        private readonly ListView groups = new ListView { Name = "consumerGroups", AccessibleName = "Consumer groups", Dock = DockStyle.Fill, View = View.Details, FullRowSelect = true, MultiSelect = false, HideSelection = false };
        private readonly TextBox name = new TextBox { Name = "groupName", AccessibleName = "Consumer group name", Dock = DockStyle.Fill, MaxLength = 0 };
        private readonly TextBox metadata = new TextBox { Name = "groupMetadata", AccessibleName = "User metadata", Dock = DockStyle.Fill, Multiline = true, ScrollBars = ScrollBars.Vertical, MaxLength = 0 };
        private readonly Button refresh = new Button { AutoSize = true, Text = "Refresh" };
        private readonly Button save = new Button { AutoSize = true, Text = "Create or update group…" };
        private CancellationTokenSource pending;
        private bool saving;
        private bool loaded;
        private bool keepDraft;
        private readonly Func<bool> confirmCloseDuringWrite;

        internal EventHubArmForm(EventHubArmManagement management, Func<bool> confirmCloseDuringWrite = null)
        {
            this.management = management;
            this.confirmCloseDuringWrite = confirmCloseDuringWrite ?? (() => MessageBox.Show(this,
                    "A write is pending and may already have reached Azure. Close this window and cancel the wait? Refresh the group before retrying.",
                    "Unconfirmed ARM write", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes);
            EntraWindowOwners.Register(this);
            Text = "Event Hub — Azure Resource Manager";
            StartPosition = FormStartPosition.CenterParent;
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(760, 495);
            MinimumSize = new Size(540, 420);

            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(10), ColumnCount = 1, RowCount = 10 };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            for (var i = 0; i < layout.RowCount; i++)
                layout.RowStyles.Add(i == 3 ? new RowStyle(SizeType.Percent, 100) :
                    i == 7 ? new RowStyle(SizeType.Absolute, 64) : new RowStyle(SizeType.AutoSize));
            layout.Controls.Add(new Label { Text = "Event Hub resource ID", AutoSize = true }, 0, 0);
            layout.Controls.Add(new TextBox { Text = management.ResourceId, ReadOnly = true, Dock = DockStyle.Fill }, 0, 1);
            layout.Controls.Add(hubSummary, 0, 2);
            groups.Columns.Add("Consumer group", 170);
            groups.Columns.Add("User metadata", 310);
            groups.Columns.Add("Updated (UTC)", 165);
            layout.Controls.Add(groups, 0, 3);
            layout.Controls.Add(new Label { Text = "Consumer group name", AutoSize = true }, 0, 4);
            layout.Controls.Add(name, 0, 5);
            layout.Controls.Add(new Label { Text = "User metadata (empty clears the value)", AutoSize = true }, 0, 6);
            layout.Controls.Add(metadata, 0, 7);
            var actions = new FlowLayoutPanel { AutoSize = true, Dock = DockStyle.Fill };
            actions.Controls.Add(refresh);
            actions.Controls.Add(save);
            layout.Controls.Add(actions, 0, 8);
            layout.Controls.Add(status, 0, 9);
            Controls.Add(layout);

            groups.SelectedIndexChanged += (_, __) =>
            {
                if (groups.SelectedItems.Count == 0) return;
                var row = groups.SelectedItems[0];
                name.Text = row.Name;
                metadata.Text = row.SubItems[1].Text;
            };
            Shown += async (_, __) => await RefreshGroupsAsync();
            refresh.Click += async (_, __) => await RefreshGroupsAsync();
            save.Click += async (_, __) => await SaveGroupAsync(groupName =>
                MessageBox.Show(this, "Create this consumer group if missing, or replace its user metadata if it exists?\n\n" +
                    management.ResourceId + "/consumergroups/" + groupName, "Confirm ARM write", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes);
            name.TextChanged += (_, __) => UpdateButtons();
            UpdateButtons();
        }

        internal Task RefreshGroupsAsync() => RunAsync(false, async token =>
        {
            var draftName = name.Text;
            var draftMetadata = metadata.Text;
            var hub = await management.GetHubAsync(token);
            var current = await management.GetGroupsAsync(token);
            token.ThrowIfCancellationRequested();
            hubSummary.Text = $"{hub.Name}   •   Status: {hub.Status}   •   Partitions: {hub.PartitionCount}";
            groups.BeginUpdate();
            try
            {
                groups.Items.Clear();
                foreach (var group in current)
                    groups.Items.Add(GroupItem(group, group.UserMetadata));
            }
            finally { groups.EndUpdate(); }
            name.Text = keepDraft ? draftName : string.Empty;
            metadata.Text = keepDraft ? draftMetadata : string.Empty;
            loaded = true;
            status.Text = $"{current.Count} consumer group(s)";
        });

        internal Task SaveGroupAsync(Func<string, bool> confirm)
        {
            var groupName = name.Text;
            var value = metadata.Text;
            if (!loaded || pending != null) return Task.CompletedTask;
            try { EventHubArmManagement.ValidateGroupMetadata(groupName, value); }
            catch (ArgumentException ex)
            {
                status.Text = ex.Message;
                return Task.CompletedTask;
            }
            if (!confirm(groupName))
                return Task.CompletedTask;

            return RunAsync(true, async token =>
            {
                var updated = await management.SetGroupMetadataAsync(groupName, value, token);
                token.ThrowIfCancellationRequested();
                // Some successful PUT responses omit userMetadata. Keep the acknowledged input until refresh.
                var row = GroupItem(updated, updated.UserMetadata ?? value);
                var index = groups.Items.IndexOfKey(updated.Name);
                if (index < 0) groups.Items.Add(row);
                else groups.Items[index] = row;
                keepDraft = false;
                row.Selected = true;
                status.Text = $"ARM accepted the metadata for {updated.Name}. Refresh to read it back.";
            });
        }

        private static ListViewItem GroupItem(EventHubsConsumerGroupData group, string userMetadata)
        {
            return new ListViewItem(new[] { group.Name, userMetadata ?? string.Empty,
                group.UpdatedOn?.UtcDateTime.ToString("u") ?? string.Empty }) { Name = group.Name };
        }

        private async Task RunAsync(bool write, Func<CancellationToken, Task> operation)
        {
            if (pending != null) return;
            using (var request = new CancellationTokenSource())
            {
                pending = request;
                saving = write;
                UpdateButtons();
                status.Text = write ? "Saving consumer group metadata…" : "Reading from Azure Resource Manager…";
                try { await operation(request.Token); }
                catch (OperationCanceledException) when (request.IsCancellationRequested) { }
                catch (Exception ex)
                {
                    if (!IsDisposed)
                    {
                        if (write) keepDraft = true;
                        loaded = false;
                        var reason = ex is RequestFailedException arm ? $"ARM {arm.Status} {arm.ErrorCode}" : ex.Message;
                        status.Text = (write ? "Write not confirmed. Refresh before retrying. " : "Refresh failed. ") + reason;
                    }
                }
                finally
                {
                    pending = null;
                    saving = false;
                    if (!IsDisposed) UpdateButtons();
                }
            }
        }

        private void UpdateButtons()
        {
            refresh.Enabled = pending == null;
            groups.Enabled = name.Enabled = metadata.Enabled = loaded && pending == null;
            save.Enabled = name.Enabled && !string.IsNullOrWhiteSpace(name.Text);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (!e.Cancel && saving && e.CloseReason == CloseReason.UserClosing)
                e.Cancel = !confirmCloseDuringWrite();
            if (!e.Cancel) pending?.Cancel();
        }
    }
}
