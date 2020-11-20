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
    public class ChatUser : IdentityUser, IEntity
    {
        public string CurrentChannelId { get; set; }
        public string CurrentServerId { get; set; }

        public List<Message> Messages { get; set; }
        public List<Group> Groups { get; set; }
        public List<Server> Servers { get; set; }
        public UserSettings UserSettings { get; set; }

        public Bitmap Image { get; set; }
    }
}