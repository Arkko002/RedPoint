using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RedPoint.Areas.Chat.Services;
using RedPoint.Areas.Identity.Models;
using RedPoint.Data;
using RedPoint.Models;

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
            var dbUserStub = _db.UserStubs.Find(user.UserDto.Id);

            //assert
            Assert.IsInstanceOf<UserDTO>(user.UserDto);
            Assert.IsInstanceOf<UserDTO>(dbUserStub);
        }

        //[Test]
        //public void CreateUserStub_ApplicationUserNotFound()
        //{
        //    //assert
        //    Assert.Throws<ApplicationUserNotFoundException>(() => _stubManager.CreateUserStub("NoUser"));
        //}
    }
}
