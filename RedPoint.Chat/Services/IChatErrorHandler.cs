using RedPoint.Chat.Models.Errors;
using RedPoint.Chat.Services.Security;

namespace RedPoint.Chat.Services
{
    /// <summary>
    /// Performs error handling for non-critical errors that occured in chat functionality.
    /// </summary>
    public interface IChatErrorHandler
    {
        void HandleChatError(ChatError error);
    }
}