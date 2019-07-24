using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RedPoint.Models.Chat_Models;
using Microsoft.EntityFrameworkCore;
using RedPoint.Models.Chat_Models.User_Settings;

namespace RedPoint.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string CurrentChannelId { get; set; }
        public int CurrentServerId { get; set; }

        public List<Message> Messages { get; set; }
        public List<Group> Groups { get; set; }
        public List<Server> Servers { get; set; }
        public UserStub UserStub { get; set; }
        public UserSettings UserSettings { get; set; }

        //public ApplicationUser()
        //{
        //    CreateUserStub();
        //}

        //private void CreateUserStub()
        //{
        //    UserStub = new UserStub()
        //    {
        //        AppUserId = Id,
        //        AppUserName = UserName
        //    };
        //}
    }


}