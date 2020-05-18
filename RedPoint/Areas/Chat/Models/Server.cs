using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Chat.Models.Dto;
using RedPoint.Data;

namespace RedPoint.Areas.Chat.Models
{
    /// <summary>
    ///     Server class that contains channels list and users list
    /// </summary>
    public class Server : IEntity, IChatGroups
    {
        public Server()
        {
            InitializeVariables();
        }

        public Server(ServerIconDto serverIconDto)
        {
            Name = serverIconDto.Name;
            Description = serverIconDto.Description;
            Image = serverIconDto.Image;
            IsVisible = serverIconDto.IsVisible;

            InitializeVariables();
        }

        public int Id { get; set; }
        public HubGroupIdentifier HubGroupId { get; set; }


        [Required] public string Name { get; set; }

        [Required] public string Description { get; set; }

        public Bitmap Image { get; set; }

        public List<ApplicationUser> Users { get; set; }
        public List<Channel> Channels { get; set; }

        //Determines if server is visible in the server browser
        public bool IsVisible { get; set; }
        public List<Group> Groups { get; set; }

        private void InitializeVariables()
        {
            HubGroupId = new HubGroupIdentifier();
            Users = new List<ApplicationUser>();
            Groups = new List<Group>();
            Channels = new List<Channel>();
        }
    }
}