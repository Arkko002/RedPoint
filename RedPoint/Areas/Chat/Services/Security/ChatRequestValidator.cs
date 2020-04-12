using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Identity.Models;
using RedPoint.Exceptions.Security;

namespace RedPoint.Services.Security
{
    public class ChatRequestValidator : IChatRequestValidator
    {
        public void IsServerRequestValid(Server server, ApplicationUser user)
        {
            if (!server.Users.Contains(user))
            {
                throw new InvalidServerRequestException("User is not part of the server");
            }
        }

        public void IsChannelRequestValid(Channel channel, Server server, ApplicationUser user)
        {
            if (!server.Users.Contains(user))
            {
                throw new InvalidChannelRequestException("User is not part of the server");
            }
            
            //TODO Server permissions, channel permissions
        }
    }
}