namespace RedPoint.Data
{
    public interface IUnitOfWork : IDisposing
    {
        void Submit();
        bool HasEnded { get; }
    }
}