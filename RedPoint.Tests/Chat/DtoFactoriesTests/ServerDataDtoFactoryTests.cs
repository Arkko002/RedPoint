using System.Collections.Generic;
using Moq;
using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Models.Dto;
using RedPoint.Areas.Chat.Services.DtoFactories;
using Xunit;

namespace RedPoint.Tests.Chat.DtoFactoriesTests
{
    public class ServerDataDtoFactoryTests
    {
        public ServerDataDtoFactoryTests()
        {
            var mockChannelDtoFactory = new Mock<IChatDtoFactory<Channel, ChannelIconDto>>();
            var mockUserDtoFactory = new Mock<IChatDtoFactory<ApplicationUser, UserChatDto>>();

            var channelDtoList = new List<ChannelIconDto> {new ChannelIconDto {Name = "CALLED"}};
            mockChannelDtoFactory.Setup(x => x.CreateDtoList(It.IsAny<List<Channel>>()))
                .Returns(channelDtoList);

            var userDtoList = new List<UserChatDto> {new UserChatDto {Username = "CALLED"}};
            mockUserDtoFactory.Setup(x => x.CreateDtoList(It.IsAny<List<ApplicationUser>>()))
                .Returns(userDtoList);

            _factory = new ServerDataDtoFactory(mockChannelDtoFactory.Object, mockUserDtoFactory.Object);
        }

        private readonly ServerDataDtoFactory _factory;

        [Fact]
        public void CreateDto_ValidInput_ShouldReturnDtoObject()
        {
            var server = new Server
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
                new Server
                {
                    Id = 1234
                },
                new Server
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