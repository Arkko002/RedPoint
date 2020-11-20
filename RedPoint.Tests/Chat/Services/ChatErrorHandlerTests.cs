using System;
using System.IO;
using NLog;
using NLog.Config;
using RedPoint.Chat.Exceptions;
using RedPoint.Chat.Exceptions.Security;
using RedPoint.Chat.Models;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Errors;
using RedPoint.Chat.Services;
using RedPoint.Chat.Services.Security;
using Xunit;

namespace RedPoint.Tests.Chat.Services
{
    //TODO Verify logging   
    public class ChatErrorHandlerTests
    {
        private readonly ChatErrorHandler _errorHandler;
        private readonly ChatUser _user;

        public ChatErrorHandlerTests() 
        {
            var configPath = Path.Join(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "nlog.config");
            var logFactory = new LogFactory();
            logFactory.Configuration = new XmlLoggingConfiguration(configPath, logFactory);

            var logger = logFactory.GetCurrentClassLogger();
            _user = new ChatUser() { UserName = "testUsername" };
            _errorHandler = new ChatErrorHandler(logger);
        }

        [Fact]
        public void HandleChatError_InvalidServerRequest_ShouldLogAndThrowException()
        {
            var error = new ChatError(ChatErrorType.UserNotInServer, _user, LogLevel.Fatal, string.Empty);

            Assert.Throws<InvalidServerRequestException>(() => _errorHandler.HandleChatError(error));
        }

        [Theory]
        [ClassData(typeof(ChatErrorClassData))]
        public void HandleChatError_EntityNotFound_ShouldLogAndThrowException(ChatError error)
        {
            Assert.Throws<EntityNotFoundException>(() => _errorHandler.HandleChatError(error));
        }

        [Fact]
        public void HandleChatError_LackOfPermission_ShouldLogAndThrowException()
        {
            var error = new ChatError(ChatErrorType.NoPermission, _user, LogLevel.Fatal, string.Empty);

            Assert.Throws<LackOfPermissionException>(() => _errorHandler.HandleChatError(error));
        }

        [Fact]
        public void HandleChatError_NoError_ShouldntThrowException()
        {
            var error = new ChatError(ChatErrorType.NoError);

            _errorHandler.HandleChatError(error);
        }
    }
}