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
        public ChatError IsServerRequestValid(Server server, ChatUser user, PermissionType permissionType)
        {
            if (!server.Users.Contains(user))
            {
                return new ChatError(ChatErrorType.UserNotInServer,
                    user,
                    LogLevel.Warn,
                    $"ID: {user.Id} tried to access server {server.Id} without joining first");
            }

            var userPermissions = GetUserPermissionsOnEntity(user, server);
            if (!IsUserPermitted(userPermissions, permissionType))
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
            PermissionType permissionType)
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
            if (!IsUserPermitted(userPermissions, permissionType))
            {
                return new ChatError(ChatErrorType.NoPermission,
                    user,
                    LogLevel.Warn,
                    $"ID: {user.Id} tried to access channel {channel.Id} without permission");
            }

            return new ChatError(ChatErrorType.NoError);
        }

        /// <summary>
        /// Gets a list of PermissionTypes attached to groups user is part of in a given server.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        private static IEnumerable<PermissionType> GetUserPermissionsOnEntity(ChatUser user, IChatGroups entity)
        {
            var userGroupsOnServer = entity.Groups.Where(x => x.Users.Contains(user));

            IEnumerable<PermissionType> userPermissions = new List<PermissionType>();

            return userGroupsOnServer.Aggregate(userPermissions,
                (current, @group) => current.Concat(@group.GroupPermissions));
        }

        /// <summary>
        /// Checks for occurence of an expected permission in the provided list of permissions.
        /// </summary>
        /// <param name="userPermissions"></param>
        /// <param name="expectedPermission"></param>
        /// <returns></returns>
        private static bool IsUserPermitted(IEnumerable<PermissionType> userPermissions,
            PermissionType expectedPermission)
        {
            return userPermissions.Any(x => x == expectedPermission || x == PermissionType.IsAdmin);
        }
    }
}