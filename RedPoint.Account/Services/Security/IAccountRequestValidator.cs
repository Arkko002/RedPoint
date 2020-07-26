using RedPoint.Account.Models;

namespace RedPoint.Account.Services.Security
{
    public interface IAccountRequestValidator
    {
        AccountError IsRegisterRequestValid(UserRegisterDto requestDto);
        AccountError IsLoginRequestValid(UserLoginDto requestDto);
    }
}