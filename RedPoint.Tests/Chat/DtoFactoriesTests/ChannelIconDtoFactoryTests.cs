using System.Collections.Generic;
using RedPoint.Chat.Models;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Chat.Dto;
using RedPoint.Chat.Services.DtoFactories;
using Xunit;

namespace RedPoint.Tests.Chat.DtoFactoriesTests
{
    public class ChannelIconDtoFactoryTests
    {
        public ChannelIconDtoFactoryTests()
        {
            _factory = new ChannelIconDtoFactory();
        }

        private readonly ChannelIconDtoFactory _factory;

        [Fact]
        public void CreateDto_ValidInput_ShouldReturnDtoObject()
        {
            var channel = new Channel
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
        }

        [Fact]
        public void CreateDtoList_ValidInput_ShouldReturnDtoList()
        {
            var list = new List<Channel>
            {
                new Channel
                {
                    Id = 1234,
                    Name = "testName2",
                    Description = "testDescription2"
                },
                new Channel
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