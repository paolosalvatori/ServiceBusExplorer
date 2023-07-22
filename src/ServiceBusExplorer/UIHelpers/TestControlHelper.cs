using ServiceBusExplorer.Forms;
using ServiceBusExplorer.Utilities.Helpers;

using System;
using System.Threading.Tasks;

namespace ServiceBusExplorer.UIHelpers
{
    internal class TestControlHelper
    {
        #region Internal Constants
        //***************************
        // Formats
        //***************************
        internal const string ExceptionFormat = "Exception: {0}";
        internal const string InnerExceptionFormat = "InnerException: {0}";
        internal const string LabelFormat = "{0:0.000}";
        #endregion

        #region Internal instance properties
        internal bool IsReadyToStoreMessageText { get; set; }
        internal ServiceBusHelper ServiceBusHelper { get; }
        internal MainForm MainForm { get; }
        internal WriteToLogDelegate WriteToLog { get; }
        internal Action StartLog { get; }
        public Func<Task> StopLog { get; }
        #endregion

        #region Public Constructors
        public TestControlHelper(MainForm mainForm,
                                WriteToLogDelegate writeToLog,
                                Func<Task> stopLog,
                                Action startLog,
                                ServiceBusHelper serviceBusHelper)
        {
            MainForm = mainForm ?? throw new ArgumentNullException(nameof(mainForm));
            WriteToLog = writeToLog;
            StopLog = stopLog;
            StartLog = startLog;
            ServiceBusHelper = serviceBusHelper;
        }
        #endregion

        #region Internal Methods
        internal void OnMessageTextChanged(string text)
        {
            if (IsReadyToStoreMessageText)
            {
                MainForm.MessageText = text;
            }
        }
        #endregion
    }
}
