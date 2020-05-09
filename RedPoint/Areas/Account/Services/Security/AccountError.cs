using Microsoft.Extensions.Logging;

namespace RedPoint.Areas.Account.Services.Security
{
    public class AccountError
    {
        public LogLevel LogLevel { get; }
        public  string LogMessage { get; }

        public AccountErrorType ErrorType {get;}

        /// <summary>
        /// Constructor only for NoError type objects
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
    }
}