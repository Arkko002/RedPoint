using NLog;

namespace RedPoint.Account.Services.Security
{
    //TODO Move this and ChatError into models.security namespace
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

        public LogLevel LogLevel { get; }
        public string LogMessage { get; }

        public AccountErrorType ErrorType { get; }
    }
}