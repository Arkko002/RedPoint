using System.Threading.Tasks;
using RedPoint.Areas.Account.Models;

namespace RedPoint.Areas.Account.Services
{
    public interface IAccountService
    {
        Task<string> Login(UserLoginDto model);
        Task<string> Register(UserRegisterDto model);
    }
}