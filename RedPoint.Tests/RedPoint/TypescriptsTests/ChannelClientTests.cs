using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using RedPoint.Areas.Chat.Hubs;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Services;
using RedPoint.Areas.Identity.Models;
using RedPoint.Data;
using RedPoint.Models;

namespace RedPoint.Tests.RedPoint.TypescriptsTests
{
    [TestFixture]
    class ChannelClientTests
    {
        private ApplicationDbContext _db;
        private UserManager<ApplicationUser> _userManager;
        private ChannelHub _channelHub;
        private HubUserInputValidator _inputValidator;

        [SetUp]
        protected void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ChatHubTestDb")
                .Options;

            _db = new ApplicationDbContext(options);

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            _userManager = new UserManager<ApplicationUser>(userStore.Object, null, null, null,
                null, null, null, null, null);

            _inputValidator = new HubUserInputValidator(_db);

            _channelHub = new ChannelHub(_userManager, _db, _inputValidator);


            Server server = new Server()
            {
                Name = "TestServer",
                Channels = new List<Channel>()
            };

            server.Channels.Add(new Channel()
            {
                Name = "Channel_1",
                Description = string.Empty,
                ChannelStub = new ChannelStub()
                {
                    Name = "Channel_1",
                    Description = string.Empty,
                }
            });
        }
    }
}
