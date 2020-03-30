using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Identity.Models;

namespace RedPoint.Areas.Chat.Services.Security
{
    public interface IRequestValidator
    {
        bool IsServerRequestValid(Server server, ApplicationUser user);
        bool IsChannelRequestValid(Channel channel, Server server, ApplicationUser user);
    }
}