namespace RedPoint.Account.Services.Security
{
    public enum AccountErrorType
    {
        NoError,
        PasswordTooWeak,
        UserLockedOut,
        LoginFailure,
        RegisterFailure
    }
}