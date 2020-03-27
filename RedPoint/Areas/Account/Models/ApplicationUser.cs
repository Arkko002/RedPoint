using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Models.User_Settings;

namespace RedPoint.Areas.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string CurrentChannelId { get; set; }
        public int CurrentServerId { get; set; }

        public List<Message> Messages { get; set; }
        public List<Group> Groups { get; set; }
        public List<Server> Servers { get; set; }
        public UserSettings UserSettings { get; set; }

        public string ImagePath { get; set; }
    }
}