using System.Collections.Generic;
using RedPoint.Areas.Chat.Models.Dto;
using RedPoint.Data;

namespace RedPoint.Areas.Chat.Models
{
    /// <summary>
    /// Channel class that represents text chat room
    /// </summary>
    public class Channel : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Message> Messages { get; set; }
        public List<Group> Groups { get; set; }

        public Channel()
        {
            InitializeLists();
        }

        public Channel(ChannelDto channelDto)
        {
            InitializeLists();

            Name = channelDto.Name;
            Description = channelDto.Description;
        }

        private void InitializeLists()
        {
            Messages = new List<Message>();
            Groups = new List<Group>();
        }
    }
}