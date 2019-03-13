using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RedPoint.Data;
using RedPoint.Infrastructure.Builders;
using RedPoint.Models;
using RedPoint.Models.Chat_Models;

namespace RedPoint.Tests.RedPoint.InfrastructureTests.BuildersTests
{
    [TestFixture]
    class ServerBuilderTests
    {
        private ServerBuilder _builder;
        private ApplicationDbContext _db;

        [OneTimeSetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ChatHubTestDb")
                .Options;

            _db = new ApplicationDbContext(options);

            _builder = new ServerBuilder(_db);
        }

        [Test]
        public async Task BuildServer()
        {
            //arrange
            ApplicationUser user = new ApplicationUser()
            {
                UserStub = new UserStub()
            };

            //act
            var server = await _builder.BuildServer("Test", "Test", true, user.UserStub, null);
            var dbServer = _db.Servers.Find(server.Id);

            //assert
            Assert.IsInstanceOf<Server>(server);
            Assert.IsInstanceOf<Server>(dbServer);
            Assert.IsTrue(server.Name == dbServer.Name && server.Description == dbServer.Description);
            Assert.IsTrue(server.Users.Contains(user.UserStub));
            Assert.IsTrue(server.Users.Contains(user.UserStub));
        }
    }
}
