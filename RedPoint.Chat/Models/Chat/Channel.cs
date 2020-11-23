﻿using System.Collections.Generic;
using RedPoint.Chat.Models.Chat.Dto;
using RedPoint.Data.Repository;

namespace RedPoint.Chat.Models.Chat
{
    /// <summary>
    /// Contains list of messages that were wrote in this channel.
    /// Can have separate permissions assigned per group basis.
    /// </summary>
    public class Channel : IEntity, IChatGroups
    {
        public int Id { get; set; }
        public HubGroupIdentifier HubGroupId { get; set; }

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
        
        public Channel()
        {
            HubGroupId = new HubGroupIdentifier();
            InitializeLists();
        }

        public Channel(ChannelIconDto channelIconDto)
        {
            InitializeLists();

            Name = channelIconDto.Name;
            Description = channelIconDto.Description;
            HubGroupId = channelIconDto.HubGroupId;
        }

        private void InitializeLists()
        {
            Messages = new List<Message>();
            Groups = new List<Group>();
        }
    }
}