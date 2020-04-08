using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using RedPoint.Areas.Chat.Models.Dto;
using RedPoint.Areas.Identity.Models;
using RedPoint.Data;

namespace RedPoint.Areas.Chat.Models
{
    public class Group : IEntity
    {
        public int Id { get; set; }
        public UniqueIdentifier UniqueId { get; set; }

        public string Name { get; set; }
        public Server Server { get; set; }

        //Used to store user's IDs
        public List<ApplicationUser> Users { get; set; }
        public GroupPermissions GroupPermissions { get; set; }
        public bool CanBeDeleted { get; }

        public Group()
        {
            UniqueId = new UniqueIdentifier();

            if (Name == "Default")
            {
                CanBeDeleted = false;
            }

            Users = new List<ApplicationUser>();
        }
    }
}