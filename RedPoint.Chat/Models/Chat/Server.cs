﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using RedPoint.Chat.Models.Chat.Dto;
using RedPoint.Data.Repository;

namespace RedPoint.Chat.Models.Chat
{
    /// <summary>
    /// Represents chat server, which is the internal highest level of content organisation.
    /// Contains lists of users, channels, and groups that are part of it.
    /// Can have an image assigned to it.
    /// </summary>
    public class Server : IEntity, IGroupEntity
    {
        public int Id { get; set; }
        public HubGroupIdentifier HubGroupId { get; set; }


        [Required] public string Name { get; set; }

        [Required] public string Description { get; set; }

        public byte[] Image { get; private set; }

        public List<ChatUser> Users { get; set; }
        public List<Channel> Channels { get; set; }
        
        /// <summary>
        /// Determines if server is visible in the server browser
        /// </summary>
        public bool IsVisible { get; set; }
        public List<Group> Groups { get; set; }
        
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
        
        private void InitializeVariables()
        {
            HubGroupId = new HubGroupIdentifier();
            Users = new List<ChatUser>();
            Groups = new List<Group>();
            Channels = new List<Channel>();
        }
    }
}