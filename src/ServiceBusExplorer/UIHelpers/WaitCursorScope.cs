using System;
using System.Windows.Forms;
namespace ServiceBusExplorer.UIHelpers
{
    internal class WaitCursorScope : IDisposable
    {
        private readonly Form _form;
        public WaitCursorScope(Form form = null)
        {
            _form = form;
            WaitCursor(true);
        }

        public void Dispose()
        {
            WaitCursor(false);
        }

        private void WaitCursor(bool waitCursor)
        {
            _ = _form == null ? Application.UseWaitCursor = waitCursor : _form.UseWaitCursor = waitCursor;
        }
    }
}
