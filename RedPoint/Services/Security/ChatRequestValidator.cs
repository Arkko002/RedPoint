using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Identity.Models;

namespace RedPoint.Services.Security
{
    public class ChatRequestValidator : IChatRequestValidator
    {
        public bool IsServerRequestValid(Server server, ApplicationUser user)
        {
            if (server.Users.Contains(user))
            {

                return true;
            }

            return false;
        }

        public bool IsChannelRequestValid(Channel channel, Server server, ApplicationUser user)
        {
            return true;
        }
    }
}