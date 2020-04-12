using System.Collections.Generic;
using System.Linq;
using System.Threading;
using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Chat.Models;
using RedPoint.Exceptions.Security;

namespace RedPoint.Areas.Chat.Services.Security
{
    public class ChatRequestValidator : IChatRequestValidator
    {
        public void IsServerRequestValid(Server server, ApplicationUser user, PermissionType permissionType)
        {
            IsUserInServer(server, user);
            
            var userPermissions = GetUserPermissionsOnEntity(user, server);
            
            CheckUserPermissions(userPermissions, permissionType);
        }

        public void IsChannelRequestValid(Channel channel, Server server, ApplicationUser user, PermissionType permissionType)
        {
            IsUserInServer(server, user);

            var userPermissions = GetUserPermissionsOnEntity(user, channel);
            CheckUserPermissions(userPermissions, permissionType);
        }

        private void IsUserInServer(Server server, ApplicationUser user)
        {
            if (!server.Users.Contains(user))
            {
                throw new InvalidServerRequestException("User is not part of the server");
            }
        }
        
        private void CheckUserPermissions(IEnumerable<PermissionType> userPermissions,
            PermissionType expectedPermissions)
        {
            if (!userPermissions.Any(x => x == expectedPermissions || x == PermissionType.IsAdmin))
            {
                throw new LackOfPermissionException($"User lacks {expectedPermissions} permission");
            }
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
    }
}