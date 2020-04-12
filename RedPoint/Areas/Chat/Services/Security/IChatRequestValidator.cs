using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Chat.Models;

namespace RedPoint.Areas.Chat.Services.Security
{
    public interface IChatRequestValidator
    {
        void IsServerRequestValid(Server server, ApplicationUser user, PermissionType permissionType);
        void IsChannelRequestValid(Channel channel, Server server, ApplicationUser user, PermissionType permissionType);
    }
}