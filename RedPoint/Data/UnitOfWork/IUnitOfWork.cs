namespace RedPoint.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposing
    {
        bool HasEnded { get; }
        void Submit();
    }
}