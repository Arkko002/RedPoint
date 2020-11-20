using System;
using NLog;
using RedPoint.Chat.Services.Security;
using RedPoint.Chat.Exceptions;
using RedPoint.Chat.Exceptions.Security;
using RedPoint.Chat.Models.Errors;

namespace RedPoint.Chat.Services
{
    
    /// <inheritdoc/>
    public class ChatErrorHandler : IChatErrorHandler
    {
        private readonly Logger _logger;

        public ChatErrorHandler(Logger logger)
        {
            _logger = logger;
        }
        
        public void HandleChatError(ChatError error)
        {
            //TODO should non-critical chat errors be handled as exceptions? Rethink error handling.
            if (!string.IsNullOrEmpty(error.LogMessage))
            {
                _logger.Log(error.LogLevel, error.LogMessage);
            }

            switch (error.ErrorType)
            {
                case ChatErrorType.UserNotInServer:
                    throw new InvalidServerRequestException($"{error.User.UserName} is not part of the server.");

                case ChatErrorType.ServerNotFound:
                    throw new EntityNotFoundException("No server found.");

                case ChatErrorType.ChannelNotFound:
                    throw new EntityNotFoundException("No channel found.");

                case ChatErrorType.NoPermission:
                    throw new LackOfPermissionException($"{error.User.UserName} has no required permission.");

                case ChatErrorType.NoError:
                    return;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}