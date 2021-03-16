using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Security.Cryptography;
using RedPoint.Chat.Models.Chat.Dto;
using RedPoint.Data.Repository;

namespace RedPoint.Chat.Models.Chat
{
    //TODO!!! References to entities containing child entity, update DTOs, update factories
    /// <summary>
    /// Contains list of messages that were wrote in this channel.
    /// Can have separate permissions assigned per group basis.
    /// </summary>
    public class Channel : IEntity, IGroupEntity, IHubGroupIdentifier
    {
        public int Id { get; set; }
        public string GroupId { get; private set; }
        
        public Server Server { get; }

        /// <summary>
        /// Name of the channel
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Description of channel. Can be empty.
        /// </summary>
        public string Description { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        public IEnumerable<Group> Groups { get; set; }

        public Channel() {}
        
        public Channel(Server server, string name)
        {
            Server = server;
            Name = name;
            InitializeVariables();
        }

        public Channel(ChannelInfoDto channelInfoDto)
        {
            Name = channelInfoDto.Name;
            Description = channelInfoDto.Description;
            InitializeVariables();
        }

        private void InitializeVariables()
        {
            GroupId = ComputeHash();
            Messages = new List<Message>();
            Groups = new List<Group>();
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