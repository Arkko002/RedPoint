using System.Threading.Tasks;
using RedPoint.Account.Models;

namespace RedPoint.Account.Services
{
    public interface IAccountService
    {
        Task<string> Login(UserLoginDto model);
        Task<string> Register(UserRegisterDto model);
        Task<bool> Delete(UserLoginDto model);
    }
}