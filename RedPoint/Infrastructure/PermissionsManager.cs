using System.Collections.Generic;
using System.Linq;
using RedPoint.Models;
using RedPoint.Models.Chat_Models;
using RedPoint.Models.Users_Permissions_Models;

namespace RedPoint.Infrastructure
{
    /// <summary>
    /// Provides methods for checking users' chat-related permissions.
    /// </summary>
    public class PermissionsManager
    {    
        /// <summary>
        /// Checks user's permissions in the given server.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="server"></param>
        /// <param name="permissions"></param>
        /// <returns></returns>
        public bool CheckUserServerPermissions(ApplicationUser user, Server server, PermissionTypes[] permissions)
        {          
            List<Group> groups = user.Groups.Where(g => g.Server.Id == server.Id).ToList();
            if (groups.Count == 0)
            {
                return false;
            }
            if (groups.Any(g => g.GroupPermissions.IsAdmin))
            {
                return true;
            }

            return CheckPermission(groups, permissions);
        }

        /// <summary>
        /// Checks user's permissions in the given channel.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="channel"></param>
        /// <param name="permissions"></param>
        /// <returns></returns>
        public bool CheckUserChannelPermissions(ApplicationUser user, Channel channel, PermissionTypes[] permissions)
        {
            var userStub = user.UserStub;
            var groups = channel.Groups.Where(g => g.Users.Contains(userStub)).ToList();
            if(groups.Any(g => g.GroupPermissions.IsAdmin))
            {
                return true;
            }
          
            return CheckPermission(groups, permissions);
        }

        /// <summary>
        /// Returns true if any of the provided permissions is set to true in provided groups 
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="permissions"></param>
        /// <returns></returns>
        private bool CheckPermission(List<Group> groups, PermissionTypes[] permissions)
        {
            bool hasPermission = false;
            foreach (var perm in permissions)
            {
                foreach (var group in groups)
                {
                    var groupPerms = group.GroupPermissions;
                    if ((bool)groupPerms.GetType().GetProperty(perm.ToString()).GetValue(groupPerms, null))
                    {
                        hasPermission = true;
                    }
                }
            }

            return hasPermission;
        }
    }
}