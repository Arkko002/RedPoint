namespace RedPoint.Services.Security
{
    public interface IAccountRequestValidator
    {
        bool IsRegisterRequestValid(UserRegisterDto requestDto);
        bool IsLoginRequestValid();
    }
}