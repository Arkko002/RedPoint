using System.Collections.Generic;
using RedPoint.Areas.Identity.Models;

namespace RedPoint.Areas.Chat.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ServerStub Server { get; set; }

        //Used to store user's IDs
        public List<UserDTO> Users { get; set; }
        public GroupPermissions GroupPermissions { get; set; }
        public bool CanBeDeleted { get; }

        public Group()
        {
            if (Name == "Default")
            {
                CanBeDeleted = false;
            }

            Users = new List<UserDTO>();
        }
    }
}