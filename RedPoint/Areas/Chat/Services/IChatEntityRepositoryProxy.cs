using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Chat.Models;
using RedPoint.Data;
using RedPoint.Data.Repository;

namespace RedPoint.Areas.Chat.Services
{
    public interface IChatEntityRepositoryProxy
    {
        EntityRepository<Server, ApplicationDbContext> ServerRepository { get; }
        EntityRepository<Channel, ApplicationDbContext> ChannelRepository { get; }

        EntityRepository<Message, ApplicationDbContext> MessageRepository { get; }


        Channel TryFindingChannel(int channelId, ApplicationUser requestingUser);
        Message TryFindingMessage(int messageId, ApplicationUser requestingUser);
        Server TryFindingServer(int serverId, ApplicationUser requestingUser);
    }
}