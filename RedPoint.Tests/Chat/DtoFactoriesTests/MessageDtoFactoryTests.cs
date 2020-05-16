using System;
using System.Collections.Generic;
using Moq;
using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Models.Dto;
using RedPoint.Areas.Chat.Services.DtoFactories;
using Xunit;

namespace RedPoint.Tests.Chat.DtoFactoriesTests
{
    public class MessageDtoFactoryTests
    {
        private MessageDtoFactory _factory;

        public MessageDtoFactoryTests()
        {
            _factory = new MessageDtoFactory();
        }


        [Fact]
        public void CreateDto_ValidInput_ReturnObjectWithCorrectValues()
        {
            ApplicationUser user = new ApplicationUser
            {
                Id = "testId"
            };

            Message msg = new Message
            {
                Id = 123,
                DateTimePosted = DateTime.Today,
                Text = "test",
                User = user
            };

            var returnDto = _factory.CreateDto(msg);

            Assert.IsType<MessageDto>(returnDto);
            Assert.True(returnDto.Id == 123);
            Assert.True(returnDto.DateTimePosted == DateTime.Today.ToString());
            Assert.True(returnDto.Text == "test");
            Assert.True(returnDto.UserId == user.Id);
        }

        [Fact]
        public void CreateDto_NullFields_ShouldThrowException()
        {
            //TODO
        }
        

        [Fact]
        public void CreateDtoList_ValidInput_ReturnsListOfDto()
        {
            List<Message> msgList = new List<Message>
            {
                new Message{ Id = 1, Text = "1", DateTimePosted = DateTime.Today, User = new ApplicationUser() },
                new Message{ Id = 2, Text = "2", DateTimePosted = DateTime.Today, User = new ApplicationUser() },
                new Message{ Id = 3, Text = "3", DateTimePosted = DateTime.Today, User = new ApplicationUser() }
            };

            var returnList = _factory.CreateDtoList(msgList);

            Assert.IsType<List<MessageDto>>(returnList);
            Assert.True(returnList.Count == 3);
        }
    }
}