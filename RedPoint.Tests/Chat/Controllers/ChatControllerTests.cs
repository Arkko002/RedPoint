using System.Web;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Moq;
using RedPoint.Chat.Controllers;
using RedPoint.Chat.Data;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Chat.Dto;
using RedPoint.Chat.Services;
using RedPoint.Chat.Services.DtoFactories;
using Xunit;

namespace RedPoint.Tests.Chat.Controllers
{
    public class ChatControllerTests
    {
        private readonly ChatController _controller;

        private readonly Mock<IChatControllerService> _service;

        public ChatControllerTests()
        {
            var userRepo = new Mock<IChatEntityRepositoryProxy<ChatUser, ChatDbContext>>();
            userRepo.Setup(x => x.Find(It.IsAny<object[]>()))
                .Returns(new ChatUser());

            
            _service = new Mock<IChatControllerService>();
            _controller = new ChatController(_service.Object);
        }

        [Fact]
        public void GetChannelMessages_ShouldCallService()
        {
            var mockList = new List<MessageDto>();
            _service.Setup(x => x.GetChannelMessages(It.IsAny<int>(),
                    It.IsAny<IChatDtoFactory<Message,MessageDto>>(),
                    It.IsAny<ChatEntityRepositoryProxy<Channel,ChatDbContext>>()))
                .Returns(mockList);

            _controller.GetChannelMessages(1, null, null);

            _service.Verify(x => x.GetChannelMessages(It.IsAny<int>(),
                    It.IsAny<IChatDtoFactory<Message,MessageDto>>(),
                    It.IsAny<ChatEntityRepositoryProxy<Channel,ChatDbContext>>()),
                Times.Once);
        }

        [Fact]
        public void GetServers_ShouldCallService()
        {
            var mockData = new ServerDataDto();
            _service.Setup(x => x.GetServerData(It.IsAny<int>(), It.IsAny<IChatDtoFactory<Server,ServerDataDto>>(), It.IsAny<ChatEntityRepositoryProxy<Server,ChatDbContext>>()))
                .Returns(mockData);

            _controller.GetServer(1, null, null);

            _service.Verify(x => x.GetServerData(It.IsAny<int>(),
                    It.IsAny<IChatDtoFactory<Server,ServerDataDto>>(),
                    It.IsAny<ChatEntityRepositoryProxy<Server,ChatDbContext>>()),
                Times.Once);
        }

        [Fact]
        public void GetUserServers_ShouldCallService()
        {
            var mockList = new List<ServerIconDto>();
            _service.Setup(x => x.GetUserServers(It.IsAny<IChatDtoFactory<Server, ServerIconDto>>()))
                .Returns(mockList);
            
            _controller.GetUserServers(null);

            _service.Verify(x => x.GetUserServers(It.IsAny<IChatDtoFactory<Server, ServerIconDto>>()), Times.Once);
        }
    }
}