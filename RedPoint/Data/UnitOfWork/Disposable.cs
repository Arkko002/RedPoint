using System;

namespace RedPoint.Data
{
    public abstract class Disposable : IDisposing
    {
        protected virtual void Dispose(bool disposing)
        {

        }

        public event EventHandler Disposing;

        public void Dispose()
        {
            Disposing?.Invoke(this, new EventArgs());
            Dispose(true);
            GC.SuppressFinalize(this);

        }
    }
}