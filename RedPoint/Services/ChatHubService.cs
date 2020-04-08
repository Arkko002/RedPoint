using Microsoft.AspNetCore.Identity;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Models.Dto;
using RedPoint.Areas.Identity.Models;
using RedPoint.Data;
using RedPoint.Data.UnitOfWork;

namespace RedPoint.Services
{
    public class ChatHubService : IChatHubService
    {
        private readonly EntityUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EntityRepository<Server, ApplicationDbContext> _serverRepo;
        private readonly EntityRepository<Channel, ApplicationDbContext> _channelRepo;
        private readonly EntityRepository<Message, ApplicationDbContext> _messageRepo;

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

        public void TryAddingChannel(int serverId, ChannelDto channel)
        {
            //TODO validate
            Channel newChannel = new Channel(channel);

            _channelRepo.Add(newChannel);
            _unitOfWork.Submit();
        }

        public void TryAddingMessage(int channelId, MessageDto message)
        {
            //TODO validate
            ApplicationUser user = _userManager.FindByIdAsync(message.UserId).Result;
            Message newMessage = new Message(message, user);

            _messageRepo.Add(newMessage);
            _unitOfWork.Submit();
        }

        public void TryAddingServer(ServerDto server)
        {
            Server newServer = new Server(server);

            _serverRepo.Add(newServer);
            _unitOfWork.Submit();
        }
    }
}