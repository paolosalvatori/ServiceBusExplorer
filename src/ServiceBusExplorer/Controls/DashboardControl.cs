using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ServiceBusExplorer.Controls
{
    public class DashboardControl : UserControl
    {
        private DataGridView dataGridView;
        private Button refreshButton;
        private CheckBox autoRefreshCheckBox;
        private ComboBox intervalComboBox;
        private Label statusLabel;
        private Panel toolbarPanel;
        private Timer autoRefreshTimer;
        private ContextMenuStrip contextMenu;
        private Label hintLabel;
        private Panel warningPanel;

        private Func<IEnumerable<QueueDescription>> getQueues;
        private Func<IEnumerable<TopicDescription>> getTopics;
        private Func<string, IEnumerable<SubscriptionDescription>> getSubscriptions;
        private Action<string> writeToLog;
        private int isLoading;
        private List<DashboardRow> _allRows = new List<DashboardRow>();
        private CheckBox syncWithTreeViewCheckBox;

        public Action<string, string> OnRowSelected { get; set; }
        public Action<string, string> OnRowDoubleClicked { get; set; }
        public Func<string, string, System.Threading.Tasks.Task> OnRefreshRowRequested { get; set; }
        public Func<System.Threading.Tasks.Task> OnRefreshRequested { get; set; }
        public bool SyncWithTreeView => syncWithTreeViewCheckBox?.Checked ?? true;
        public Action OnSyncWithTreeViewChanged { get; set; }
        private Font headerFont;
        private Font cellFont;
        private Font totalFont;
        private Font hintFont;
        private Font warningFont;

        public DashboardControl()
        {
            InitializeControls();
        }

        public void Initialize(
            Func<IEnumerable<QueueDescription>> getQueuesFunc,
            Func<IEnumerable<TopicDescription>> getTopicsFunc,
            Func<string, IEnumerable<SubscriptionDescription>> getSubscriptionsFunc,
            Action<string> log)
        {
            getQueues = getQueuesFunc;
            getTopics = getTopicsFunc;
            getSubscriptions = getSubscriptionsFunc;
            writeToLog = log;
        }

        private void InitializeControls()
        {
            SuspendLayout();

            // Toolbar panel
            toolbarPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 36,
                Padding = new Padding(4)
            };

            refreshButton = new Button
            {
                Text = "Refresh",
                Location = new Point(4, 6),
                Size = new Size(75, 24),
                FlatStyle = FlatStyle.System
            };
            refreshButton.Click += async (s, e) => await RequestRefreshAsync();

            autoRefreshCheckBox = new CheckBox
            {
                Text = "Auto-refresh",
                Location = new Point(88, 8),
                AutoSize = true
            };
            autoRefreshCheckBox.CheckedChanged += AutoRefreshCheckBox_CheckedChanged;

            intervalComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(190, 6),
                Size = new Size(90, 24),
                Enabled = false
            };
            intervalComboBox.Items.AddRange(new object[] { "1min", "5min", "15min", "30min", "60min" });
            intervalComboBox.SelectedIndex = 0;
            intervalComboBox.SelectedIndexChanged += IntervalComboBox_SelectedIndexChanged;

            syncWithTreeViewCheckBox = new CheckBox
            {
                Text = "Sync with treeview",
                Location = new Point(290, 8),
                AutoSize = true,
                Checked = true
            };
            syncWithTreeViewCheckBox.CheckedChanged += (s, e) => OnSyncWithTreeViewChanged?.Invoke();

            statusLabel = new Label
            {
                Location = new Point(430, 10),
                AutoSize = true,
                ForeColor = SystemColors.GrayText
            };

            toolbarPanel.Controls.AddRange(new Control[] { refreshButton, autoRefreshCheckBox, intervalComboBox, syncWithTreeViewCheckBox, statusLabel });

            // Warning panel (shown when auto-refresh is enabled)
            warningPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 24,
                BackColor = Color.FromArgb(255, 243, 205),
                Visible = false,
                Padding = new Padding(8, 4, 0, 0)
            };
            var warningLabel = new Label
            {
                Text = "Auto refreshing messaging entities affects the performance of the Service Bus resource, especially when set to a high frequency.",
                Dock = DockStyle.Fill,
                ForeColor = Color.FromArgb(102, 77, 3),
                Font = warningFont,
                AutoSize = false
            };
            warningPanel.Controls.Add(warningLabel);

            // Fonts (disposed in Dispose)
            headerFont = new Font("Segoe UI", 9F, FontStyle.Bold);
            cellFont = new Font("Segoe UI", 9F);
            totalFont = new Font("Segoe UI", 9F, FontStyle.Bold);
            hintFont = new Font("Segoe UI", 8F);
            warningFont = new Font("Segoe UI", 8.5F);

            // DataGridView
            dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                BackgroundColor = SystemColors.Window,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = headerFont,
                    BackColor = Color.FromArgb(153, 180, 209),
                    ForeColor = Color.White,
                    SelectionBackColor = Color.FromArgb(153, 180, 209),
                    SelectionForeColor = Color.White
                },
                EnableHeadersVisualStyles = false,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = cellFont
                },
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(245, 245, 245)
                }
            };

            dataGridView.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { Name = "Name", HeaderText = "Name", FillWeight = 40 },
                new DataGridViewTextBoxColumn { Name = "Type", HeaderText = "Type", FillWeight = 10 },
                new DataGridViewTextBoxColumn { Name = "Active", HeaderText = "Active", FillWeight = 12, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } },
                new DataGridViewTextBoxColumn { Name = "DeadLetter", HeaderText = "Dead Letter", FillWeight = 12, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } },
                new DataGridViewTextBoxColumn { Name = "Scheduled", HeaderText = "Scheduled", FillWeight = 12, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } },
                new DataGridViewTextBoxColumn { Name = "Total", HeaderText = "Total", FillWeight = 14, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Font = totalFont } }
            });

            dataGridView.CellClick += DataGridView_CellClick;
            dataGridView.CellDoubleClick += DataGridView_CellDoubleClick;
            dataGridView.KeyDown += DataGridView_KeyDown;

            // Context menu
            contextMenu = new ContextMenuStrip();
            var refreshItem = new ToolStripMenuItem("Refresh");
            refreshItem.Click += async (s, e) => await RefreshSelectedRowAsync();
            var copyRowItem = new ToolStripMenuItem("Copy row");
            copyRowItem.Click += (s, e) => CopySelectedRow();
            var copyNameItem = new ToolStripMenuItem("Copy name");
            copyNameItem.Click += (s, e) => CopySelectedName();
            contextMenu.Items.AddRange(new ToolStripItem[] { refreshItem, new ToolStripSeparator(), copyRowItem, copyNameItem });
            contextMenu.Opening += ContextMenu_Opening;
            dataGridView.ContextMenuStrip = contextMenu;

            // Hint label
            hintLabel = new Label
            {
                Text = "Single click: sync tree  |  Double click: open in Explorer",
                Dock = DockStyle.Bottom,
                AutoSize = false,
                Height = 20,
                Font = hintFont,
                ForeColor = SystemColors.GrayText,
                Padding = new Padding(4, 2, 0, 0)
            };

            Controls.Add(dataGridView);
            Controls.Add(hintLabel);
            Controls.Add(warningPanel);
            Controls.Add(toolbarPanel);

            // Timer
            autoRefreshTimer = new Timer { Enabled = false };
            autoRefreshTimer.Tick += async (s, e) => await RequestRefreshAsync();

            ResumeLayout(false);
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (OnRowSelected == null) return;
            var id = GetRowIdentifier(e.RowIndex);
            if (id != null) OnRowSelected(id.Value.name, id.Value.type);
        }

        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (OnRowDoubleClicked == null) return;
            var id = GetRowIdentifier(e.RowIndex);
            if (id != null) OnRowDoubleClicked(id.Value.name, id.Value.type);
        }

        private (string name, string type)? GetRowIdentifier(int rowIndex)
        {
            if (rowIndex < 0) return null;
            var row = dataGridView.Rows[rowIndex];
            var name = row.Cells["Name"].Value?.ToString();
            var type = row.Cells["Type"].Value?.ToString();
            return string.IsNullOrEmpty(name) ? ((string, string)?)null : (name, type);
        }

        private void AutoRefreshCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            intervalComboBox.Enabled = autoRefreshCheckBox.Checked;
            autoRefreshTimer.Enabled = autoRefreshCheckBox.Checked;
            warningPanel.Visible = autoRefreshCheckBox.Checked;
            if (autoRefreshCheckBox.Checked)
            {
                autoRefreshTimer.Interval = GetIntervalMs();
                autoRefreshTimer.Start();
            }
            else
            {
                autoRefreshTimer.Stop();
            }
        }

        private void IntervalComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (autoRefreshTimer.Enabled)
            {
                autoRefreshTimer.Interval = GetIntervalMs();
            }
        }

        private int GetIntervalMs()
        {
            switch (intervalComboBox.SelectedIndex)
            {
                case 1: return 300000;
                case 2: return 900000;
                case 3: return 1800000;
                case 4: return 3600000;
                default: return 60000;
            }
        }

        private async System.Threading.Tasks.Task RequestRefreshAsync()
        {
            if (OnRefreshRequested != null)
            {
                await OnRefreshRequested();
            }
            else
            {
                await LoadDataAsync();
            }
        }

        public async System.Threading.Tasks.Task LoadDataAsync()
        {
            if (getQueues == null || getTopics == null || getSubscriptions == null) return;
            if (System.Threading.Interlocked.CompareExchange(ref isLoading, 1, 0) != 0) return;
            refreshButton.Enabled = false;
            statusLabel.Text = "Loading...";

            try
            {
                var errors = new List<string>();
                var rows = await System.Threading.Tasks.Task.Run(() =>
                {
                    var result = new List<DashboardRow>();

                    try
                    {
                        var queues = getQueues();
                        if (queues != null)
                        {
                            foreach (var q in queues)
                            {
                                var details = q.MessageCountDetails;
                                result.Add(new DashboardRow
                                {
                                    Name = q.Path,
                                    Type = "Queue",
                                    Active = details?.ActiveMessageCount ?? 0,
                                    DeadLetter = details?.DeadLetterMessageCount ?? 0,
                                    Scheduled = details?.ScheduledMessageCount ?? 0
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Dashboard: Error loading queues: {ex.Message}");
                    }

                    try
                    {
                        var topics = getTopics();
                        if (topics != null)
                        {
                            foreach (var t in topics)
                            {
                                try
                                {
                                    var subscriptions = getSubscriptions(t.Path);
                                    if (subscriptions != null)
                                    {
                                        foreach (var s in subscriptions)
                                        {
                                            var details = s.MessageCountDetails;
                                            result.Add(new DashboardRow
                                            {
                                                Name = $"{t.Path} / {s.Name}",
                                                Type = "Subscription",
                                                Active = details?.ActiveMessageCount ?? 0,
                                                DeadLetter = details?.DeadLetterMessageCount ?? 0,
                                                Scheduled = details?.ScheduledMessageCount ?? 0
                                            });
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    errors.Add($"Dashboard: Error loading subscriptions for {t.Path}: {ex.Message}");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Dashboard: Error loading topics: {ex.Message}");
                    }

                    return result;
                });

                // Log errors on UI thread (after await resumes on UI context)
                foreach (var error in errors)
                {
                    writeToLog?.Invoke(error);
                }

                _allRows = rows;
                PopulateGrid(rows);
                statusLabel.Text = $"Last refresh: {DateTime.Now:HH:mm:ss} — {rows.Count} items";
            }
            catch (Exception ex)
            {
                statusLabel.Text = $"Error: {ex.Message}";
                writeToLog?.Invoke($"Dashboard: {ex.Message}");
            }
            finally
            {
                refreshButton.Enabled = true;
                System.Threading.Interlocked.Exchange(ref isLoading, 0);
            }
        }

        public void ApplyFilter(string filterText)
        {
            if (!syncWithTreeViewCheckBox.Checked || string.IsNullOrWhiteSpace(filterText))
            {
                PopulateGrid(_allRows);
                return;
            }
            var filtered = _allRows
                .Where(r => r.Name.IndexOf(filterText, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
            PopulateGrid(filtered);
        }

        private void PopulateGrid(List<DashboardRow> rows)
        {
            var sortColumn = dataGridView.SortedColumn;
            var sortOrder = dataGridView.SortOrder;

            dataGridView.Rows.Clear();
            foreach (var row in rows.OrderBy(r => r.Type).ThenBy(r => r.Name))
            {
                var idx = dataGridView.Rows.Add(row.Name, row.Type, row.Active, row.DeadLetter, row.Scheduled, row.Total);

                if (row.DeadLetter > 0)
                {
                    dataGridView.Rows[idx].DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 230);
                }
            }

            if (sortColumn != null && sortOrder != SortOrder.None)
            {
                dataGridView.Sort(sortColumn, sortOrder == SortOrder.Ascending
                    ? System.ComponentModel.ListSortDirection.Ascending
                    : System.ComponentModel.ListSortDirection.Descending);
            }
        }

        private void DataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                CopySelectedRow();
            }
        }

        private void ContextMenu_Opening(object sender, CancelEventArgs e)
        {
            var hasSelection = dataGridView.SelectedRows.Count > 0;
            contextMenu.Items[0].Enabled = hasSelection && OnRefreshRowRequested != null;
        }

        private async System.Threading.Tasks.Task RefreshSelectedRowAsync()
        {
            if (dataGridView.SelectedRows.Count == 0 || OnRefreshRowRequested == null) return;
            var row = dataGridView.SelectedRows[0];
            var name = row.Cells["Name"].Value?.ToString();
            var type = row.Cells["Type"].Value?.ToString();
            if (!string.IsNullOrEmpty(name))
            {
                await OnRefreshRowRequested(name, type);
            }
        }

        private void CopySelectedRow()
        {
            if (dataGridView.SelectedRows.Count == 0) return;
            var row = dataGridView.SelectedRows[0];
            var values = new string[row.Cells.Count];
            for (int i = 0; i < row.Cells.Count; i++)
            {
                values[i] = row.Cells[i].Value?.ToString() ?? "";
            }
            Clipboard.SetText(string.Join("\t", values));
        }

        private void CopySelectedName()
        {
            if (dataGridView.SelectedRows.Count == 0) return;
            var name = dataGridView.SelectedRows[0].Cells["Name"].Value?.ToString();
            if (!string.IsNullOrEmpty(name))
            {
                Clipboard.SetText(name);
            }
        }

        public void UpdateRow(string entityName, long active, long deadLetter, long scheduled)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (string.Equals(row.Cells["Name"].Value?.ToString(), entityName, StringComparison.OrdinalIgnoreCase))
                {
                    row.Cells["Active"].Value = active;
                    row.Cells["DeadLetter"].Value = deadLetter;
                    row.Cells["Scheduled"].Value = scheduled;
                    row.Cells["Total"].Value = DashboardRow.ComputeTotal(active, deadLetter, scheduled);

                    // Update color coding
                    row.DefaultCellStyle.BackColor = deadLetter > 0
                        ? Color.FromArgb(255, 235, 230)
                        : Color.Empty;
                    return;
                }
            }
        }

        public void RemoveRow(string entityName)
        {
            for (int i = dataGridView.Rows.Count - 1; i >= 0; i--)
            {
                if (string.Equals(dataGridView.Rows[i].Cells["Name"].Value?.ToString(), entityName, StringComparison.OrdinalIgnoreCase))
                {
                    dataGridView.Rows.RemoveAt(i);
                    return;
                }
            }
        }

        public void RemoveRowsWithPrefix(string topicPath)
        {
            var prefix = topicPath + " / ";
            for (int i = dataGridView.Rows.Count - 1; i >= 0; i--)
            {
                var name = dataGridView.Rows[i].Cells["Name"].Value?.ToString();
                if (name != null && name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                {
                    dataGridView.Rows.RemoveAt(i);
                }
            }
        }

        public void AddRow(string name, string type)
        {
            // Idempotent: skip if row already exists
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (string.Equals(row.Cells["Name"].Value?.ToString(), name, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }

            // Find sorted insert position (same order as PopulateGrid: Type then Name)
            int insertIndex = dataGridView.Rows.Count;
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                var rowType = dataGridView.Rows[i].Cells["Type"].Value?.ToString() ?? "";
                var rowName = dataGridView.Rows[i].Cells["Name"].Value?.ToString() ?? "";
                var cmpType = string.Compare(type, rowType, StringComparison.OrdinalIgnoreCase);
                if (cmpType < 0 || (cmpType == 0 && string.Compare(name, rowName, StringComparison.OrdinalIgnoreCase) < 0))
                {
                    insertIndex = i;
                    break;
                }
            }
            dataGridView.Rows.Insert(insertIndex, name, type, 0L, 0L, 0L, 0L);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                autoRefreshTimer?.Stop();
                autoRefreshTimer?.Dispose();
                contextMenu?.Dispose();
                headerFont?.Dispose();
                cellFont?.Dispose();
                totalFont?.Dispose();
                hintFont?.Dispose();
                warningFont?.Dispose();
            }
            base.Dispose(disposing);
        }

        private class DashboardRow
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public long Active { get; set; }
            public long DeadLetter { get; set; }
            public long Scheduled { get; set; }
            public long Total => ComputeTotal(Active, DeadLetter, Scheduled);
            public static long ComputeTotal(long active, long deadLetter, long scheduled) => active + deadLetter + scheduled;
        }
    }
}
