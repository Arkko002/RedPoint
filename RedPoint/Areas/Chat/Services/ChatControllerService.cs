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
        private ApplicationUser _requestingUser;

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
            _requestingUser = _userManager.GetUserAsync(user).Result;
        }
        
        public List<ServerDto> GetUserServers(IChatDtoFactory<Server> dtoFactory)
        {
            return _dtoManager.CreateDtoList<Server, ServerDto>(_requestingUser.Servers, dtoFactory);
        }

        
        public List<ChannelDto> GetServerChannels(int serverId, IChatDtoFactory<Channel> dtoFactory)
        {
            var requestedServer = _serverRepo.Find(serverId);

            return _dtoManager.CreateDtoList<Channel, ChannelDto>(requestedServer.Channels, dtoFactory);
        }

        
        public List<UserChatDto> GetServerUserList(int serverId, IChatDtoFactory<ApplicationUser> dtoFactory)
        {
            var requestedServer = TryFindingServerAndValidateRequest(serverId);

            return _dtoManager.CreateDtoList<ApplicationUser, UserChatDto>(requestedServer.Users, dtoFactory);
        }

        private Server TryFindingServerAndValidateRequest(int serverId, PermissionType permissionType)
        {
            Server requestedServer = _serverRepo.Find(serverId);

            if (requestedServer == null)
            {
                _logger.LogError("Non-existing server (ID: {0}) requested by {1}", serverId, _requestingUser.Id);
                throw new EntityNotFoundException("No server found.");
            }

            ValidateServerRequest(requestedServer);

            return requestedServer;
        }
        
        public List<MessageDto> GetChannelMessages(int channelId, int serverId, IChatDtoFactory<Message> dtoFactory)
        {
            var requestedChannel = _channelRepo.Find(channelId);
            var requestedServer = _serverRepo.Find(serverId);
            
            ValidateChannelRequest(requestedChannel, requestedServer);
            
            //TODO Lazy loading, return only 20-40 currently visible messages
            return _dtoManager.CreateDtoList<Message, MessageDto>(requestedChannel.Messages, dtoFactory);
        }
        
        private void ValidateServerRequest(Server requestedServer, PermissionType permissionType)
        {
            try
            {
                _requestValidator.IsServerRequestValid(requestedServer, _requestingUser, permissionType);
            }
            catch (Exception e)
            {
                _logger.LogError("Invalid server request: ({0}) by {1}", e.Message, _requestingUser.Id);
                throw new InvalidRequestException("Invalid server request from user.", e);
            }
        }
        
        private void ValidateChannelRequest(Channel requestedChannel, Server requestedServer)
        {
            try
            {
                _requestValidator.IsChannelRequestValid(requestedChannel, requestedServer, _requestingUser);
            }
            catch (Exception e)
            {
                _logger.LogError("Invalid channel request: ({0}) by {1}", e.Message, _requestingUser.Id);
                throw new InvalidRequestException("Invalid server request from user.", e);
            }
        }
    }
}