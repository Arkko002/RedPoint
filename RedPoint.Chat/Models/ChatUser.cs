using System.Collections.Generic;
using System.Drawing;
using Microsoft.AspNetCore.Identity;
using RedPoint.Chat.Models;
using RedPoint.Chat.Models.User_Settings;
using RedPoint.Data;

namespace RedPoint.Chat.Models
{
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