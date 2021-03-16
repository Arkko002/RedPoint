using System.Collections.Generic;
using System.Linq;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Chat.Dto;
using RedPoint.Chat.Services.DtoFactories;
using Xunit;

namespace RedPoint.Tests.Chat.DtoFactoriesTests
{
    public class ServerInfoDtoFactoryTests
    {
        private readonly ServerInfoDtoFactory _factory;

        public ServerInfoDtoFactoryTests()
        {
            _factory = new ServerInfoDtoFactory();
        }

        [Fact]
        public void CreateDto_ValidInput_ShouldReturnDtoObject()
        {
            var server = new Server("Test")
            {
                Id = 1234,
                Name = "testName",
                Description = "testDescription",
                IsVisible = true
            };

            var dto = _factory.CreateDto(server);

            Assert.IsType<ServerInfoDto>(dto);
            Assert.True(dto.Id == 1234);
            Assert.True(dto.Name == "testName");
            Assert.True(dto.Description == "testDescription");
            Assert.True(dto.HubGroupId == server.GroupId);
            Assert.True(dto.IsVisible);
        }

        [Fact]
        public void CreateDtoList_ValidInput_ShouldReturnDtoList()
        {
            var list = new List<Server>
            {
                new("Test")
                {
                    Id = 1234,
                    Name = "testName",
                    Description = "testDescription",
                    IsVisible = true
                },
                new("Test")
                {
                    Id = 12345,
                    Name = "testName2",
                    Description = "testDescription2",
                    IsVisible = true
                }
            };

            var dtoList = _factory.CreateDtoList(list);

            Assert.IsType<List<ServerInfoDto>>(dtoList);
            Assert.True(dtoList.ToList().Count == 2);
        }
    }
}