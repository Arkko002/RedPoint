using System.Security.Claims;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Identity.Models;
using RedPoint.Exceptions;

namespace RedPoint.Areas.Chat.Services.Security
{
    public class ChatRequestValidator : IRequestValidator
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