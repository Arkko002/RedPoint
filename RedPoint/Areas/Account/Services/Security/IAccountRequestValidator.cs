namespace RedPoint.Services.Security
{
    public interface IAccountRequestValidator
    {
        void IsRegisterRequestValid(UserRegisterDto requestDto);
        void IsLoginRequestValid(UserLoginDto requestDto);
    }
}