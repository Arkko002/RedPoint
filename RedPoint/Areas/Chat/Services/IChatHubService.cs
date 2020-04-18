using System.Security.Claims;
using RedPoint.Areas.Chat.Models.Dto;

namespace RedPoint.Areas.Chat.Services
{
    public interface IChatHubService
    {
        void AssignApplicationUser(ClaimsPrincipal user);
        void AddServer(ServerDto server);
        void AddMessage(int channelId, int serverId, MessageDto message);
        void AddChannel(int serverId, ChannelDto channel);

        void DeleteChannel(int channelId, int serverId);
        void DeleteServer(int serverId);
        void DeleteMessage(int messageId, int channelId, int serverId);
    }
}