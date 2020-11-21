using System.Collections.Generic;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Chat.Dto;
using RedPoint.Chat.Services.DtoFactories;
using Xunit;

namespace RedPoint.Tests.Chat.DtoFactoriesTests
{
    public class ServerIconDtoFactoryTests
    {
        public ServerIconDtoFactoryTests()
        {
            _factory = new ServerIconDtoFactory();
        }

        private readonly ServerIconDtoFactory _factory;

        [Fact]
        public void CreateDto_ValidInput_ShouldReturnDtoObject()
        {
            var server = new Server
            {
                Id = 1234,
                Name = "testName",
                Description = "testDescription",
                IsVisible = true
            };

            var dto = _factory.CreateDto(server);

            Assert.IsType<ServerIconDto>(dto);
            Assert.True(dto.Id == 1234);
            Assert.True(dto.Name == "testName");
            Assert.True(dto.Description == "testDescription");
            Assert.True(dto.IsVisible);
        }

        [Fact]
        public void CreateDtoList_ValidInput_ShouldReturnDtoList()
        {
            var list = new List<Server>
            {
                new Server
                {
                    Id = 1234,
                    Name = "testName",
                    Description = "testDescription",
                    IsVisible = true
                },
                new Server
                {
                    Id = 12345,
                    Name = "testName2",
                    Description = "testDescription2",
                    IsVisible = true
                }
            };

            var dtoList = _factory.CreateDtoList(list);

            Assert.IsType<List<ServerIconDto>>(dtoList);
            Assert.True(dtoList.Count == 2);
        }
    }
}