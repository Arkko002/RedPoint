using RedPoint.Areas.Chat.Services.Security;

namespace RedPoint.Areas.Chat.Services
{
    public interface IChatErrorHandler
    {
        void HandleChatError(ChatError chatError);
    }
}