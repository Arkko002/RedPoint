using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Moq;
using RedPoint.Chat.Data;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Chat.Dto;
using RedPoint.Chat.Services;
using RedPoint.Chat.Services.DtoFactories;
using RedPoint.Chat.Services.Security;
using Xunit;

namespace RedPoint.Tests.Chat.Services
{
    public class ChatControllerServiceTests
    {
        private readonly Mock<IChatRequestValidator> _requestValidator;

        private readonly ChatControllerService _service;

        public ChatControllerServiceTests()
        {
            _requestValidator = new Mock<IChatRequestValidator>();

            var user = new ChatUser();
            user.Groups = new List<Group>();
            user.Servers = new List<Server>();
            user.Messages = new List<Message>();

            var userRepo = new Mock<IChatEntityRepositoryProxy<ChatUser, ChatDbContext>>();
            userRepo.Setup(x => x.Find(It.IsAny<object[]>()))
                .Returns(user);

            var httpAccessor = new HttpContextAccessor();
            httpAccessor.HttpContext = new DefaultHttpContext();
            httpAccessor.HttpContext.Items["UserToken"] = new JwtSecurityToken();
            
            _service = new ChatControllerService(_requestValidator.Object, httpAccessor, userRepo.Object);
        }

        [Fact]
        public void GetUserServers_ShouldCallDtoFactory()
        {
            var mockFactory = new Mock<IChatDtoFactory<Server, ServerIconDto>>();
            mockFactory.Setup(x => x.CreateDtoList(It.IsAny<List<Server>>()))
                .Returns(new List<ServerIconDto>());

            _service.GetUserServers(mockFactory.Object);

            mockFactory.Verify(x => x.CreateDtoList(It.IsAny<List<Server>>()), Times.Once);
        }

        [Fact]
        public void GetServerData_ShouldCallDtoFactory()
        {
            var mockFactory = new Mock<IChatDtoFactory<Server, ServerDataDto>>();
            var mockRepo = new Mock<IChatEntityRepositoryProxy<Server, ChatDbContext>>();

            mockRepo.Setup(x => x.Find(It.IsAny<object[]>()))
                .Returns(new Server("Test"));
            mockFactory.Setup(x => x.CreateDto(It.IsAny<Server>()))
                .Returns(new ServerDataDto());

            _service.GetServerData(1, mockFactory.Object, mockRepo.Object);

            mockRepo.Verify(x => x.Find(It.IsAny<object[]>()), Times.Once);
            mockFactory.Verify(x => x.CreateDto(It.IsAny<Server>()), Times.Once);
        }

        [Fact]
        public void GetChannelMessages_ShouldCallDtoFactory()
        {
            var mockFactory = new Mock<IChatDtoFactory<Message, MessageDto>>();
            var mockRepo = new Mock<IChatEntityRepositoryProxy<Channel, ChatDbContext>>();
            
            mockFactory.Setup(x => x.CreateDtoList(It.IsAny<List<Message>>()))
                .Returns(new List<MessageDto>());
            mockRepo.Setup(x => x.Find(It.IsAny<object[]>()))
                .Returns(new Channel(new Server("Test"), "Test"));

            _service.GetChannelMessages(1, mockFactory.Object, mockRepo.Object);

            mockRepo.Verify(x => x.Find(It.IsAny<object[]>()), Times.Once);
            _requestValidator.Verify(x => x.IsChannelRequestValid(It.IsAny<Channel>(), It.IsAny<Server>(),
                It.IsAny<ChatUser>(), It.IsAny<PermissionTypes>()), Times.Once);
            mockFactory.Verify(x => x.CreateDtoList(It.IsAny<List<Message>>()), Times.Once);
        }
    }
}