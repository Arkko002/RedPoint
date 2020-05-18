using System;
using Microsoft.Extensions.Logging;
using RedPoint.Areas.Chat.Services.Security;
using RedPoint.Exceptions;
using RedPoint.Exceptions.Security;

namespace RedPoint.Areas.Chat.Services
{
    public class ChatErrorHandler : IChatErrorHandler
    {
        private readonly ILogger<ChatControllerService> _logger;

        public ChatErrorHandler(ILogger<ChatControllerService> logger)
        {
            _logger = logger;
        }

        public void HandleChatError(ChatError chatError)
        {
            if (chatError.LogMessage != null)
            {
                _logger.Log(chatError.LogLevel, chatError.LogMessage);
            }

            switch (chatError.ErrorType)
            {
                case ChatErrorType.UserNotInServer:
                    throw new InvalidServerRequestException($"{chatError.User.UserName} is not part of the server.");

                case ChatErrorType.ServerNotFound:
                    throw new EntityNotFoundException("No server found.");

                case ChatErrorType.ChannelNotFound:
                    throw new EntityNotFoundException("No channel found.");

                case ChatErrorType.NoPermission:
                    throw new LackOfPermissionException($"{chatError.User.UserName} has no required permission.");

                case ChatErrorType.NoError:
                    return;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}