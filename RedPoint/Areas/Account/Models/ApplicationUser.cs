using System.Collections.Generic;
using System.Drawing;
using Microsoft.AspNetCore.Identity;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Models.User_Settings;
using RedPoint.Data;

namespace RedPoint.Areas.Account.Models
{
    public class ApplicationUser : IdentityUser, IEntity
    {
        public string CurrentChannelId { get; set; }
        public int CurrentServerId { get; set; }

        public List<Message> Messages { get; set; }
        public List<Group> Groups { get; set; }
        public List<Server> Servers { get; set; }
        public UserSettings UserSettings { get; set; }

        public Bitmap Image { get; set; }
    }
}