using System.Collections.Generic;
using RedPoint.Areas.Account.Models;
using RedPoint.Data;

namespace RedPoint.Chat.Models
{
    public class Group : IEntity
    {
        public Group()
        {
            HubGroupId = new HubGroupIdentifier();

            if (Name == "Default")
            {
                CanBeDeleted = false;
            }

            Users = new List<ChatUser>();
            GroupPermissions = new List<PermissionType>();
        }

        public int Id { get; set; }
        public HubGroupIdentifier HubGroupId { get; set; }

        public string Name { get; set; }
        public Server Server { get; set; }

        //Used to store user's IDs
        public List<ChatUser> Users { get; set; }
        public List<PermissionType> GroupPermissions { get; set; }
        public bool CanBeDeleted { get; }
    }
}