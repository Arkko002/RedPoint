using System.Collections.Generic;
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
            Messages = new List<Message>();
            Groups = new List<Group>();
        }
    }
}