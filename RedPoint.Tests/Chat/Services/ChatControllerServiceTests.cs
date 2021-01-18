using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Chat.Dto;
using RedPoint.Chat.Models.Errors;
using RedPoint.Chat.Services;
using RedPoint.Chat.Services.DtoFactories;
using RedPoint.Chat.Services.Security;
using Xunit;

namespace RedPoint.Tests.Chat.Services
{
    public class ChatControllerServiceTests
    {
        private readonly Mock<IChatEntityRepositoryProxy> _repoProxy;
        private readonly Mock<IChatRequestValidator> _requestValidator;

        private readonly ChatControllerService _service;

        public ChatControllerServiceTests()
        {
            var userManager = new Mock<UserManager<ChatUser>>(new Mock<IUserStore<ChatUser>>().Object,
                null, null, null, null, null, null, null, null);
            _repoProxy = new Mock<IChatEntityRepositoryProxy>();
            _requestValidator = new Mock<IChatRequestValidator>();
            var errorHandler = new Mock<IChatErrorHandler>();
            var httpContextAccessor = new Mock<IHttpContextAccessor>();

            var user = new ChatUser();
            user.Groups = new List<Group>();
            user.Servers = new List<Server>();
            user.Messages = new List<Message>();

            httpContextAccessor.Setup(x => x.HttpContext.User).Returns(new ClaimsPrincipal());

            userManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(user);

            _service = new ChatControllerService(userManager.Object, _repoProxy.Object, _requestValidator.Object,
                errorHandler.Object, httpContextAccessor.Object);
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

            _repoProxy.Setup(x => x.TryFindingServer(It.IsAny<int>(), It.IsAny<ChatUser>()))
                .Returns(new Server());
            mockFactory.Setup(x => x.CreateDto(It.IsAny<Server>()))
                .Returns(new ServerDataDto());

            _service.GetServerData(1, mockFactory.Object);

            _repoProxy.Verify(x => x.TryFindingServer(It.IsAny<int>(), It.IsAny<ChatUser>()), Times.Once);
            mockFactory.Verify(x => x.CreateDto(It.IsAny<Server>()), Times.Once);
        }

        [Fact]
        public void GetChannelMessages_ShouldCallDtoFactory()
        {
            _repoProxy.Setup(x => x.TryFindingChannel(It.IsAny<int>(), It.IsAny<ChatUser>()))
                .Returns(new Channel());
            _repoProxy.Setup(x => x.TryFindingServer(It.IsAny<int>(), It.IsAny<ChatUser>()))
                .Returns(new Server());
            _requestValidator.Setup(x => x.IsChannelRequestValid(It.IsAny<Channel>(), It.IsAny<Server>(),
                    It.IsAny<ChatUser>(), It.IsAny<PermissionTypes>()))
                .Returns(new ChatError(ChatErrorType.NoError));

            var mockFactory = new Mock<IChatDtoFactory<Message, MessageDto>>();
            mockFactory.Setup(x => x.CreateDtoList(It.IsAny<List<Message>>()))
                .Returns(new List<MessageDto>());

            _service.GetChannelMessages(1, 1, mockFactory.Object);

            _repoProxy.Verify(x => x.TryFindingChannel(It.IsAny<int>(), It.IsAny<ChatUser>()), Times.Once);
            _repoProxy.Verify(x => x.TryFindingServer(It.IsAny<int>(), It.IsAny<ChatUser>()), Times.Once);
            _requestValidator.Verify(x => x.IsChannelRequestValid(It.IsAny<Channel>(), It.IsAny<Server>(),
                It.IsAny<ChatUser>(), It.IsAny<PermissionTypes>()), Times.Once);
            mockFactory.Verify(x => x.CreateDtoList(It.IsAny<List<Message>>()), Times.Once);
        }
    }
}