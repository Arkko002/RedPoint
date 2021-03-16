using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Chat.Dto;
using RedPoint.Chat.Services.DtoFactories;
using Xunit;

namespace RedPoint.Tests.Chat.DtoFactoriesTests
{
    public class ChannelDataDtoFactoryTests
    {
        private readonly ChannelDataDtoFactory _factory;

        public ChannelDataDtoFactoryTests()
        {
            var messageFactory = new MessageDtoFactory();
            
            _factory = new ChannelDataDtoFactory(messageFactory);
        }

        [Fact]
        public void CreateDto_ValidInput_ShouldReturnDtoObject()
        {
            var server = new Server("Test");
            var channel = new Channel(server, "Test")
            {
                Id = 1234
            };

            channel.Messages = new List<Message>()
            {
                new Message(channel)
                {
                    Id = 1,
                    DateTimePosted = DateTime.Now,
                    Text = "test",
                    User = new ChatUser()
                }
            };

            var dto = _factory.CreateDto(channel);

            Assert.IsType<ChannelDataDto>(dto);
            Assert.True(dto.Id == 1234);
            Assert.True(dto.Messages.ToList()[0].ChannelId == channel.Id);
            Assert.Same(server, channel.Server);
        }

        [Fact]
        public void CreateDtoList_ValidInput_ShouldReturnDtoList()
        {
            var list = new List<Channel>
            {
                new(new Server("Test"), "Test")
                {
                    Id = 1234
                },
                new(new Server("Test"), "Test")
                {
                    Id = 12345
                }
            };

            var dtoList = _factory.CreateDtoList(list);

            Assert.IsType<List<ChannelDataDto>>(dtoList);
            Assert.True(dtoList.ToList().Count == 2);
        }
    }
}