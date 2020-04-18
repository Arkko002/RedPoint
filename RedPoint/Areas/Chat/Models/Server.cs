﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            HubGroupId = new HubGroupIdentifier();
            InitializeLists();
        }

        public Server(ServerDto serverDto)
        {
            Name = serverDto.Name;
            Description = serverDto.Description;
            ImagePath = serverDto.ImagePath;
            IsVisible = serverDto.IsVisible;

            InitializeLists();
        }

        public int Id { get; set; }
        public HubGroupIdentifier HubGroupId { get; set; }


        [Required] public string Name { get; set; }

        [Required] public string Description { get; set; }

        public string ImagePath { get; set; }

        public List<ApplicationUser> Users { get; set; }
        public IEnumerable<Group> Groups { get; set; }
        public List<Channel> Channels { get; set; }

        //Determines if server is visible in the server browser
        public bool IsVisible { get; set; }

        private void InitializeLists()
        {
            Users = new List<ApplicationUser>();
            Groups = new List<Group>();
            Channels = new List<Channel>();
        }
    }
}