using System.Collections.Generic;
using RedPoint.Data.Repository;

namespace RedPoint.Chat.Models.Chat
{
    /// <summary>
    /// Allows organising permissions by grouping users together.
    /// On server creation a default server group is created that cannot be removed.
    /// Every user that joined a server is a part of it's default group.
    /// </summary>
    public class Group : IEntity
    {
        public int Id { get; set; }
        public HubGroupIdentifier HubGroupId { get; set; }

        public string Name { get; set; }
        public Server Server { get; set; }
        
        public List<ChatUser> Users { get; set; }
        
        /// <summary>
        /// Stores Flags defining group's permissions/
        /// </summary>
        public PermissionTypes GroupPermissions { get; set; }
        
        /// <summary>
        /// Set to true if group is the default, mandatory group of a server.
        /// Default groups cannot be deleted, and every user in a server is a part of default group.
        /// </summary>
        public bool IsDefaultGroup { get; }
        
        public Group()
        {
            HubGroupId = new HubGroupIdentifier();

            if (Name == "Default")
            {
                IsDefaultGroup = false;
            }

            Users = new List<ChatUser>();
        }
    }
}