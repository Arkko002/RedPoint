using RedPoint.Areas.Chat.Models.Dto;

namespace RedPoint.Services
{
    public interface IChatHubService
    {
        void TryAddingServer(ServerDto server);
        void TryAddingMessage(int channelId, MessageDto message);
        void TryAddingChannel(int serverId, ChannelDto channel);
    }
}