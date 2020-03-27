using System.Collections.Generic;
using RedPoint.Areas.Chat.Models.Dto;
using RedPoint.Data;

namespace RedPoint.Areas.Chat.Models
{
    public class Group : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ServerDto Server { get; set; }

        //Used to store user's IDs
        public List<UserChatDto> Users { get; set; }
        public GroupPermissions GroupPermissions { get; set; }
        public bool CanBeDeleted { get; }

        public Group()
        {
            if (Name == "Default")
            {
                CanBeDeleted = false;
            }

            Users = new List<UserChatDto>();
        }
    }
}