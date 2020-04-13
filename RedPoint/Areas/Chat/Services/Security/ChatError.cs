using Microsoft.Extensions.Logging;

namespace RedPoint.Areas.Chat.Services.Security
{
    public class ChatError
    {
        public LogLevel LogLevel { get; }
        public string LogMessage { get; }
        
        public ChatErrorType ErrorType { get; }

        public ChatError(ChatErrorType errorType,
            LogLevel logLevel = LogLevel.None,
            string logMessage = null)
        {
            LogLevel = logLevel;
            LogMessage = logMessage;
            ErrorType = errorType;
        }
    }
}