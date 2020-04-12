using RedPoint.Areas.Account.Models;

namespace RedPoint.Areas.Account.Services.Security
{
    public interface IAccountRequestValidator
    {
        void IsRegisterRequestValid(UserRegisterDto requestDto);
        void IsLoginRequestValid(UserLoginDto requestDto);
    }
}