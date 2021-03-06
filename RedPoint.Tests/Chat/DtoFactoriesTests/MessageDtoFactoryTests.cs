using System;
using System.Collections.Generic;
using System.Globalization;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Chat.Dto;
using RedPoint.Chat.Services.DtoFactories;
using Xunit;

namespace RedPoint.Tests.Chat.DtoFactoriesTests
{
    public class MessageDtoFactoryTests
    {
        private readonly MessageDtoFactory _factory;

        public MessageDtoFactoryTests()
        {
            _factory = new MessageDtoFactory();
        }

        [Fact]
        public void CreateDto_NullFields_ShouldThrowException()
        {
            //TODO
        }


        [Fact]
        public void CreateDto_ValidInput_ReturnObjectWithCorrectValues()
        {
            var user = new ChatUser
            {
                Id = "testId"
            };

            var channel = new Channel(new Server("Test"), "Test");
            
            var msg = new Message(channel)
            {
                Id = 123,
                DateTimePosted = DateTime.Today,
                Text = "test",
                User = user
            };

            var returnDto = _factory.CreateDto(msg);

            Assert.IsType<MessageDto>(returnDto);
            Assert.True(returnDto.Id == 123);
            Assert.True(returnDto.DateTimePosted == DateTime.Today.ToString(CultureInfo.InvariantCulture));
            Assert.True(returnDto.Text == "test");
            Assert.True(returnDto.UserId == user.Id);
            Assert.True(returnDto.ChannelId == msg.Channel.Id);
        }


        [Fact]
        public void CreateDtoList_ValidInput_ReturnsListOfDto()
        {
            var msgList = new List<Message>
            {
                new(new Channel(new Server("Test"), "Test")) {Id = 1, Text = "1", DateTimePosted = DateTime.Today, User = new ChatUser()},
                new(new Channel(new Server("Test"), "Test")) {Id = 2, Text = "2", DateTimePosted = DateTime.Today, User = new ChatUser()},
                new(new Channel(new Server("Test"), "Test")) {Id = 3, Text = "3", DateTimePosted = DateTime.Today, User = new ChatUser()}
            };

            var returnList = _factory.CreateDtoList(msgList);

            Assert.IsType<List<MessageDto>>(returnList);
            Assert.True(returnList.Count == 3);
        }
    }
}