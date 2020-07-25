using RedPoint.Areas.Account.Models;
using RedPoint.Chat.Models;
using RedPoint.Data;
using RedPoint.Data.Repository;

namespace RedPoint.Chat.Services
{
    public interface IChatEntityRepositoryProxy
    {
        EntityRepository<Server, ApplicationDbContext> ServerRepository { get; }
        EntityRepository<Channel, ApplicationDbContext> ChannelRepository { get; }

        EntityRepository<Message, ApplicationDbContext> MessageRepository { get; }


        Channel TryFindingChannel(int channelId, ChatUser requestingUser);
        Message TryFindingMessage(int messageId, ChatUser requestingUser);
        Server TryFindingServer(int serverId, ChatUser requestingUser);
    }
}