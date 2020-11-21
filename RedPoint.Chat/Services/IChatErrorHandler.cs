using RedPoint.Chat.Models.Errors;

namespace RedPoint.Chat.Services
{
    /// <summary>
    /// Performs error handling for non-critical errors that occured in chat functionality.
    /// </summary>
    public interface IChatErrorHandler
    {
        /// <summary>
        /// Throws exception based on ChatErrorType in the error object.
        /// Should return with no exception on NoError.
        /// </summary>
        /// <param name="error"></param>
        void HandleChatError(ChatError error);
    }
}