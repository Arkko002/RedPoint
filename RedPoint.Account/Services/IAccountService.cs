using System.Threading.Tasks;
using RedPoint.Account.Models;

namespace RedPoint.Account.Services
{
    /// <summary>
    /// Provided methods necessary for account manipulation.
    /// </summary>
    public interface IAccountService
    {
        Task<string> Login(UserLoginDto model);
        Task<string> Register(UserRegisterDto model);
        Task<bool> Delete(UserLoginDto model);
    }
}