using System;
using NLog;
using NLog;
using RedPoint.Chat.Services.Security;
using RedPoint.Chat.Exceptions;
using RedPoint.Chat.Exceptions.Security;

namespace RedPoint.Chat.Services
{
    /// <summary>
    /// Performs error handling for non-critical errors that occured in chat functionality.
    /// </summary>
    public class ChatErrorHandler : IChatErrorHandler
    {
        private readonly Logger _logger;

        public ChatErrorHandler(Logger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chatError"></param>
        /// <exception cref="InvalidServerRequestException"></exception>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <exception cref="LackOfPermissionException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void HandleChatError(ChatError chatError)
        {
            //TODO should non-critical chat errors be handled as exceptions? Rethink error handling.
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