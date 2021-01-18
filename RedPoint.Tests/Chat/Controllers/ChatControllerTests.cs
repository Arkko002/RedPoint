using System.Collections.Generic;
using Moq;
using RedPoint.Chat.Controllers;
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
            _service = new Mock<IChatControllerService>();
            _controller = new ChatController(_service.Object);
        }

        [Fact]
        public void GetChannelMessages_ShouldCallService()
        {
            var mockList = new List<MessageDto>();
            _service.Setup(x => x.GetChannelMessages(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<MessageDtoFactory>()))
                .Returns(mockList);

            _controller.GetChannelMessages(1, 1, new MessageDtoFactory());

            _service.Verify(x => x.GetChannelMessages(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<MessageDtoFactory>()),
                Times.Once);
        }

        [Fact]
        public void GetServers_ShouldCallService()
        {
            var mockData = new ServerDataDto();
            _service.Setup(x => x.GetServerData(It.IsAny<int>(), It.IsAny<IChatDtoFactory<Server, ServerDataDto>>()))
                .Returns(mockData);

            _controller.GetServer(1, new ServerDataDtoFactory(new ChannelIconDtoFactory(), new UserDtoFactory()));

            _service.Verify(x => x.GetServerData(It.IsAny<int>(), It.IsAny<IChatDtoFactory<Server, ServerDataDto>>()),
                Times.Once);
        }

        [Fact]
        public void GetUserServers_ShouldCallService()
        {
            var mockList = new List<ServerIconDto>();
            _service.Setup(x => x.GetUserServers(It.IsAny<IChatDtoFactory<Server, ServerIconDto>>()))
                .Returns(mockList);

            var returnValue = _controller.GetUserServers(new ServerIconDtoFactory());

            _service.Verify(x => x.GetUserServers(It.IsAny<IChatDtoFactory<Server, ServerIconDto>>()), Times.Once);
        }
    }
}