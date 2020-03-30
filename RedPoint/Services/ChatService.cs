using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using RedPoint.Areas.Chat.Models.Dto;
using RedPoint.Areas.Identity.Models;
using RedPoint.Utilities.DtoFactories;
using RedPoint.Data.UnitOfWork;
using RedPoint.Areas.Chat.Models;
using RedPoint.Data;
using RedPoint.Areas.Chat.Services.DtoManager;
using RedPoint.Areas.Chat.Services.Security;
using RedPoint.Exceptions;

namespace RedPoint.Areas.Chat.Services
{
    public class ChatService
    {
        private EntityUnitOfWork _unitOfWork;
        private UserManager<ApplicationUser> _userManager;
        private EntityRepository<Server, ApplicationDbContext> _serverRepo;
        private EntityRepository<Channel, ApplicationDbContext> _channelRepo;
        private EntityRepository<Message, ApplicationDbContext> _messageRepo;

        private IDtoManager _dtoManager;
        private IRequestValidator _requestValidator;


        private Server _requestedServer;
        private Channel _requestedChannel;
        private ApplicationUser _requestingUser;

        public ChatService(EntityUnitOfWork unitOfWork,
         UserManager<ApplicationUser> userManager,
         EntityRepository<Server, ApplicationDbContext> serverRepo,
         EntityRepository<Channel, ApplicationDbContext> channelRepo,
         EntityRepository<Message, ApplicationDbContext> messageRepo,
         IDtoManager dtoManager,
         IRequestValidator requestValidator)
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

            if (!_requestValidator.IsServerRequestValid(_requestedServer, _requestingUser))
            {
                throw new RequestInvalidException("Invalid server request from user.");
            }
        }

        public List<ChannelDto> GetServerChannels(ChannelDtoFactory dtoFactory)
        {
            return _dtoManager.CreateDtoList<Channel, ChannelDto>(_requestedServer.Channels, dtoFactory);
        }

        public List<UserChatDto> GetServerUserList(UserDtoFactory dtoFactory)
        {
            return _dtoManager.CreateDtoList<ApplicationUser, UserChatDto>(_requestedServer.Users, dtoFactory);
        }

        public void ValidateChannelRequest(int channelId, int serverId, ClaimsPrincipal user)
        {
            _requestedChannel = _channelRepo.Find(channelId);
            _requestingUser = _userManager.GetUserAsync(user).Result;

            if (!_requestValidator.IsChannelRequestValid(_requestedChannel, _requestingUser))
            {
                throw new RequestInvalidException("Invalid channel request from user.");
            }
        }

        public List<MessageDto> GetChannelMessages(MessageDtoFactory dtoFactory)
        {
            //TODO Lazy loading, return only 20-40 currently visible messages
            return _dtoManager.CreateDtoList<Message, MessageDto>(_requestedChannel.Messages, dtoFactory);
        }
    }
}