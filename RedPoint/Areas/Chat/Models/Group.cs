using System.Collections.Generic;
using RedPoint.Areas.Account.Models;
using RedPoint.Data;

namespace RedPoint.Areas.Chat.Models
{
    public class Group : IEntity
    {
        public int Id { get; set; }
        public HubGroupIdentifier HubGroupId { get; set; }

        public string Name { get; set; }
        public Server Server { get; set; }

        //Used to store user's IDs
        public List<ApplicationUser> Users { get; set; }
        public List<PermissionType> GroupPermissions { get; set; }
        public bool CanBeDeleted { get; }
        
        public Group()
        {
            HubGroupId = new HubGroupIdentifier();

            if (Name == "Default")
            {
                CanBeDeleted = false;
            }

            Users = new List<ApplicationUser>();
            GroupPermissions = new List<PermissionType>();
        }
    }
}