using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Chat.Models;

namespace RedPoint.Areas.Chat.Services.Security
{
    public interface IChatRequestValidator
    {
        ChatError IsServerRequestValid(Server server, ApplicationUser user, PermissionType permissionType);
        ChatError IsChannelRequestValid(Channel channel, Server server, ApplicationUser user, PermissionType permissionType);
    }
}