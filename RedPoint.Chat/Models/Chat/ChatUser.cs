using System.Collections.Generic;
using System.Drawing;
using Microsoft.AspNetCore.Identity;
using RedPoint.Chat.Models.Chat.User_Settings;
using RedPoint.Data.Repository;

namespace RedPoint.Chat.Models.Chat
{
    
    /// <summary>
    /// Extends <c>IdentityUser</c> with data that is related to chat functionality (e.g. list of server's user joined).
    /// </summary>
    public class ChatUser : IEntity
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        
        public string CurrentChannelId { get; set; }
        public string CurrentServerId { get; set; }

        public IEnumerable<Message> Messages { get; set; }
        public IEnumerable<Group> Groups { get; set; }
        public IEnumerable<Server> Servers { get; set; }
        public UserSettings UserSettings { get; set; }

        public byte[] Image { get; set; }

        public ChatUser()
        {
            Messages = new List<Message>();
            Groups = new List<Group>();
            Servers = new List<Server>();
        }
    }
}