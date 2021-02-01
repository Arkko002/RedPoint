using System.Collections.Generic;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Chat.Dto;
using RedPoint.Chat.Services.DtoFactories;
using Xunit;

namespace RedPoint.Tests.Chat.DtoFactoriesTests
{
    public class ChannelIconDtoFactoryTests
    {
        private readonly ChannelIconDtoFactory _factory;

        public ChannelIconDtoFactoryTests()
        {
            _factory = new ChannelIconDtoFactory();
        }

        [Fact]
        public void CreateDto_ValidInput_ShouldReturnDtoObject()
        {
            var server = new Server();
            var channel = new Channel(server)
            {
                Id = 1234,
                Name = "testName",
                Description = "testDescription"
            };

            var dto = _factory.CreateDto(channel);

            Assert.IsType<ChannelIconDto>(dto);
            Assert.True(dto.Id == 1234);
            Assert.True(dto.Name == "testName");
            Assert.True(dto.Description == "testDescription");
            Assert.Same(server, channel.Server);
        }

        [Fact]
        public void CreateDtoList_ValidInput_ShouldReturnDtoList()
        {
            var list = new List<Channel>
            {
                new(new Server())
                {
                    Id = 1234,
                    Name = "testName2",
                    Description = "testDescription2"
                },
                new(new Server())
                {
                    Id = 12345,
                    Name = "testName",
                    Description = "testDescription"
                }
            };

            var dtoList = _factory.CreateDtoList(list);

            Assert.IsType<List<ChannelIconDto>>(dtoList);
            Assert.True(dtoList.Count == 2);
        }
    }
}