using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RedPoint.Models;
using RedPoint.Models.Chat_Models;
using RedPoint.Models.Users_Permissions_Models;

namespace RedPoint.Tests.RedPoint.InfrastructureTests
{
    [TestFixture]
    class PermissionManagerTests
    {
        private Server server;
        private Group userGroup;
        private ApplicationUser user;
      
        [SetUp]
        public void SetUp()
        {
            server = new Server();
            userGroup = new Group();
            server.Groups = new List<Group>();
            server.Groups.Add(userGroup);


            user = new ApplicationUser()
            {
                Id = "TestId",
                UserName = "Test"

            };
            UserStub userStub = new UserStub()
            {
                AppUserId = "TestId",
                AppUserName = "Test"
            };

            user.UserStub = userStub;
            user.Servers = new List<Server>();
            user.Servers.Add(server);
            user.Groups = new List<Group>();
            user.Groups.Add(userGroup);
            userGroup.Users = new List<UserStub>();
            userGroup.Users.Add(userStub);
        }

        [Test]
        public void CheckUserServerPermissions_HasPermission()
        {
            //arrange
            userGroup.GroupPermissions = new GroupPermissions()
            {
                CanView = true,
            };
            
            PermissionsManager permissionsManager = new PermissionsManager();

            //act
            bool result = permissionsManager.CheckUserServerPermissions(user, server, new[] {PermissionTypes.CanView});

            //assert
            Assert.IsTrue(result);
        }

        [Test]
        public void CheckUserServerPermissions_NoPermission()
        {
            //arrange
            userGroup.GroupPermissions = new GroupPermissions()
            {
                CanView = false,
            };

            PermissionsManager permissionsManager = new PermissionsManager();

            //act
            bool result = permissionsManager.CheckUserServerPermissions(user, server, new[] { PermissionTypes.CanView });

            //assert
            Assert.IsFalse(result);

        }        
    }
}
