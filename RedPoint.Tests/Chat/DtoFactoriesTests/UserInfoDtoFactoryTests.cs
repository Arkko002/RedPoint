using System.Collections.Generic;
using System.Linq;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Chat.Dto;
using RedPoint.Chat.Services.DtoFactories;
using Xunit;

namespace RedPoint.Tests.Chat.DtoFactoriesTests
{
    public class UserInfoDtoFactoryTests
    {
        private readonly UserInfoDtoFactory _factory;

        public UserInfoDtoFactoryTests()
        {
            _factory = new UserInfoDtoFactory();
        }

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

            Assert.IsType<UserInfoDto>(dto);
            Assert.True(dto.AppUserId == "testId");
            Assert.True(dto.Username == "testName");
        }

        [Fact]
        public void CreateDtoList_ValidInput_ShouldReturnDtoList()
        {
            var list = new List<ChatUser>
            {
                new()
                {
                    Id = "testId",
                    UserName = "testName",
                    CurrentChannelId = "testChannelId",
                    CurrentServerId = "testServerId"
                },
                new()
                {
                    Id = "testId2",
                    UserName = "testName2",
                    CurrentChannelId = "testChannelId2",
                    CurrentServerId = "testServerId2"
                }
            };

            var dtoList = _factory.CreateDtoList(list);

            Assert.IsType<List<UserInfoDto>>(dtoList);
            Assert.True(dtoList.ToList().Count == 2);
        }
    }
}