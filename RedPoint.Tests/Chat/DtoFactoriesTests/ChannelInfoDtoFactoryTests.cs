using System.Collections.Generic;
using System.Linq;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Chat.Dto;
using RedPoint.Chat.Services.DtoFactories;
using Xunit;

namespace RedPoint.Tests.Chat.DtoFactoriesTests
{
    public class ChannelInfoDtoFactoryTests
    {
        private readonly ChannelInfoDtoFactory _factory;

        public ChannelInfoDtoFactoryTests()
        {
            _factory = new ChannelInfoDtoFactory();
        }

        [Fact]
        public void CreateDto_ValidInput_ShouldReturnDtoObject()
        {
            var server = new Server("Test");
            var channel = new Channel(server, "Test")
            {
                Id = 1234,
                Name = "testName",
                Description = "testDescription"
            };

            var dto = _factory.CreateDto(channel);

            Assert.IsType<ChannelInfoDto>(dto);
            Assert.True(dto.Id == 1234);
            Assert.True(dto.Name == "testName");
            Assert.True(dto.Description == "testDescription");
            Assert.True(dto.HubGroupId == server.GroupId);
            Assert.Same(server, channel.Server);
        }

        [Fact]
        public void CreateDtoList_ValidInput_ShouldReturnDtoList()
        {
            var list = new List<Channel>
            {
                new(new Server("Test"), "Test")
                {
                    Id = 1234,
                    Name = "testName2",
                    Description = "testDescription2"
                },
                new(new Server("Test"), "Test")
                {
                    Id = 12345,
                    Name = "testName",
                    Description = "testDescription"
                }
            };

            var dtoList = _factory.CreateDtoList(list);

            Assert.IsType<List<ChannelInfoDto>>(dtoList);
            Assert.True(dtoList.ToList().Count == 2);
        }
    }
}