using System;

namespace RedPoint.Data.UnitOfWork
{
    public interface IDisposing : IDisposable
    {
        event EventHandler Disposing;
    }
}