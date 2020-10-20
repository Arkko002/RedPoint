using System.Collections.Generic;
using Moq;
using RedPoint.Chat.Models;
using RedPoint.Chat.Models.Dto;
using RedPoint.Chat.Services.DtoFactories;
using Xunit;

namespace RedPoint.Tests.Chat.DtoFactoriesTests
{
    public class ChannelDataDtoFactoryTests
    {
        public ChannelDataDtoFactoryTests()
        {
            var mockMessageFactory = new Mock<IChatDtoFactory<Message, MessageDto>>();

            var messageList = new List<MessageDto> { new MessageDto { Text = "CALLED" } };
            mockMessageFactory.Setup(x => x.CreateDtoList(It.IsAny<List<Message>>()))
                .Returns(messageList);


            _factory = new ChannelDataDtoFactory(mockMessageFactory.Object);
        }

        private readonly ChannelDataDtoFactory _factory;

        [Fact]
        public void CreateDto_ValidInput_ShouldReturnDtoObject()
        {
            var channel = new Channel
            {
                Id = 1234
            };

            var dto = _factory.CreateDto(channel);

            Assert.IsType<ChannelDataDto>(dto);
            Assert.True(dto.Id == 1234);
            Assert.True(dto.Messages[0].Text == "CALLED");
        }

        [Fact]
        public void CreateDtoList_ValidInput_ShouldReturnDtoList()
        {
            var list = new List<Channel>
            {
                new Channel
                {
                    Id = 1234
                },
                new Channel
                {
                    Id = 12345
                }
            };

            var dtoList = _factory.CreateDtoList(list);

            Assert.IsType<List<ChannelDataDto>>(dtoList);
            Assert.True(dtoList.Count == 2);
        }
    }
}