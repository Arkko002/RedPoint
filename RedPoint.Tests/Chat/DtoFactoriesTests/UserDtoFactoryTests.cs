using System.Collections.Generic;
using RedPoint.Chat.Models;
using RedPoint.Chat.Models.Dto;
using RedPoint.Chat.Services.DtoFactories;
using Xunit;

namespace RedPoint.Tests.Chat.DtoFactoriesTests
{
    public class UserDtoFactoryTests
    {
        public UserDtoFactoryTests()
        {
            _factory = new UserDtoFactory();
        }

        private readonly UserDtoFactory _factory;

        [Fact]
        public void CreateDto_ValidInput_ShouldReturnDtoObject()
        {
            var user = new ChatUser
            {
                Id = "testId",
                UserName = "testName",
                CurrentChannelId = "testChannelId",
                CurrentServerId = "testServerId"
            };

            var dto = _factory.CreateDto(user);

            Assert.IsType<ChatUserDto>(dto);
            Assert.True(dto.AppUserId == "testId");
            Assert.True(dto.Username == "testName");
            Assert.True(dto.CurrentChannelId == "testChannelId");
            Assert.True(dto.CurrentServerId == "testServerId");
        }

        [Fact]
        public void CreateDtoList_ValidInput_ShouldReturnDtoList()
        {
            var list = new List<ChatUser>
            {
                new ChatUser
                {
                    Id = "testId",
                    UserName = "testName",
                    CurrentChannelId = "testChannelId",
                    CurrentServerId = "testServerId"
                },
                new ChatUser
                {
                    Id = "testId2",
                    UserName = "testName2",
                    CurrentChannelId = "testChannelId2",
                    CurrentServerId = "testServerId2"
                }
            };

            var dtoList = _factory.CreateDtoList(list);

            Assert.IsType<List<ChatUserDto>>(dtoList);
            Assert.True(dtoList.Count == 2);
        }
    }
}