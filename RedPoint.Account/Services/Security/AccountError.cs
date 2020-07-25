using Microsoft.Extensions.Logging;

namespace RedPoint.Areas.Account.Services.Security
{
    public class AccountError
    {
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