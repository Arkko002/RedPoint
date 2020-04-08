using System.Threading.Tasks;

namespace RedPoint.Services
{
    public interface IAccountService
    {
        void ValidateLoginRequest(); // TODO
        Task<object> Login(UserLoginDto model);
        Task<object> Register(UserRegisterDto model);
    }
}