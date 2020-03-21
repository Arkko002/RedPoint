using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Models.User_Settings;

namespace RedPoint.Areas.Identity.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string CurrentChannelId { get; set; }
        public int CurrentServerId { get; set; }

        public List<Message> Messages { get; set; }
        public List<Group> Groups { get; set; }
        public List<Server> Servers { get; set; }
        public UserChatDto UserDto { get; set; }
        public UserSettings UserSettings { get; set; }

        //public ApplicationUser()
        //{
        //    CreateUserStub();
        //}

        //private void CreateUserStub()
        //{
        //    UserDTO = new UserDTO()
        //    {
        //        AppUserId = Id,
        //        AppUserName = UserName
        //    };
        //}
    }


}