using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Identity.Models;

namespace RedPoint.Services.Security
{
    public interface IChatRequestValidator
    {
        void IsServerRequestValid(Server server, ApplicationUser user);
        void IsChannelRequestValid(Channel channel, Server server, ApplicationUser user);
    }
}