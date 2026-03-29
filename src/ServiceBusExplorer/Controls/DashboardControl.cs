using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ServiceBusExplorer.Controls
{
    public partial class DashboardControl : UserControl
    {
        private DataGridView dataGridView;
        private Button refreshButton;
        private CheckBox autoRefreshCheckBox;
        private ComboBox intervalComboBox;
        private Label statusLabel;
        private Panel toolbarPanel;
        private Timer autoRefreshTimer;

        private Func<IEnumerable<QueueDescription>> getQueues;
        private Func<IEnumerable<TopicDescription>> getTopics;
        private Action<string> writeToLog;

        public DashboardControl()
        {
            InitializeControls();
        }

        public void Initialize(
            Func<IEnumerable<QueueDescription>> getQueuesFunc,
            Func<IEnumerable<TopicDescription>> getTopicsFunc,
            Action<string> log)
        {
            getQueues = getQueuesFunc;
            getTopics = getTopicsFunc;
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
            refreshButton.Click += (s, e) => LoadDataAsync();

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
                Size = new Size(80, 24),
                Enabled = false
            };
            intervalComboBox.Items.AddRange(new object[] { "30s", "60s", "5min" });
            intervalComboBox.SelectedIndex = 1;
            intervalComboBox.SelectedIndexChanged += IntervalComboBox_SelectedIndexChanged;

            statusLabel = new Label
            {
                Location = new Point(280, 10),
                AutoSize = true,
                ForeColor = SystemColors.GrayText
            };

            toolbarPanel.Controls.AddRange(new Control[] { refreshButton, autoRefreshCheckBox, intervalComboBox, statusLabel });

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
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    BackColor = Color.FromArgb(153, 180, 209),
                    ForeColor = Color.White,
                    SelectionBackColor = Color.FromArgb(153, 180, 209),
                    SelectionForeColor = Color.White
                },
                EnableHeadersVisualStyles = false,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 9F)
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
                new DataGridViewTextBoxColumn { Name = "Total", HeaderText = "Total", FillWeight = 14, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Font = new Font("Segoe UI", 9F, FontStyle.Bold) } }
            });

            Controls.Add(dataGridView);
            Controls.Add(toolbarPanel);

            // Timer
            autoRefreshTimer = new Timer { Enabled = false };
            autoRefreshTimer.Tick += (s, e) => LoadDataAsync();

            ResumeLayout(false);
        }

        private void AutoRefreshCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            intervalComboBox.Enabled = autoRefreshCheckBox.Checked;
            autoRefreshTimer.Enabled = autoRefreshCheckBox.Checked;
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
                case 0: return 30000;
                case 2: return 300000;
                default: return 60000;
            }
        }

        public async void LoadDataAsync()
        {
            if (getQueues == null || getTopics == null) return;

            refreshButton.Enabled = false;
            statusLabel.Text = "Loading...";

            try
            {
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
                        writeToLog?.Invoke($"Dashboard: Error loading queues: {ex.Message}");
                    }

                    try
                    {
                        var topics = getTopics();
                        if (topics != null)
                        {
                            foreach (var t in topics)
                            {
                                var details = t.MessageCountDetails;
                                result.Add(new DashboardRow
                                {
                                    Name = t.Path,
                                    Type = "Topic",
                                    Active = details?.ActiveMessageCount ?? 0,
                                    DeadLetter = details?.DeadLetterMessageCount ?? 0,
                                    Scheduled = details?.ScheduledMessageCount ?? 0
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        writeToLog?.Invoke($"Dashboard: Error loading topics: {ex.Message}");
                    }

                    return result;
                });

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
            }
        }

        private void PopulateGrid(List<DashboardRow> rows)
        {
            dataGridView.Rows.Clear();
            foreach (var row in rows.OrderBy(r => r.Type).ThenBy(r => r.Name))
            {
                var total = row.Active + row.DeadLetter + row.Scheduled;
                var idx = dataGridView.Rows.Add(row.Name, row.Type, row.Active, row.DeadLetter, row.Scheduled, total);

                if (row.DeadLetter > 0)
                {
                    dataGridView.Rows[idx].DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 230);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                autoRefreshTimer?.Stop();
                autoRefreshTimer?.Dispose();
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
        }
    }
}
