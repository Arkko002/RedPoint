using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using RedPoint.Areas.Chat.Models.Dto;
using RedPoint.Areas.Identity.Models;
using RedPoint.Areas.Utilities.DtoFactories;
using RedPoint.Data.UnitOfWork;
using RedPoint.Areas.Chat.Models;
using RedPoint.Data;

namespace RedPoint.Areas.Chat.Services
{
    public class ChatService
    {
        private EntityUnitOfWork _unitOfWork;
        private UserManager<ApplicationUser> _userManager;
        private EntityRepository<Server, ApplicationDbContext> _serverRepo;
        private EntityRepository<Channel, ApplicationDbContext> _channelRepo;
        private EntityRepository<Message, ApplicationDbContext> _messageRepo;

        public ChatService(EntityUnitOfWork unitOfWork,
         UserManager<ApplicationUser> userManager,
         EntityRepository<Server, ApplicationDbContext> serverRepo,
         EntityRepository<Channel, ApplicationDbContext> channelRepo,
         EntityRepository<Message, ApplicationDbContext> messageRepo
         )
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _serverRepo = serverRepo;
            _channelRepo = channelRepo;
            _messageRepo = messageRepo;
        }

        public List<ServerDto> GetUserServers(ServerDtoFactory dtoFactory, ClaimsPrincipal user)
        {
            var appUser = _userManager.GetUserAsync(user).Result;

            return CreateDtoList<Server, ServerDto>(appUser.Servers, dtoFactory);
        }

        public List<ChannelDto> GetServerChannels(int serverId, ChannelDtoFactory dtoFactory)
        {
            var server = _serverRepo.Find(serverId);

            return CreateDtoList<Channel, ChannelDto>(server.Channels, dtoFactory);
        }

        public List<UserChatDto> GetServerUserList(int serverId, UserDtoFactory dtoFactory)
        {
            var server = _serverRepo.Find(serverId);

            return CreateDtoList<ApplicationUser, UserChatDto>(server.Users, dtoFactory);
        }

        private List<TDto> CreateDtoList<TEntity, TDto>(List<TEntity> sourceList, IChatDtoFactory<TEntity> dtoFactory) where TDto : IDto
        {
            List<TDto> dtoList = new List<TDto>();
            foreach (TEntity item in sourceList)
            {
                dtoList.Add((TDto)dtoFactory.GetDto(item));
            }

            return dtoList;
        }
    }
}