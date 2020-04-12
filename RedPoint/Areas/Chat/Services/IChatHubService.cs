using System.Security.Claims;
using RedPoint.Areas.Chat.Models.Dto;

namespace RedPoint.Areas.Chat.Services
{
    public interface IChatHubService
    {
        void AssignApplicationUser(ClaimsPrincipal user);
        void TryAddingServer(ServerDto server);
        void TryAddingMessage(int channelId, MessageDto message);
        void TryAddingChannel(int serverId, ChannelDto channel);
    }
}