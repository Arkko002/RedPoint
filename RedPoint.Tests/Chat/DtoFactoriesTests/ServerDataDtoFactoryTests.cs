using System.Collections.Generic;
using Moq;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Chat.Dto;
using RedPoint.Chat.Services.DtoFactories;
using Xunit;

namespace RedPoint.Tests.Chat.DtoFactoriesTests
{
    public class ServerDataDtoFactoryTests
    {
        private readonly ServerDataDtoFactory _factory;

        public ServerDataDtoFactoryTests()
        {
            var mockChannelDtoFactory = new Mock<IChatDtoFactory<Channel, ChannelIconDto>>();
            var mockUserDtoFactory = new Mock<IChatDtoFactory<ChatUser, ChatUserDto>>();

            var channelDtoList = new List<ChannelIconDto> {new() {Name = "CALLED"}};
            mockChannelDtoFactory.Setup(x => x.CreateDtoList(It.IsAny<List<Channel>>()))
                .Returns(channelDtoList);

            var userDtoList = new List<ChatUserDto> {new() {Username = "CALLED"}};
            mockUserDtoFactory.Setup(x => x.CreateDtoList(It.IsAny<List<ChatUser>>()))
                .Returns(userDtoList);

            _factory = new ServerDataDtoFactory(mockChannelDtoFactory.Object, mockUserDtoFactory.Object);
        }

        [Fact]
        public void CreateDto_ValidInput_ShouldReturnDtoObject()
        {
            var server = new Server("Test")
            {
                Id = 1234
            };

            var dto = _factory.CreateDto(server);

            Assert.IsType<ServerDataDto>(dto);
            Assert.True(dto.ChannelList[0].Name == "CALLED");
            Assert.True(dto.UserList[0].Username == "CALLED");
        }

        [Fact]
        public void CreateDtoList_ValidInput_ShouldReturnDtoList()
        {
            var list = new List<Server>
            {
                new("Test")
                {
                    Id = 1234
                },
                new("Test")
                {
                    Id = 12345
                }
            };

            var dtoList = _factory.CreateDtoList(list);

            Assert.IsType<List<ServerDataDto>>(dtoList);
            Assert.True(dtoList.Count == 2);
        }
    }
}