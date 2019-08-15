using System.Collections.Generic;
using NUnit.Framework;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Services;
using RedPoint.Areas.Identity.Models;
using RedPoint.Models;

namespace RedPoint.Tests.RedPoint.InfrastructureTests
{
    [TestFixture]
    class PermissionManagerTests
    {
        private Server _server;
        private Group _userGroup;
        private ApplicationUser _user;
        private Channel _channel;
      
        [OneTimeSetUp]
        public void SetUp()
        {

            _user = new ApplicationUser()
            {
                Id = "TestId",
                UserName = "Test"

            };
            UserDTO userDto = new UserDTO()
            {
                AppUserId = "TestId",
                AppUserName = "Test"
            };

            var userStubs = new List<UserDTO>();
            userStubs.Add(userDto);

            _server = new Server()
            {
                Name = "TestServer",
                Channels = new List<Channel>(),
                Users = userStubs,
            };

            _userGroup = new Group()
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
            groupList.Add(_userGroup);

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

            _server.Groups = new List<Group>();
            _server.Groups.Add(_userGroup);


            _user.UserDto = userDto;
            _user.Servers = new List<Server>();
            _user.Servers.Add(_server);
            _user.Groups = new List<Group>();
            _user.Groups.Add(_userGroup);
            _userGroup.Users = new List<UserDTO>();
            _userGroup.Users.Add(userDto);
        }

        [Test]
        public void CheckUserServerPermissions_HasPermission()
        {
            //arrange
            _userGroup.GroupPermissions = new GroupPermissions()
            {
                CanView = true,
            };
            
            PermissionsManager permissionsManager = new PermissionsManager();

            //act
            bool result = permissionsManager.CheckUserServerPermissions(_user, _server, new[] {PermissionTypes.CanView});

            //assert
            Assert.IsTrue(result);
        }

        [Test]
        public void CheckUserServerPermissions_NoPermission()
        {
            //arrange
            _userGroup.GroupPermissions = new GroupPermissions()
            {
                CanView = false,
            };

            PermissionsManager permissionsManager = new PermissionsManager();

            //act
            bool result = permissionsManager.CheckUserServerPermissions(_user, _server, new[] { PermissionTypes.CanView });

            //assert
            Assert.IsFalse(result);

        }

        [Test]
        public void CheckUserChannelPermissions()
        {
            //arrange
            _userGroup.GroupPermissions = new GroupPermissions()
            {
                CanView = true,
            };

            PermissionsManager permissionsManager = new PermissionsManager();

            //act
            bool result = permissionsManager.CheckUserChannelPermissions(_user, _channel, new[] { PermissionTypes.CanView });

            //assert
            Assert.IsTrue(result);
        }

        [Test]
        public void CheckUserChannelPermissions_NoPermission()
        {
            //arrange
            _userGroup.GroupPermissions = new GroupPermissions()
            {
                CanView = false,
            };

            PermissionsManager permissionsManager = new PermissionsManager();

            //act
            bool result = permissionsManager.CheckUserChannelPermissions(_user, _channel, new[] { PermissionTypes.CanView });

            //assert
            Assert.IsFalse(result);
        }
    }
}
