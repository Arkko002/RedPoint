using System.Collections.Generic;
using System.Linq;
using NLog;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Errors;

namespace RedPoint.Chat.Services.Security
{
    /// <inheritdoc />>
    public class ChatRequestValidator : IChatRequestValidator
    {
        /// <inheritdoc/>
        public ChatError IsServerRequestValid(Server server, ChatUser user, PermissionTypes permissionTypes)
        {
            if (!server.Users.Contains(user))
            {
                return new ChatError(ChatErrorType.UserNotInServer,
                    user,
                    LogLevel.Warn,
                    $"ID: {user.Id} tried to access server {server.Id} without joining first");
            }

            var userPermissions = GetUserPermissionsOnEntity(user, server);
            if (!IsUserPermitted(userPermissions, permissionTypes))
            {
                return new ChatError(ChatErrorType.NoPermission,
                    user,
                    LogLevel.Warn,
                    $"ID: {user.Id} tried to access server {server.Id} without permission");
            }

            return new ChatError(ChatErrorType.NoError);
        }

        /// <inheritdoc/>
        public ChatError IsChannelRequestValid(Channel channel, Server server, ChatUser user,
            PermissionTypes permissionTypes)
        {
            if (!server.Users.Contains(user))
            {
                return new ChatError(ChatErrorType.UserNotInServer,
                    user,
                    LogLevel.Warn,
                    $"ID: {user.Id} tried to access server {server.Id} without joining first");
            }

            if (!server.Channels.Contains(channel))
            {
                return new ChatError(ChatErrorType.ChannelNotFound,
                    user,
                    LogLevel.Warn,
                    $"ID: {user.Id} tried to access channel {channel.Id} that isn't part of server {server.Id}");
            }

            var userPermissions = GetUserPermissionsOnEntity(user, channel);
            if (!IsUserPermitted(userPermissions, permissionTypes))
            {
                return new ChatError(ChatErrorType.NoPermission,
                    user,
                    LogLevel.Warn,
                    $"ID: {user.Id} tried to access channel {channel.Id} without permission");
            }

            return new ChatError(ChatErrorType.NoError);
        }

        /// <summary>
        /// Gathers all of user's permissions on a given <c>IGroupEntity</c> and returns them as a collection.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        private static IEnumerable<PermissionTypes> GetUserPermissionsOnEntity(ChatUser user, IGroupEntity entity)
        {
            var userGroupsOnServer = entity.Groups.Where(x => x.Users.Contains(user));
            var userPermissions = new List<PermissionTypes>();

            foreach (var group in userGroupsOnServer)
            {
                userPermissions.Add(group.GroupPermissions);
            }

            return userPermissions;
        }

        /// <summary>
        /// Checks for occurence of an expected permission in the provided list of permissions.
        /// Returns true if user is an admin regardless of occurrence of expected permission. 
        /// </summary>
        /// <param name="userPermissions"></param>
        /// <param name="expectedPermission"></param>
        /// <returns></returns>
        private static bool IsUserPermitted(IEnumerable<PermissionTypes> userPermissions,
            PermissionTypes expectedPermission)
        {
            return userPermissions.Any(x => x.HasFlag(expectedPermission) || x.HasFlag(PermissionTypes.IsAdmin));
        }
    }
}