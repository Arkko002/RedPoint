using RedPoint.Account.Models.Errors;

namespace RedPoint.Account.Services
{
    public interface IAccountErrorHandler
    {
        void HandleError(AccountError error);
    }
}