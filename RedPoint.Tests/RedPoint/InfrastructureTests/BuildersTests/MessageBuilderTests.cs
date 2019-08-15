using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Services.Builders;
using RedPoint.Areas.Identity.Models;
using RedPoint.Data;
using RedPoint.Models;

namespace RedPoint.Tests.RedPoint.InfrastructureTests.BuildersTests
{
    [TestFixture]
    class MessageBuilderTests
    {
        private MessageBuilder _builder;
        private ApplicationDbContext _db;

        [OneTimeSetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ChatHubTestDb")
                .Options;

            _db = new ApplicationDbContext(options);

            _builder = new MessageBuilder(_db);
        }

        [Test]
        public async Task BuildMessage()
        {
            //arrange
            ApplicationUser user = new ApplicationUser()
            {
                UserDto = new UserDTO()
            };

            Channel channel = new Channel();

            //act
            var message = await _builder.BuildMessage(user.UserDto, "Test", channel);
            var dbMessage = _db.Messages.Find(message.Id);

            //assert
            Assert.IsInstanceOf<Message>(message);
            Assert.IsInstanceOf<Message>(dbMessage);
            Assert.IsTrue(message.Text == dbMessage.Text);
            Assert.IsTrue(message.UserDto == user.UserDto);
            Assert.IsTrue(message.ChannelStub == channel.ChannelStub);
        }
    }
}
