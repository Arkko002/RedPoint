using System.Threading.Tasks;
using RedPoint.Areas.Account.Models;

namespace RedPoint.Areas.Account.Services
{
    public interface IAccountService
    {
        Task<object> Login(UserLoginDto model);
        Task<object> Register(UserRegisterDto model);
    }
}