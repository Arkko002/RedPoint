using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using RedPoint.Areas.Chat.Hubs;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Services;
using RedPoint.Areas.Identity.Models;
using RedPoint.Data;
using RedPoint.Models;
using RedPoint.Tests.RedPoint.Identity;

namespace RedPoint.Tests.RedPoint.HubsTests
{
    [TestFixture]
    public class ChatHubTests
    {
        private ApplicationDbContext _db;
        private Mock<FakeUserManager> _userManager;
        private ChatHub _chatHub;
        private Mock<HubUserInputValidator> _inputValidator;

        [OneTimeSetUp]
        protected void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ChatHubTestDb")
                .Options;            
            _db = new ApplicationDbContext(options);


            var userId = Guid.NewGuid().ToString();
            var users = new List<ApplicationUser>
            {
                new ApplicationUser()
                {
                    UserName = "Test",
                    Id = userId,
                    Email = "test@test.com",
                    
                    UserDto = new UserDTO()
                    {
                        AppUserId = userId,
                        AppUserName = "Test"
                    }
                    
                }
            }.AsQueryable();

            _userManager = new Mock<FakeUserManager>();
            _userManager.Setup(u => u.Users).Returns(users);

            _inputValidator = new Mock<HubUserInputValidator>();

            _chatHub = new ChatHub(_db, _userManager.Object, _inputValidator.Object);

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

            _db.Servers.Add(server);
            _db.SaveChanges();
        }

        [Test]
        public async Task Send()
        {
            //arrange
            Mock<IHubCallerClients> mockClients = new Mock<IHubCallerClients>();
            Mock<IClientProxy> mockClientProxy = new Mock<IClientProxy>();
            mockClients.Setup(c => c.All).Returns(mockClientProxy.Object);

            _chatHub.Clients = (IHubCallerClients<IChatHub>)mockClients.Object;

            //TODO Mock ChatHub context and connection ID (Identity)

            //act
            await _chatHub.Send("TestMessage", "Channel_1");

            //assert

        }

        [Test]
        public void Send_NoChannel()
        {

        }

        [Test]
        public void ChannelChanged()
        {

        }

        [Test]
        public async Task ServerChanged()
        {
            //TODO Make testable hub without strong types


            //arrange
            Mock<IHubCallerClients<IChatHub>> mockClients = new Mock<IHubCallerClients<IChatHub>>();                     
            Mock<HubCallerContext> mockContext = new Mock<HubCallerContext>();
            Mock<IClientProxy> mockProxy = new Mock<IClientProxy>();

            //Mock<IHubContext<ChatHub>> mockCon = new Mock<IHubContext<ChatHub>>();

            bool serverChanged = false;
            dynamic caller = new ExpandoObject();
            caller.getChannelList = new Action<List<Channel>>((list) => { serverChanged = true; });

            mockContext.Setup(c => c.UserIdentifier).Returns("1");
            //mockClients.Setup(c => c.All).Returns(mockProxy.Object);

            _chatHub.Clients = mockClients.Object;
            _chatHub.Context = mockContext.Object;

            //act
            await _chatHub.ServerChanged(1);

            //assert
            mockClients.Verify(c => c.Caller, Times.Once);
            Assert.True(serverChanged);
        }
    }
}