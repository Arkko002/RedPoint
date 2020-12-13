using System.Collections.Generic;
using System.Linq;
using NLog;
using RedPoint.Chat.Exceptions.Security;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Errors;

namespace RedPoint.Chat.Services.Security
{
    /// <inheritdoc />>
    public class ChatRequestValidator : IChatRequestValidator
    {
        /// <inheritdoc/>
        public void IsServerRequestValid(Server server, ChatUser user, PermissionTypes permission)
        {
            CheckMembership(user, server);

            var userPermissions = GetUserPermissionsOnEntity(user, server);
            if (!IsUserPermitted(userPermissions, permission))
            {
                throw new LackOfPermissionException("User lacks permission to perform requested action.", user.UserName,
                    user.Id, server.Name, server.Id, permission); 
            }
        }

        /// <inheritdoc/>
        public void IsChannelRequestValid(Channel channel, Server server, ChatUser user,
            PermissionTypes permission)
        {
            CheckMembership(user, server);

            //Combines global server permissions with channel-specific permissions
            var userPermissions = GetUserPermissionsOnEntity(user, server).Concat(GetUserPermissionsOnEntity(user, channel));
            if (!IsUserPermitted(userPermissions, permission))
            {
                throw new LackOfPermissionException("User lacks permission to perform requested action.", user.UserName,
                    user.Id, channel.Name, channel.Id, permission);
            }
        }

        /// <summary>
        /// Checks if user is on the server's list of users.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="server"></param>
        /// <exception cref="UserMembershipException">Thrown when user is not a part of the server.</exception>
        private static void CheckMembership(ChatUser user, Server server)
        {
            if (!server.Users.Contains(user))
            {
                throw new UserMembershipException("User is not a part of the server.", user.UserName, user.Id,
                    server.Name, server.Id);
            }
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