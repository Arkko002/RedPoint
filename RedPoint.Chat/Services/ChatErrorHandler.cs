using System;
using NLog;
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
        
        /// <inheritdoc />
        /// <param name="error"></param>
        /// <exception cref="InvalidServerRequestException"></exception>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <exception cref="LackOfPermissionException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void HandleChatError(ChatError error)
        {
            if (!string.IsNullOrEmpty(error.LogMessage))
            {
                _logger.Log(error.LogLevel, error.LogMessage);
            }

            switch (error.ErrorType)
            {
                case ChatErrorType.NoError:
                    return;
            
                case ChatErrorType.UserNotInServer:
                    throw new InvalidServerRequestException($"{error.User.UserName} is not part of the server.");
                    
                case ChatErrorType.MessageNotFound:
                    throw new EntityNotFoundException("Requested message couldn't be found.");

                case ChatErrorType.ServerNotFound:
                    throw new EntityNotFoundException("Requested server couldn't be found.");

                case ChatErrorType.ChannelNotFound:
                    throw new EntityNotFoundException("Requested channel couldn't be found.");

                case ChatErrorType.NoPermission:
                    throw new LackOfPermissionException($"{error.User.UserName} has no required permission.");

                default:
                    throw new ChatRequestException($"Unknown error occured. ErrorType: {error.ErrorType}     User: {error.User}");
            }
        }
    }
}