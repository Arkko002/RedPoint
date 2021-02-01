using System.Collections.Generic;
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
        public string Name { get; set; }
        /// <summary>
        /// Description of channel. Can be empty.
        /// </summary>
        public string Description { get; set; }
        public List<Message> Messages { get; set; }
        public List<Group> Groups { get; set; }
        
        public Channel(Server server)
        {
            Server = server;
            InitializeLists();
        }

        public Channel(ChannelIconDto channelIconDto)
        {
            InitializeLists();

            Name = channelIconDto.Name;
            Description = channelIconDto.Description;
        }

        private void InitializeLists()
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