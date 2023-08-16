using ServiceBusExplorer.Forms;
using ServiceBusExplorer.Utilities.Helpers;

using System;
using System.Threading.Tasks;

namespace ServiceBusExplorer.UIHelpers
{
    internal class TestControlHelper
    {
        #region Public Constants
        //***************************
        // Formats
        //***************************
        public const string ExceptionFormat = "Exception: {0}";
        public const string InnerExceptionFormat = "InnerException: {0}";
        public const string LabelFormat = "{0:0.000}";
        #endregion

        #region Public instance properties
        public bool IsReadyToStoreMessageText { get; set; }
        public ServiceBusHelper ServiceBusHelper { get; }
        public MainForm MainForm { get; }
        public WriteToLogDelegate WriteToLog { get; }
        public Action StartLog { get; }
        public Func<Task> StopLog { get; }
        #endregion

        #region Public constructor
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

        #region Public methods
        public void OnMessageTextChanged(string text)
        {
            if (IsReadyToStoreMessageText)
            {
                MainForm.MessageText = text;
            }
        }
        #endregion
    }
}
