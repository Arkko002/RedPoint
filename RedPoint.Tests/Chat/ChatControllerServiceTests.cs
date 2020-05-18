using Moq;
using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Chat.Services;
using RedPoint.Areas.Chat.Services.Security;
using RedPoint.Tests.Mocks;

namespace RedPoint.Tests.Chat
{
    public class ChatControllerServiceTests
    {
        private readonly Mock<IChatErrorHandler> _errorHandler;
        private readonly Mock<IChatEntityRepositoryProxy> _repoProxy;
        private readonly Mock<IChatRequestValidator> _requestValidator;
        private readonly Mock<MockUserManager<ApplicationUser>> _userManager;

        private ChatControllerService _service;

        //TODO
        public ChatControllerServiceTests()
        {
            _userManager = new Mock<MockUserManager<ApplicationUser>>();
            _repoProxy = new Mock<IChatEntityRepositoryProxy>();
            _requestValidator = new Mock<IChatRequestValidator>();
            _errorHandler = new Mock<IChatErrorHandler>();

            _service = new ChatControllerService(_userManager.Object, _repoProxy.Object, _requestValidator.Object,
                _errorHandler.Object);
        }
    }
}