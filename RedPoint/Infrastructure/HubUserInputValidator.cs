using System.Drawing;
using RedPoint.Data;
using RedPoint.Models;
using RedPoint.Models.Chat_Models;
using RedPoint.Models.Users_Permissions_Models;

namespace RedPoint.Infrastructure
{
    /// <summary>
    /// Provides methods for checking user input for potential errors.
    /// </summary>
    public class HubUserInputValidator
    {

        private ApplicationDbContext _db;
        private PermissionsManager _permissionsManager;

        public HubUserInputValidator(ApplicationDbContext db)
        {
            _db = db;
            _permissionsManager = new PermissionsManager();
        }

        /// <summary>
        /// Checks the message for potential unsafe input
        /// </summary>
        /// <param name="user"></param>
        /// <param name="msg"></param>
        /// <param name="channelId"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public UserInputError CheckCreatedMessage(ApplicationUser user, string msg, string channelId, out Channel channel)
        {
            if (CheckIfChannelExists(channelId, out channel) == UserInputError.NoChannel)
            {
                return UserInputError.NoChannel;
            }

            if (!_permissionsManager.CheckUserChannelPermissions(user, channel, new[] { PermissionTypes.CanWrite }))
            {
                return UserInputError.NoPermission_CantWrite;
            }
            
            //TODO Validate msg

            return UserInputError.InputValid;
        }

        public UserInputError CheckChannelChange(ApplicationUser user, string channelId, out bool canWrite, out bool canView, out Channel channel)
        {
            if (CheckIfChannelExists(channelId, out  channel) == UserInputError.NoChannel)
            {
                canView = false;
                canWrite = false;
                return UserInputError.NoChannel;
            }

            canView = _permissionsManager.CheckUserChannelPermissions(user, channel, new[] { PermissionTypes.CanView });

            canWrite = _permissionsManager.CheckUserChannelPermissions(user, channel, new[] { PermissionTypes.CanWrite });

            return UserInputError.InputValid;
        }

        public UserInputError CheckCreatedServer(ApplicationUser user, string name, string description, Bitmap image)
        {         
            //TODO
            return UserInputError.InputValid;
        }

        public UserInputError CheckServerRemove(ApplicationUser user, int serverId, out Server server)
        {
            if (CheckIfServerExists(serverId, out server) == UserInputError.NoServer)
            {
                return UserInputError.NoServer;
            }

            if (!server.Users.Contains(user.UserStub))
            {
                return UserInputError.UserNotInServer;
            }

            if (!_permissionsManager.CheckUserServerPermissions(user, server, new []{PermissionTypes.CanManageServer}))
            {
                return UserInputError.NoPermission_CantManageServer;
            }

            return UserInputError.InputValid;
        }

        public UserInputError CheckServerJoin(ApplicationUser user, int serverId, out Server server)
        {
            if (CheckIfServerExists(serverId, out server) == UserInputError.NoServer)
            {
                return UserInputError.NoServer;
            }

            if (server.Users.Contains(user.UserStub))
            {
                return UserInputError.UserAlreadyInServer;
            }

            return UserInputError.InputValid;
        }

        public UserInputError CheckServerLeave(int serverId, ApplicationUser user, out Server server)
        {
            if (CheckIfServerExists(serverId, out server) == UserInputError.NoServer)
            {
                return UserInputError.NoServer;
            }

            if (!server.Users.Contains(user.UserStub))
            {
                return UserInputError.UserNotInServer;
            }

            return UserInputError.InputValid;
        }

        private UserInputError CheckChannelOperation(ApplicationUser user, int serverId, out Server server)
        {
            if (CheckIfServerExists(serverId, out server) == UserInputError.NoServer)
            {
                return UserInputError.NoServer;
            }

            if (!server.Users.Contains(user.UserStub))
            {
                return UserInputError.UserNotInServer;
            }

            if (!_permissionsManager.CheckUserServerPermissions(user, server, new[] { PermissionTypes.CanManageChannels }))
            {
                return UserInputError.NoPermission_CantManageChannels;
            }

            return UserInputError.InputValid;
        }

        public UserInputError CheckCreatedChannel(ApplicationUser user, int serverId,
            out Server server)
        {
            var channelError = CheckChannelOperation(user, serverId, out server);
            if (channelError != UserInputError.InputValid)
            {
                return channelError;
            }

            //TODO Validate channel data
            return UserInputError.InputValid;
        }

        public UserInputError CheckChannelRemove(ApplicationUser user, int serverId, string channelId,
            out Channel channel, out Server server)
        {
            var channelOpError = CheckChannelOperation(user, serverId, out server);
            if (channelOpError != UserInputError.InputValid)
            {
                channel = null;
                return channelOpError;
            }

            var channelExists = CheckIfChannelExists(channelId, out channel);
            if (channelExists != UserInputError.InputValid)
            {
                return channelExists;
            }

            return UserInputError.InputValid;
        }


        private UserInputError CheckIfChannelExists(string channelId, out Channel channel)
        {
            int id = int.Parse(channelId.Split("_")[1]);
            channel = _db.Channels.Find(id);
            if (channel is null)
            {
                return UserInputError.NoChannel;
            }

            return UserInputError.InputValid;
        }

        private UserInputError CheckIfServerExists(int serverId, out Server server)
        {
            //TODO
            server = _db.Servers.Find(serverId);
            if (server is null)
            {
                return UserInputError.NoServer;
            }

            return UserInputError.InputValid;
        }
    }
}
