using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Security.Cryptography;
using RedPoint.Chat.Models.Chat.Dto;
using RedPoint.Data.Repository;

namespace RedPoint.Chat.Models.Chat
{
    /// <summary>
    /// Represents chat server, which is the internal highest level of content organisation.
    /// Contains lists of users, channels, and groups that are part of it.
    /// Can have an image assigned to it.
    /// </summary>
    public class Server : IEntity, IGroupEntity, IHubGroupIdentifier
    {
        public int Id { get; set; }
        public string GroupId { get; private set; }


        [Required] public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; private set; }

        public IEnumerable<ChatUser> Users { get; set; }
        public IEnumerable<Channel> Channels { get; set; }
        
        /// <summary>
        /// Determines if server is visible in the server browser
        /// </summary>
        public bool IsVisible { get; set; }
        public IEnumerable<Group> Groups { get; set; }
        
        public Server(string name)
        {
            Name = name;
            
            InitializeVariables();
        }

        public Server(ServerInfoDto serverInfoDto)
        {
            Name = serverInfoDto.Name;
            Description = serverInfoDto.Description;
            Image = serverInfoDto.Image;
            IsVisible = serverInfoDto.IsVisible;

            InitializeVariables();
        }
        
        private void InitializeVariables()
        {
            GroupId = ComputeHash();
            Users = new List<ChatUser>();
            Groups = new List<Group>();
            Channels = new List<Channel>();
        }
        
        public string ComputeHash()
        {
            using (var ms = new MemoryStream())
            {
                using (var writer = new BinaryWriter(ms))
                {
                    writer.Write(Id);
                    writer.Write(Name);
                }
                
                HashAlgorithm algo = SHA256.Create();
                var hash = algo.ComputeHash(ms.ToArray());

                return System.Text.Encoding.UTF8.GetString(hash);
            }
        }
    }
}