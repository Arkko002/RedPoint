using System.Collections.Generic;
using RedPoint.Areas.Chat.Models.Dto;
using RedPoint.Areas.Identity.Models;
using RedPoint.Data;

namespace RedPoint.Areas.Chat.Models
{
    /// <summary>
    /// Server class that contains channels list and users list
    /// </summary>
    public class Server : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

        public List<ApplicationUser> Users { get; set; }
        public List<Group> Groups { get; set; }
        public List<Channel> Channels { get; set; }

        //Determines if server is visible in the server browser
        public bool IsVisible { get; set; }

        public Server()
        {
            Users = new List<ApplicationUser>();
            Groups = new List<Group>();
            Channels = new List<Channel>();
        }
    }

}