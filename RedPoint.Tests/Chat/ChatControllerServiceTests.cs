using Microsoft.AspNetCore.Identity;
using Moq;
using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Chat.Services;
using RedPoint.Areas.Chat.Services.Security;
using RedPoint.Tests.Account;

namespace RedPoint.Tests.Chat
{
    public class ChatControllerServiceTests
    {
        private Mock<MockUserManager<ApplicationUser>> _userManager;
        private Mock<IChatEntityRepositoryProxy> _repoProxy;
        private Mock<IChatRequestValidator> _requestValidator; 
        private Mock<IChatErrorHandler> _errorHandler;
        
        private ChatControllerService _service;

        public ChatControllerServiceTests()
        {
            _userManager = new Mock<MockUserManager<ApplicationUser>>();
            _repoProxy = new Mock<IChatEntityRepositoryProxy>();
            _requestValidator = new Mock<IChatRequestValidator>();
            _errorHandler = new Mock<IChatErrorHandler>();
            
            _service = new ChatControllerService(_userManager.Object, _repoProxy.Object, _requestValidator.Object, _errorHandler.Object);
        }
        
        
    }
}