using System.Threading.Tasks;

namespace RedPoint.Services
{
    public interface IAccountService
    {
        Task<object> Login(UserLoginDto model);
        Task<object> Register(UserRegisterDto model);
    }
}