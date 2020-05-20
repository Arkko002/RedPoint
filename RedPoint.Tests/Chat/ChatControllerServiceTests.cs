using Microsoft.AspNetCore.Http;
using Moq;
using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Chat.Services;
using RedPoint.Areas.Chat.Services.Security;
using RedPoint.Tests.Mocks;
using Xunit;

namespace RedPoint.Tests.Chat
{
    public class ChatControllerServiceTests
    {
        private readonly Mock<IChatErrorHandler> _errorHandler;
        private readonly Mock<IChatEntityRepositoryProxy> _repoProxy;
        private readonly Mock<IChatRequestValidator> _requestValidator;
        private readonly Mock<MockUserManager<ApplicationUser>> _userManager;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessor;

        private ChatControllerService _service;

        //TODO
        public ChatControllerServiceTests()
        {
            _userManager = new Mock<MockUserManager<ApplicationUser>>();
            _repoProxy = new Mock<IChatEntityRepositoryProxy>();
            _requestValidator = new Mock<IChatRequestValidator>();
            _errorHandler = new Mock<IChatErrorHandler>();
            _httpContextAccessor = new Mock<IHttpContextAccessor>();

            _service = new ChatControllerService(_userManager.Object, _repoProxy.Object, _requestValidator.Object,
                _errorHandler.Object, _httpContextAccessor.Object);
        }

        [Fact]
        public void GetUserServers_UserWithServers_ShouldReturnServerIconDtoList()
        {
            
        }
    }
}