using RedPoint.Areas.Account.Models;

namespace RedPoint.Areas.Account.Services.Security
{
    public interface IAccountRequestValidator
    {
        AccountError IsRegisterRequestValid(UserRegisterDto requestDto);
        AccountError IsLoginRequestValid(UserLoginDto requestDto);
    }
}