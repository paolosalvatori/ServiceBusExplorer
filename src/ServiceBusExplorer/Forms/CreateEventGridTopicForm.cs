// // Auto-added comment

// using ServiceBusExplorer.Utilities.Helpers;
// using System;
// using System.Collections.Generic;
// using System.ComponentModel;
// using System.Data;
// using System.Diagnostics.Tracing;
// using System.Drawing;
// using System.Globalization;
// using System.Linq;
// using System.Reflection;
// using System.Runtime.ExceptionServices;
// using System.Text;
// using System.Threading.Tasks;
// using System.Windows.Forms;

// namespace ServiceBusExplorer.Forms
// {
//     public partial class CreateEventGridTopicForm : Form
//     {
//         #region Private Constants
//         private const string ExceptionFormat = "Exception: {0}";
//         private readonly WriteToLogDelegate writeToLog = default!;
//         #endregion

//         #region Public Fields
//         public string TopicName;
//         #endregion

//         public CreateEventGridTopicForm(WriteToLogDelegate writeToLog)
//         {
//             this.writeToLog = writeToLog;
//             InitializeComponent();
//         }

//         private void btnOk_Click(object sender, EventArgs e)
//         {
//             try
//             {
//                 TopicName = txtTopicName.Text;
//             }
//             catch (Exception ex)
//             {
//                 HandleException(ex);
//             }

//             DialogResult = DialogResult.OK;
//         }

//         private void btnCancel_Click(object sender, EventArgs e)
//         {
//             DialogResult = DialogResult.Cancel;
//             Close();
//         }

//         private void HandleException(Exception ex)
//         {
//             if (string.IsNullOrWhiteSpace(ex?.Message))
//             {
//                 return;
//             }

//             writeToLog(string.Format(CultureInfo.CurrentCulture, ExceptionFormat, ex?.Message));
//         }
//     }
// }
