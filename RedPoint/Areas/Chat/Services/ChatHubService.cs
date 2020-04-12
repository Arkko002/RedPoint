using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Models.Dto;
using RedPoint.Data;
using RedPoint.Data.Repository;
using RedPoint.Data.UnitOfWork;

namespace RedPoint.Areas.Chat.Services
{
    public class ChatHubService : IChatHubService
    {
        private readonly EntityRepository<Channel, ApplicationDbContext> _channelRepo;
        private readonly EntityRepository<Message, ApplicationDbContext> _messageRepo;
        private readonly EntityRepository<Server, ApplicationDbContext> _serverRepo;
        private readonly EntityUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        private ApplicationUser _requestingUser;

        public ChatHubService(EntityUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            EntityRepository<Server, ApplicationDbContext> serverRepo,
            EntityRepository<Channel, ApplicationDbContext> channelRepo,
            EntityRepository<Message, ApplicationDbContext> messageRepo)
        {
            //TODO Reduce the size of constructor

            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _serverRepo = serverRepo;
            _channelRepo = channelRepo;
            _messageRepo = messageRepo;
        }

        public void AssignApplicationUser(ClaimsPrincipal user)
        {
            _requestingUser = _userManager.GetUserAsync(user).Result;
        }

        public void TryAddingChannel(int serverId, ChannelDto channel)
        {
            
            
            //TODO validate
            var newChannel = new Channel(channel);

            _channelRepo.Add(newChannel);
            _unitOfWork.Submit();
        }

        public void TryAddingMessage(int channelId, MessageDto message)
        {
            //TODO validate
            var user = _userManager.FindByIdAsync(message.UserId).Result;
            var newMessage = new Message(message, user);

            _messageRepo.Add(newMessage);
            _unitOfWork.Submit();
        }

        public void TryAddingServer(ServerDto server)
        {
            var newServer = new Server(server);

            _serverRepo.Add(newServer);
            _unitOfWork.Submit();
        }
    }
}