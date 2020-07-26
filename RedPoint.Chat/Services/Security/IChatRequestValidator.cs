using RedPoint.Chat.Models;

namespace RedPoint.Chat.Services.Security
{
    public interface IChatRequestValidator
    {
        ChatError IsServerRequestValid(Server server, ChatUser user, PermissionType permissionType);

        ChatError IsChannelRequestValid(Channel channel, Server server, ChatUser user,
            PermissionType permissionType);
    }
}