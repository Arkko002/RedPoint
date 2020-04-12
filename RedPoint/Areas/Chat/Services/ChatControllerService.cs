using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Models.Dto;
using RedPoint.Areas.Chat.Services.Security;
using RedPoint.Data;
using RedPoint.Data.Repository;
using RedPoint.Exceptions;
using RedPoint.Exceptions.Security;
using RedPoint.Services.DtoManager;
using RedPoint.Utilities.DtoFactories;

namespace RedPoint.Areas.Chat.Services
{
    public class ChatControllerService : IChatControllerService
    {
        private readonly EntityRepository<Channel, ApplicationDbContext> _channelRepo;
        private readonly EntityRepository<Message, ApplicationDbContext> _messageRepo;
        private readonly EntityRepository<Server, ApplicationDbContext> _serverRepo;
        
        private readonly IDtoManager _dtoManager;
        private readonly IChatRequestValidator _requestValidator;

        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationUser _user;

        private readonly ILogger<ChatControllerService> _logger;
        
        public ChatControllerService(UserManager<ApplicationUser> userManager,
            EntityRepository<Server, ApplicationDbContext> serverRepo,
            EntityRepository<Channel, ApplicationDbContext> channelRepo,
            EntityRepository<Message, ApplicationDbContext> messageRepo,
            IDtoManager dtoManager,
            IChatRequestValidator requestValidator,
            ILogger<ChatControllerService> logger)
        {
            //TODO Reduce the size of constructor
            _userManager = userManager;
            _serverRepo = serverRepo;
            _channelRepo = channelRepo;
            _messageRepo = messageRepo;
            _dtoManager = dtoManager;
            _requestValidator = requestValidator;
            _logger = logger;
        }

        public void AssignApplicationUser(ClaimsPrincipal user)
        {
            _user = _userManager.GetUserAsync(user).Result;
        }
        
        public List<ServerDto> GetUserServers(IChatDtoFactory<Server> dtoFactory)
        {
            return _dtoManager.CreateDtoList<Server, ServerDto>(_user.Servers, dtoFactory);
        }
        
        public List<ChannelDto> GetServerChannels(int serverId, IChatDtoFactory<Channel> dtoFactory)
        {
            var server = TryFindingServer(serverId);
            
            List<Channel> userPermittedChannels = new List<Channel>();
            foreach (var channel in server.Channels)
            {
                //TODO flow control
                if (ValidateChannelRequest(channel, server, PermissionType.CanView) == ChatErrorType.NoError)
                {
                    userPermittedChannels.Add(channel);
                }
            }
            
            return _dtoManager.CreateDtoList<Channel, ChannelDto>(userPermittedChannels, dtoFactory);
        }
        
        public List<UserChatDto> GetServerUserList(int serverId, IChatDtoFactory<ApplicationUser> dtoFactory)
        {
            var server = TryFindingServer(serverId);
            ValidateServerRequest(server, PermissionType.CanView);

            return _dtoManager.CreateDtoList<ApplicationUser, UserChatDto>(server.Users, dtoFactory);
        }

        
        public List<MessageDto> GetChannelMessages(int channelId, int serverId, IChatDtoFactory<Message> dtoFactory)
        {
            var channel = TryFindingChannel(channelId);
            var server = TryFindingServer(serverId);
            
            ValidateChannelRequest(channel, server, PermissionType.CanView);
            
            //TODO Lazy loading, return only 20-40 currently visible messages
            return _dtoManager.CreateDtoList<Message, MessageDto>(channel.Messages, dtoFactory);
        }
        
        private Server TryFindingServer(int serverId)
        {
            Server server = _serverRepo.Find(serverId);

            if (server == null)
            {
                _logger.LogError($"Non-existing server (ID: {serverId}) requested by {_user.Id}");
                HandleChatError(ChatErrorType.ServerNotFound);
            }

            return server;
        }

        private Channel TryFindingChannel(int channelId)
        {
            Channel channel = _channelRepo.Find(channelId);
            
            if (channel == null)
            {
                _logger.LogError($"Non-existing channel (ID: {channelId}) requested by {_user.Id}");
                HandleChatError(ChatErrorType.ChannelNotFound);
            }

            return channel;
        }
        
        private ChatErrorType ValidateServerRequest(Server server, PermissionType permissionType)
        {
            var errorType = _requestValidator.IsServerRequestValid(server, _user, permissionType);
            HandleChatError(errorType);
            
            return errorType;
        }
        
        private ChatErrorType ValidateChannelRequest(Channel channel, Server server, PermissionType permissionType)
        {
            var errorType = _requestValidator.IsChannelRequestValid(channel, server, _user, permissionType);
            HandleChatError(errorType);
            
            return errorType;
        }

        private void HandleChatError(ChatErrorType errorType)
        {
            switch (errorType)
            {
                case ChatErrorType.UserNotInServer:
                    throw new InvalidServerRequestException($"{_user.UserName} is not part of the server.");
                
                case ChatErrorType.ServerNotFound:
                    throw new EntityNotFoundException("No server found.");
                
                case ChatErrorType.ChannelNotFound:
                    throw new EntityNotFoundException("No channel found.");

                case ChatErrorType.NoPermission:
                    throw new LackOfPermissionException($"{_user.UserName} has no required permission.");

                case ChatErrorType.NoError:
                    return;
            }
        }
    }
}