using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RedPoint.Data;
using RedPoint.Models;
using RedPoint.Models.Chat_Models;
using RedPoint.Models.Users_Permissions_Models;

namespace RedPoint.Infrastructure
{
    public class HubUserInputValidator
    {
        private ApplicationDbContext _db;

        public HubUserInputValidator(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Returns true if provided data is valid and safe
        /// </summary>
        /// <param name="user"></param>
        /// <param name="msg"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public UserInputError CheckCreatedMessage(ApplicationUser user, string msg, string channelId, out Channel channel)
        {
            int id = int.Parse(channelId.Split("_")[1]);
            channel = _db.Channels.Find(id);      
            if (channel is null)
            {
                return UserInputError.NoChannel;
            }


            //TODO Validate msg

            return UserInputError.InputValid;
        }

        public UserInputError CheckCreatedServer()
        {
            return UserInputError.InputValid;
        }

        public UserInputError CheckIfChannelExists(string channelId, out Channel channel)
        {
            int id = int.Parse(channelId.Split("_")[1]);
            channel = _db.Channels.Find(id);
            if (channel is null)
            {
                return UserInputError.NoChannel;
            }

            return UserInputError.InputValid;
        }

        public UserInputError CheckIfServerExists(int serverId, out Server server)
        {
            server = _db.Servers.Find(serverId);
            if (server is null)
            {
                return UserInputError.NoServer;
            }

            return UserInputError.InputValid;
        }

        public UserInputError CheckServerLeave(int serverId, ApplicationUser user, Server server)
        {
            server = _db.Servers.Find(serverId);
            if (server is null)
            {
                return UserInputError.NoServer;
            }

            if (!server.Users.Contains(user.UserStub))
            {
                return UserInputError.UserNotInServer;
            }

            return UserInputError.InputValid;
        }
    }
}
