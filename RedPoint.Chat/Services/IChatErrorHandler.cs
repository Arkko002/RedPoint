using RedPoint.Chat.Services.Security;

namespace RedPoint.Chat.Services
{
    public interface IChatErrorHandler
    {
        void HandleChatError(ChatError chatError);
    }
}