using System;
namespace RedPoint.Data
{
    public interface IDisposing : IDisposable
    {
        event EventHandler Disposing;
    }
}