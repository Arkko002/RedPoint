using System.Collections.Generic;
using System.Linq;
using NLog;
using RedPoint.Chat.Models;

namespace RedPoint.Chat.Services.Security
{
    /// <summary>
    /// Detects potential errors and security issues in user request's within chat functionality.
    /// </summary>
    public class ChatRequestValidator : IChatRequestValidator
    {
        /// <summary>
        /// Performs internal checks of user's request against a server (e.g. check's for appropriate permissions).
        /// </summary>
        /// <param name="server"></param>
        /// <param name="user"></param>
        /// <param name="permissionType"></param>
        /// <returns>ChatError object with error details. ChatErrorType is set to NoError on valid requests.</returns>
        public ChatError IsServerRequestValid(Server server, ChatUser user, PermissionType permissionType)
        {
            if (!IsUserInServer(server, user))
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

        /// <summary>
        /// Performs internal checks of user's request against a channel (e.g. check's for appropriate permissions).
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="server"></param>
        /// <param name="user"></param>
        /// <param name="permissionType"></param>
        /// <returns>ChatError object with error details. ChatErrorType is set to NoError on valid requests.</returns>
        public ChatError IsChannelRequestValid(Channel channel, Server server, ChatUser user,
            PermissionType permissionType)
        {
            //TODO Check if channel is part of the server
            if (!IsUserInServer(server, user))
            {
                return new ChatError(ChatErrorType.UserNotInServer,
                    user,
                    LogLevel.Warn,
                    $"ID: {user.Id} tried to access server {server.Id} without joining first");
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

        private static bool IsUserInServer(Server server, ChatUser user)
        {
            return server.Users.Contains(user);
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

            return userGroupsOnServer.Aggregate(userPermissions, (current, @group) => current.Concat(@group.GroupPermissions));
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