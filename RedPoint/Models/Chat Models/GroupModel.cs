using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RedPoint.Models.Users_Permissions_Models;

namespace RedPoint.Models.Chat_Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ServerStub Server { get; set; }

        //Used to store user's IDs
        public List<UserStub> Users { get; set; }
        public GroupPermissions GroupPermissions { get; set; }
        public bool CanBeDeleted { get; }

        public Group()
        {
            if (Name == "Default")
            {
                CanBeDeleted = false;
            }

            Users = new List<UserStub>();
        }
    }
}