using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Moq;
using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Models.Dto;
using RedPoint.Areas.Chat.Services;
using RedPoint.Areas.Chat.Services.DtoFactories;
using RedPoint.Areas.Chat.Services.Security;
using RedPoint.Tests.Mocks;
using Xunit;

namespace RedPoint.Tests.Chat
{
    //TODO Update all interaction tests to use Moq Verify, Times
    public class ChatControllerServiceTests
    {
        private readonly Mock<IChatErrorHandler> _errorHandler;
        private readonly Mock<IChatEntityRepositoryProxy> _repoProxy;
        private readonly Mock<IChatRequestValidator> _requestValidator;
        private readonly Mock<MockUserManager<ApplicationUser>> _userManager;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessor;
        private readonly ApplicationUser _user;

        private ChatControllerService _service;

        //TODO
        public ChatControllerServiceTests()
        {
            _userManager = new Mock<MockUserManager<ApplicationUser>>();
            _repoProxy = new Mock<IChatEntityRepositoryProxy>();
            _requestValidator = new Mock<IChatRequestValidator>();
            _errorHandler = new Mock<IChatErrorHandler>();
            _httpContextAccessor = new Mock<IHttpContextAccessor>();

            _user = new ApplicationUser();
            _user.Groups = new List<Group>();
            _user.Servers = new List<Server>();
            _user.Messages = new List<Message>();

            _httpContextAccessor.Setup(x => x.HttpContext.User).Returns(new ClaimsPrincipal());
            
            _userManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(_user);

            _service = new ChatControllerService(_userManager.Object, _repoProxy.Object, _requestValidator.Object,
                _errorHandler.Object, _httpContextAccessor.Object);
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
            
            _repoProxy.Setup(x => x.TryFindingServer(It.IsAny<int>(), It.IsAny<ApplicationUser>()))
                .Returns(new Server());
            mockFactory.Setup(x => x.CreateDto(It.IsAny<Server>()))
                .Returns(new ServerDataDto());

            _service.GetServerData(1, mockFactory.Object);
            
            _repoProxy.Verify(x => x.TryFindingServer(It.IsAny<int>(), It.IsAny<ApplicationUser>()), Times.Once);
            mockFactory.Verify(x => x.CreateDto(It.IsAny<Server>()), Times.Once);
        }

        [Fact]
        public void GetChannelMessages_ShouldCallDtoFactory()
        {
            _repoProxy.Setup(x => x.TryFindingChannel(It.IsAny<int>(), It.IsAny<ApplicationUser>()))
                .Returns(new Channel());
            _repoProxy.Setup(x => x.TryFindingServer(It.IsAny<int>(), It.IsAny<ApplicationUser>()))
                .Returns(new Server());
            _requestValidator.Setup(x => x.IsChannelRequestValid(It.IsAny<Channel>(), It.IsAny<Server>(),
                    It.IsAny<ApplicationUser>(), It.IsAny<PermissionType>()))
                .Returns(new ChatError(ChatErrorType.NoError));
                
            var mockFactory = new Mock<IChatDtoFactory<Message, MessageDto>>();
            mockFactory.Setup(x => x.CreateDtoList(It.IsAny<List<Message>>()))
                .Returns(new List<MessageDto>());

            _service.GetChannelMessages(1, 1, mockFactory.Object);
            
            _repoProxy.Verify(x => x.TryFindingChannel(It.IsAny<int>(), It.IsAny<ApplicationUser>()), Times.Once);
            _repoProxy.Verify(x => x.TryFindingServer(It.IsAny<int>(), It.IsAny<ApplicationUser>()), Times.Once);
            _requestValidator.Verify(x => x.IsChannelRequestValid(It.IsAny<Channel>(), It.IsAny<Server>(),
                It.IsAny<ApplicationUser>(), It.IsAny<PermissionType>()), Times.Once);
            mockFactory.Verify(x => x.CreateDtoList(It.IsAny<List<Message>>()), Times.Once);
        }
    }
}