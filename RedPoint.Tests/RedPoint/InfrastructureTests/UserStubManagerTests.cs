using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RedPoint.Data;
using RedPoint.Exceptions;
using RedPoint.Infrastructure;
using RedPoint.Models;
using RedPoint.Models.Chat_Models;

namespace RedPoint.Tests.RedPoint.InfrastructureTests
{
    [TestFixture]
    class UserStubManagerTests
    {
        private ApplicationDbContext _db;
        private UserStubManager _stubManager;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ChatHubTestDb")
                .Options;

            _db = new ApplicationDbContext(options);

            _stubManager = new UserStubManager(_db);
        }

        [Test]
        public void CreateUserStub_ApplicationUser()
        {
            //arrange
            ApplicationUser user = new ApplicationUser()
            {
                UserName = "Test",
                Id = "TestId"
            };

            //act
            var userStub = _stubManager.CreateUserStub(user);
            var dbUserStub = _db.UserStubs.Find(userStub.Id);

            //assert
            Assert.IsInstanceOf<UserStub>(userStub);
            Assert.IsInstanceOf<UserStub>(dbUserStub);
        }

        [Test]
        public void CreateUserStub_ApplicationUserId()
        {
            //arrange
            ApplicationUser user = new ApplicationUser()
            {
                UserName = "Test",
                Id = "TestId"                
            };

            _db.Users.Add(user);
            _db.SaveChanges();

            //act
            var userStub = _stubManager.CreateUserStub(user.Id);
            var dbUserStub = _db.UserStubs.Find(userStub.Id);

            //assert
            Assert.IsInstanceOf<UserStub>(userStub);
            Assert.IsInstanceOf<UserStub>(dbUserStub);
        }

        [Test]
        public void CreateUserStub_ApplicationUserNotFound()
        {
            //assert
            Assert.Throws<ApplicationUserNotFoundException>(() => _stubManager.CreateUserStub("NoUser"));
        }
    }
}
