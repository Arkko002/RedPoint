using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RedPoint.Data;
using RedPoint.Infrastructure.Builders;
using RedPoint.Models.Chat_Models;

namespace RedPoint.Tests.RedPoint.InfrastructureTests.BuildersTests
{
    [TestFixture]
    class ChannelBuilderTests
    {
        private ChannelBuilder _builder;
        private ApplicationDbContext _db;

        [OneTimeSetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ChatHubTestDb")
                .Options;

            _db = new ApplicationDbContext(options);

            _builder = new ChannelBuilder(_db);
        }

        [Test]
        public async Task BuildChannel()
        {
            //arrange
            Server server = new Server();

            //act
            var channel = await _builder.BuildChannel("Test", "Test", server);
            var dbChannel = _db.Channels.Find(channel.Id);

            //assert
            Assert.IsInstanceOf<Channel>(dbChannel);
            Assert.IsInstanceOf<Channel>(channel);
            Assert.IsTrue(channel.Name == dbChannel.Name && channel.Description == dbChannel.Description);
            Assert.IsTrue(server.Channels.Contains(channel));
            Assert.IsTrue(server.Channels.Contains(dbChannel));
        }
    }
}
