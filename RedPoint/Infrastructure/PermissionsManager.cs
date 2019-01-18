using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RedPoint.Models.Chat_Models;

namespace RedPoint.Models.Users_Permissions_Models
{
    public class PermissionsManager
    {    
        /// <summary>
        /// Checks user's permissions in the given channel
        /// </summary>
        /// <param name="user"></param>
        /// <param name="serverId"></param>
        /// <param name="permissions"></param>
        /// <returns></returns>
        public bool CheckUserServerPermissions(ApplicationUser user, Server server, PermissionTypes[] permissions)
        {
            if (CheckIfSuperAdmin(user))
            {
                return true;
            }
           
            List<Group> groups = user.Groups.Where(g => g.ServerId == server.Id).ToList();
            if (groups.Count == 0)
            {
                return false;
            }
            if (groups.Any(g => g.GroupPermissions.IsAdmin))
            {
                return true;
            }

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

        /// <summary>
        /// Checks user's permissions unrelated to specific servers or channels
        /// </summary>
        /// <param name="user"></param>
        /// <param name="permissions"></param>
        /// <returns></returns>
        public bool CheckUserPermissions(ApplicationUser user, string[] permissions)
        {
            if (CheckIfSuperAdmin(user))
            {
                return true;
            }

            bool hasPermission = false;

            foreach (var perm in permissions)
            {
                foreach (var group in user.Groups)
                {
                    if ((bool)group.GroupPermissions.GetType().GetProperty(perm).GetValue(group))
                    {
                        hasPermission = true;
                    }
                }
            }

            return hasPermission;
        }

        /// <summary>
        /// Checks user's permissions in the given channel
        /// </summary>
        /// <param name="user"></param>
        /// <param name="channel"></param>
        /// <param name="permissions"></param>
        /// <returns></returns>
        public bool CheckUserChannelPermissions(ApplicationUser user, Channel channel, PermissionTypes[] permissions)
        {
            if(CheckIfSuperAdmin(user))
            {
                return true;
            }

            var userStub = user.UserStub;
            var groups = channel.Groups.Where(g => g.Users.Contains(userStub));
            if(groups.Any(g => g.GroupPermissions.IsAdmin))
            {
                return true;
            }

            bool hasPermission = false;
            foreach (var perm in permissions)
            {
                foreach (var group in groups)
                {
                    if ((bool)group.GroupPermissions.GetType().GetProperty(perm.ToString()).GetValue(group))
                    {
                        hasPermission = true;
                    }
                }
            }

            return hasPermission;
        }

        private bool CheckIfSuperAdmin(ApplicationUser user)
        {
            return user.Groups.Any(g => g.GroupPermissions.IsSuperAdmin);
        }
    }
}