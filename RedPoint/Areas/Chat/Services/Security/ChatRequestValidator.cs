using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Logging;
using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Chat.Models;
using RedPoint.Exceptions.Security;

namespace RedPoint.Areas.Chat.Services.Security
{
    public class ChatRequestValidator : IChatRequestValidator
    {
        public ChatError IsServerRequestValid(Server server, ApplicationUser user, PermissionType permissionType)
        {
            if (!IsUserInServer(server, user))
            {
                return new ChatError(ChatErrorType.UserNotInServer,
                    user,
                    LogLevel.Warning,
                    $"ID: {user.Id} tried to access server {server.Id} without joining first");
            }

            var userPermissions = GetUserPermissionsOnEntity(user, server);
            if(!IsUserPermitted(userPermissions, permissionType))
            {
                return new ChatError(ChatErrorType.NoPermission,
                    user,
                    LogLevel.Warning,
                    $"ID: {user.Id} tried to access server {server.Id} without permission");
            }
            
            return new ChatError(ChatErrorType.NoError);
        }

        public ChatError IsChannelRequestValid(Channel channel, Server server, ApplicationUser user, PermissionType permissionType)
        {
            //TODO Check if channel is part of the server
            if (!IsUserInServer(server, user))
            {
                return new ChatError(ChatErrorType.UserNotInServer,
                    user,
                    LogLevel.Warning,
                    $"ID: {user.Id} tried to access server {server.Id} without joining first");
            }
            
            var userPermissions = GetUserPermissionsOnEntity(user, channel);
            if(!IsUserPermitted(userPermissions, permissionType))
            {
                return new ChatError(ChatErrorType.NoPermission,
                    user,
                    LogLevel.Warning,
                    $"ID: {user.Id} tried to access channel {channel.Id} without permission");
            }
            
            return new ChatError(ChatErrorType.NoError);
        }

        private bool IsUserInServer(Server server, ApplicationUser user)
        {
            return server.Users.Contains(user);
        }
        
        private IEnumerable<PermissionType> GetUserPermissionsOnEntity(ApplicationUser user, IChatGroups entity)
        {
            var userGroupsOnServer =  entity.Groups.Where(x => x.Users.Contains(user));
            
            IEnumerable<PermissionType> userPermissions = new List<PermissionType>();
            foreach (var group in userGroupsOnServer)
            {
                userPermissions = userPermissions.Concat(group.GroupPermissions);
            }

            return userPermissions;
        }
        
        private bool IsUserPermitted(IEnumerable<PermissionType> userPermissions,
            PermissionType expectedPermissions)
        {
            return userPermissions.Any(x => x == expectedPermissions || x == PermissionType.IsAdmin);
        }
    }
}