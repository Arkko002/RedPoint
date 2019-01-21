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

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ChatHubTestDb")
                .Options;

            _db = new ApplicationDbContext(options);
        }

        [Test]
        public void CheckIfUserStubExists()
        {
            //arrange
            ApplicationUser user = new ApplicationUser()
            {
                UserName = "Test",
                Id = "TestId"
            };

            //act
            UserStubManager.CheckIfUserStubExists(user, _db);
            var dbUserStub = _db.UserStubs.Find(user.UserStub.Id);

            //assert
            Assert.IsInstanceOf<UserStub>(user.UserStub);
            Assert.IsInstanceOf<UserStub>(dbUserStub);
        }


        //[Test]
        //public void CreateUserStub_ApplicationUserNotFound()
        //{
        //    //assert
        //    Assert.Throws<ApplicationUserNotFoundException>(() => _stubManager.CreateUserStub("NoUser"));
        //}
    }
}
