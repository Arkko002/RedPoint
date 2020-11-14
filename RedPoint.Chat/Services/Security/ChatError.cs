using NLog;
using RedPoint.Chat.Models;

namespace RedPoint.Chat.Services.Security
{
    /// <summary>
    /// Error object used to represent internal, non-critical errors related to chat functionality.
    /// </summary>
    public class ChatError
    {
        
        public ChatUser User { get; }

        public LogLevel LogLevel { get; }
        public string LogMessage { get; }

        public ChatErrorType ErrorType { get; }
        
        /// <summary>
        /// This constructor overload should be only used for NoError objects
        /// </summary>
        /// <param name="errorType">Should be <c>ChatErrorType.NoError</c></param>
        public ChatError(ChatErrorType errorType)
        {
            ErrorType = errorType;
        }

        public ChatError(ChatErrorType errorType,
            LogLevel logLevel,
            string logMessage)
        {
            ErrorType = errorType;
            LogLevel = logLevel;
            LogMessage = logMessage;
        }

        public ChatError(ChatErrorType errorType,
            ChatUser user,
            LogLevel logLevel,
            string logMessage
        )
        {
            User = user;
            LogLevel = logLevel;
            LogMessage = logMessage;
            ErrorType = errorType;
        }

    }
}