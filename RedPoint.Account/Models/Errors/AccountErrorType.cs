namespace RedPoint.Account.Models.Errors
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