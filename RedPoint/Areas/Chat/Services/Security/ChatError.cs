using Microsoft.Extensions.Logging;
using RedPoint.Areas.Account.Models;

namespace RedPoint.Areas.Chat.Services.Security
{
    public class ChatError
    {
        /// <summary>
        ///     Constructor only for NoError type objects
        /// </summary>
        /// <param name="errorType"></param>
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
            ApplicationUser user,
            LogLevel logLevel,
            string logMessage
        )
        {
            User = user;
            LogLevel = logLevel;
            LogMessage = logMessage;
            ErrorType = errorType;
        }

        public ApplicationUser User { get; }

        public LogLevel LogLevel { get; }
        public string LogMessage { get; }

        public ChatErrorType ErrorType { get; }
    }
}