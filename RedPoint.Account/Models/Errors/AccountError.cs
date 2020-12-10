using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;


namespace RedPoint.Account.Models.Errors
{
    //TODO Add user reference in the future
    /// <summary>
    /// Error object used to represent internal, non-critical errors in account-related functionality. 
    /// </summary>
    public class AccountError
    {
        /// <summary>
        /// This constructor overload should be only used for NoError objects.
        /// </summary>
        /// <param name="errorType"></param>
        public AccountError(AccountErrorType errorType)
        {
            ErrorType = errorType;
        }

        public AccountError(AccountErrorType errorType, LogLevel logLevel, string logMessage)
        {
            ErrorType = errorType;
            LogLevel = logLevel;
            LogMessage = logMessage;
        }

        public AccountError(AccountErrorType errorType, LogLevel logLevel, string logMessage, IdentityUser user)
        {
            ErrorType = errorType;
            LogLevel = logLevel;
            LogMessage = logMessage;
            User = user;
        }

        public LogLevel LogLevel { get; }
        public string LogMessage { get; }

        public AccountErrorType ErrorType { get; }
        
        public IdentityUser User { get; }
    }
}