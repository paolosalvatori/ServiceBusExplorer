using System;
using System.Windows.Forms;
namespace ServiceBusExplorer.UIHelpers
{
    internal class DisposableUseWaitCursor : IDisposable
    {
        private readonly Form _form;
        public DisposableUseWaitCursor(Form form = null)
        {
            _form = form;
            UseWaitCursor(true);
        }

        public void Dispose()
        {
            UseWaitCursor(false);
        }

        private void UseWaitCursor(bool useWaitCursor)
        {
            _ = _form == null ? Application.UseWaitCursor = useWaitCursor : _form.UseWaitCursor = useWaitCursor;
        }
    }
}
