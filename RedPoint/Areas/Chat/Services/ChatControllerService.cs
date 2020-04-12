using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Models.Dto;
using RedPoint.Areas.Identity.Models;
using RedPoint.Data;
using RedPoint.Data.UnitOfWork;
using RedPoint.Exceptions;
using RedPoint.Exceptions.Security;
using RedPoint.Services;
using RedPoint.Services.DtoManager;
using RedPoint.Services.Security;
using RedPoint.Utilities.DtoFactories;

namespace RedPoint.Areas.Chat.Services
{
    public class ChatControllerService : IChatControllerService
    {
        private readonly EntityRepository<Channel, ApplicationDbContext> _channelRepo;

        private readonly IDtoManager _dtoManager;
        private readonly EntityRepository<Message, ApplicationDbContext> _messageRepo;
        private readonly IChatRequestValidator _requestValidator;
        private readonly EntityRepository<Server, ApplicationDbContext> _serverRepo;
        private readonly EntityUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private Channel _requestedChannel;


        private Server _requestedServer;
        private ApplicationUser _requestingUser;

        //TODO Exception handling in Try... methods
        public ChatControllerService(EntityUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            EntityRepository<Server, ApplicationDbContext> serverRepo,
            EntityRepository<Channel, ApplicationDbContext> channelRepo,
            EntityRepository<Message, ApplicationDbContext> messageRepo,
            IDtoManager dtoManager,
            IChatRequestValidator requestValidator)
        {
            //TODO Reduce the size of constructor

            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _serverRepo = serverRepo;
            _channelRepo = channelRepo;
            _messageRepo = messageRepo;
            _dtoManager = dtoManager;
            _requestValidator = requestValidator;
        }

        public List<ServerDto> GetUserServers(ServerDtoFactory dtoFactory, ClaimsPrincipal user)
        {
            var appUser = _userManager.GetUserAsync(user).Result;

            return _dtoManager.CreateDtoList<Server, ServerDto>(appUser.Servers, dtoFactory);
        }

        public void ValidateServerRequest(int serverId, ClaimsPrincipal user)
        {
            _requestedServer = _serverRepo.Find(serverId);
            _requestingUser = _userManager.GetUserAsync(user).Result;

            try
            {
                _requestValidator.IsServerRequestValid(_requestedServer, _requestingUser);
            }
            catch (RequestInvalidException e)
            {
                throw new RequestInvalidException("Invalid server request from user.", e);
            }
        }

        public List<ChannelDto> GetServerChannels(ChannelDtoFactory dtoFactory)
        {
            return _dtoManager.CreateDtoList<Channel, ChannelDto>(_requestedServer.Channels, dtoFactory);
        }

        public void ValidateChannelRequest(int channelId, int serverId, ClaimsPrincipal user)
        {
            _requestedChannel = _channelRepo.Find(channelId);
            _requestedServer = _serverRepo.Find(serverId);
            _requestingUser = _userManager.GetUserAsync(user).Result;

            try
            {
                _requestValidator.IsChannelRequestValid(_requestedChannel, _requestedServer, _requestingUser);
            }
            catch (RequestInvalidException e)
            {
                throw new RequestInvalidException("Invalid server request from user.", e);
            }
        }

        public List<UserChatDto> GetServerUserList(UserDtoFactory dtoFactory)
        {
            return _dtoManager.CreateDtoList<ApplicationUser, UserChatDto>(_requestedServer.Users, dtoFactory);
        }

        public List<MessageDto> GetChannelMessages(MessageDtoFactory dtoFactory)
        {
            //TODO Lazy loading, return only 20-40 currently visible messages
            return _dtoManager.CreateDtoList<Message, MessageDto>(_requestedChannel.Messages, dtoFactory);
        }
    }
}