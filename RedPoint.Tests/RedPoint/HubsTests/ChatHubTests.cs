using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RedPoint.Data;
using RedPoint.Hubs;
using RedPoint.Models;
using Moq;
using Microsoft.AspNetCore.SignalR;

namespace RedPoint.Tests.RedPoint.HubTests
{
    [TestFixture]
    public class ChatHubTests
    {
        private ApplicationDbContext _db;
        private UserManager<ApplicationUser> _userManager;
        private ChatHub _chatHub;

        [SetUp]
        protected void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ChatHubTestDb")
                .Options;
                
            _db = new ApplicationDbContext(options);

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            _userManager = new UserManager<ApplicationUser>(userStore.Object, null, null, null,
                                                            null, null, null, null, null);
           
            
            _chatHub = new ChatHub(_db, _userManager);

        }

        [Test]
        public void Send()
        {

        }

        [Test]
        public void ChannelChanged()
        {

        }

        [Test]
        public void ServerChanged()
        {

        }
    }
}