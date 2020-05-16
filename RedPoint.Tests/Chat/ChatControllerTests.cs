using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RedPoint.Areas.Chat.Controllers;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Models.Dto;
using RedPoint.Areas.Chat.Services;
using RedPoint.Areas.Chat.Services.DtoFactories;
using Xunit;

namespace RedPoint.Tests.Chat
{
    public class ChatControllerTests
    {
        private Mock<IChatControllerService> _service;
        private ChatController _controller;

        public ChatControllerTests()
        {
            _service = new Mock<IChatControllerService>();
            _controller = new ChatController(_service.Object);
        }

        [Fact]
        public void GetUserServers_ShouldCallService()
        {
            List<ServerIconDto> mockList = new List<ServerIconDto>
            {
                new ServerIconDto
                {
                    Name = "CALLED"
                }
            };

            _service.Setup(x => x.GetUserServers(It.IsAny<IChatDtoFactory<Server, ServerIconDto>>()))
                .Returns(mockList);

            var returnValue = _controller.GetUserServers(new ServerIconDtoFactory()) as OkObjectResult;
            var returnList = returnValue.Value as List<ServerIconDto>;
            
            Assert.True(returnList[0].Name == "CALLED");
        }

        [Fact]
        public void GetServers_ShouldCallService()
        {
            ServerDataDto mockData = new ServerDataDto
            {
                Id = 12345
            };
            
            _service.Setup(x => x.GetServerData(It.IsAny<int>(),It.IsAny<IChatDtoFactory<Server, ServerDataDto>>()))
                .Returns(mockData);
            
            var returnValue = _controller.GetServer(1, new ServerDataDtoFactory(new ChannelIconDtoFactory(), new UserDtoFactory())) as OkObjectResult;
            var returnDto = returnValue.Value as ServerDataDto;
            
            Assert.True(returnDto.Id == 12345);
        }

        [Fact]
        public void GetChannelMessages_ShouldCallService()
        {
            List<MessageDto> mockList = new List<MessageDto>
            {
                new MessageDto
                {
                    Text = "CALLED"
                }
            };

            _service.Setup(x => x.GetChannelMessages(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<MessageDtoFactory>()))
                .Returns(mockList);
            
            var returnValue = _controller.GetChannelMessages(1, 1, new MessageDtoFactory()) as OkObjectResult;
            var returnList = returnValue.Value as List<MessageDto>;
            
            Assert.True(returnList[0].Text == "CALLED");
        }
    }
}