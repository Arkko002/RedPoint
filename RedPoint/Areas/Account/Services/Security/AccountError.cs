using Microsoft.Extensions.Logging;

namespace RedPoint.Areas.Account.Services.Security
{
    public class AccountError
    {
        public LogLevel LogLevel { get; }
        public  string LogMessage { get; }

        public AccountErrorType ErrorType {get;}

        public AccountError(AccountErrorType errorType, LogLevel logLevel = LogLevel.None, string logMessage = null)
        {
            ErrorType = errorType;
            LogLevel = logLevel;
            LogMessage = logMessage;
        }
    }
}