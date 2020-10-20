using RedPoint.Chat.Models;
using RedPoint.Data;
using RedPoint.Data.Repository;

namespace RedPoint.Chat.Services
{
    public interface IChatEntityRepositoryProxy
    {
        EntityRepository<Server, ChatDbContext> ServerRepository { get; }
        EntityRepository<Channel, ChatDbContext> ChannelRepository { get; }
        EntityRepository<Message, ChatDbContext> MessageRepository { get; }


        Channel TryFindingChannel(int channelId, ChatUser requestingUser);
        Message TryFindingMessage(int messageId, ChatUser requestingUser);
        Server TryFindingServer(int serverId, ChatUser requestingUser);
    }
}