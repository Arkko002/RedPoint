using Microsoft.Extensions.Logging;
using RedPoint.Areas.Account.Models;

namespace RedPoint.Areas.Chat.Services.Security
{
    public class ChatError
    {
        public ApplicationUser User { get; }
        
        public LogLevel LogLevel { get; }
        public string LogMessage { get; }
        
        public ChatErrorType ErrorType { get; }

        public ChatError(ChatErrorType errorType,
            ApplicationUser user = null,
            LogLevel logLevel = LogLevel.None,
            string logMessage = null)
        {
            User = user;
            LogLevel = logLevel;
            LogMessage = logMessage;
            ErrorType = errorType;
        }
    }
}