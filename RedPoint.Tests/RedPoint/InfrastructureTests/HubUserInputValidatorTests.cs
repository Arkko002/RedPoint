using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Services;
using RedPoint.Areas.Identity.Models;
using RedPoint.Data;
using RedPoint.Models;

namespace RedPoint.Tests.RedPoint.InfrastructureTests
{
    [TestFixture]
    class HubUserInputValidatorTests
    {
        private ApplicationDbContext _db;
        private HubUserInputValidator _inputValidator;
        private ApplicationUser _user;
        private Channel _channel;
        private Server _server;
        private Group _group;

        [OneTimeSetUp]
        public async Task SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ChatHubTestDb")
                .Options;

            _db = new ApplicationDbContext(options);


            var userId = Guid.NewGuid().ToString();
            _user = new ApplicationUser()
            {
                UserName = "Test",
                Id = userId,
                Email = "test@test.com",
            };

            var userStub = new UserDTO()
            {
                AppUserId = userId,
                AppUserName = _user.UserName
            };
            _user.UserDto = userStub;

            var userStubs = new List<UserDTO>();
            userStubs.Add(userStub);

            _server = new Server()
            {
                Name = "TestServer",
                Channels = new List<Channel>(),
                Users = userStubs,                
            };

            _group = new Group()
            {
                GroupPermissions = new GroupPermissions(),
                Name = "Default",
                Server = new ServerStub()
                {
                    Id = _server.Id,
                    Name = _server.Name,
                },
                Users = userStubs
                
            };

            var groupList = new List<Group>();
            groupList.Add(_group);

            _channel = new Channel()
            {
                Name = "Channel_1",
                Description = string.Empty,
                ChannelStub = new ChannelStub()
                {
                    Name = "Channel_1",
                    Description = string.Empty,
                },
                Groups = groupList
            };

            _server.Channels.Add(_channel);
            

            _db.Users.Add(_user);
            _db.Servers.Add(_server);
            _db.Channels.Add(_channel);
            _db.Groups.Add(_group);
            await _db.SaveChangesAsync();

            _inputValidator = new HubUserInputValidator(_db);
        }

        [Test]
        public void CheckCreatedMessage()
        {
            //act
            var result = _inputValidator.CheckCreatedMessage(_user, "TestMsg", "Channel_1", out var channel);

            //assert
            Assert.IsInstanceOf(typeof(UserInputError), result);
            Assert.IsTrue(result == UserInputError.InputValid);
        }

        [Test]
        public void CheckCreatedMessage_NoChannel()
        {
            //act
            var result = _inputValidator.CheckCreatedMessage(_user, "TestMsg", "Channel_2", out var channel);

            //assert
            Assert.IsInstanceOf(typeof(UserInputError), result);
            Assert.IsTrue(result == UserInputError.NoChannel);
        }

        [Test]
        public void CheckCreatedMessage_NoWritePermission()
        {
            //arrange
            _group.GroupPermissions.CanWrite = false;

            //act
            var result = _inputValidator.CheckCreatedMessage(_user, "TestMsg", "Channel_1", out var channel);

            //assert
            Assert.IsInstanceOf(typeof(UserInputError), result);
            Assert.IsTrue(result == UserInputError.NoPermission_CantWrite);
        }
    }
}
