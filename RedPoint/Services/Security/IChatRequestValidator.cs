using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Identity.Models;

namespace RedPoint.Services.Security
{
    public interface IChatRequestValidator
    {
        bool IsServerRequestValid(Server server, ApplicationUser user);
        bool IsChannelRequestValid(Channel channel, Server server, ApplicationUser user);
    }
}