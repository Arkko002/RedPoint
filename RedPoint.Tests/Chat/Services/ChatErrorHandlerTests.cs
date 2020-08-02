using Microsoft.Extensions.Logging;
using Moq;
using NLog;
using RedPoint.Chat.Models;
using RedPoint.Chat.Services;
using RedPoint.Chat.Services.Security;
using RedPoint.Chat.Exceptions;
using RedPoint.Chat.Exceptions.Security;
using Xunit;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace RedPoint.Tests.Chat
{
    //TODO Verify logging   
    public class ChatErrorHandlerTests
    {
        private readonly ChatErrorHandler _errorHandler;
        private readonly Mock<ILogger<ChatControllerService>> _logger;
        private readonly ChatUser _user;

        public ChatErrorHandlerTests()
        {
            _user = new ChatUser() { UserName = "testUsername" };
            _logger = new Mock<ILogger<ChatControllerService>>();
            _errorHandler = new ChatErrorHandler(_logger.Object);
        }

        [Fact]
        public void HandleChatError_InvalidServerRequest_ShouldLogAndThrowException()
        {
            var error = new ChatError(ChatErrorType.UserNotInServer, _user, LogLevel.Critical, string.Empty);

            Assert.Throws<InvalidServerRequestException>(() => _errorHandler.HandleChatError(error));
            _logger.Verify(x => x.Log(LogLevel.Critical, It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [ClassData(typeof(ChatErrorClassData))]
        public void HandleChatError_EntityNotFound_ShouldLogAndThrowException(ChatError error)
        {
            Assert.Throws<EntityNotFoundException>(() => _errorHandler.HandleChatError(error));
            _logger.Verify(x => x.Log(LogLevel.Critical, It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void HandleChatError_LackOfPermission_ShouldLogAndThrowException()
        {
            var error = new ChatError(ChatErrorType.NoPermission, _user, LogLevel.Critical, string.Empty);

            Assert.Throws<LackOfPermissionException>(() => _errorHandler.HandleChatError(error));
            _logger.Verify(x => x.Log(LogLevel.Critical, It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void HandleChatError_NoError_ShouldntThrowException()
        {
            var error = new ChatError(ChatErrorType.NoError);

            _errorHandler.HandleChatError(error);
        }
    }
}