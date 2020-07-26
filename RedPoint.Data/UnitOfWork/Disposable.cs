using System;

namespace RedPoint.Data.UnitOfWork
{
    public abstract class Disposable : IDisposing
    {
        public event EventHandler Disposing;

        public void Dispose()
        {
            Disposing?.Invoke(this, new EventArgs());
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}