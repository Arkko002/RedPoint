using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RedPoint.Data;
using RedPoint.Hubs;
using RedPoint.Models;
using Moq;
using Microsoft.AspNetCore.SignalR;
using System.Dynamic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using RedPoint.Models.Chat_Models;

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


            Server server = new Server()
            {
                Name = "TestServer",
                Channels = new List<Channel>()
            };

            server.Channels.Add(new Channel()
            {
                Name = "Channel_1",
                Description = String.Empty,
                ChannelStub = new ChannelStub()
                {
                    Name = "Channel_1",
                    Description = String.Empty,
                }
            });
        }

        [Test]
        public async Task Send()
        {
            //arrange
            bool sendCalled = false;
            dynamic all = new ExpandoObject();
            all.AddNewMessage = new Action<Models.Chat_Models.Message>((message => { sendCalled = true; }));

            Mock<IHubCallerClients> mockClients = new Mock<IHubCallerClients>();
            Mock<IClientProxy> mockClientProxy = new Mock<IClientProxy>();
            mockClients.Setup(c => c.All).Returns(mockClientProxy.Object);

            _chatHub.Clients = (IHubCallerClients<IChatHub>) mockClients.Object;

            //act
            await _chatHub.Send("TestMessage", "Channel_1");

            //assert
            Assert.IsTrue(sendCalled);
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
        public void ServerChanged()
        {

        }
    }
}