using System.Collections.Generic;
using RedPoint.Models.Chat_Models;

namespace RedPoint.Models.Chat_Models
{
    /// <summary>
    /// Channel class that represents text chat room
    /// </summary>
    public class Channel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Message> Messages { get; set; }
        public List<Group> Groups { get; set; }

        public ChannelStub ChannelStub { get; set; }
    }

    /// <summary>
    /// DTO class for Channel
    /// </summary>
    public class ChannelStub
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}