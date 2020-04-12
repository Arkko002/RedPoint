using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Chat.Models;

namespace RedPoint.Areas.Chat.Services.Security
{
    public interface IChatRequestValidator
    {
        ChatErrorType IsServerRequestValid(Server server, ApplicationUser user, PermissionType permissionType);
        ChatErrorType IsChannelRequestValid(Channel channel, Server server, ApplicationUser user, PermissionType permissionType);
    }
}