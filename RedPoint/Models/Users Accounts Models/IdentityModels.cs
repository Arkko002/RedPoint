using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RedPoint.Models.Chat_Models;
using Microsoft.EntityFrameworkCore;

namespace RedPoint.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public List<Message> Messages { get; set; }
        public List<Group> Groups { get; set; }
        public List<Server> Servers { get; set; }
        public int CurrentChannelId { get; set; }
        public int CurrentServerIId { get; set; }
        public UserStub UserStub { get; set; }
    }


}