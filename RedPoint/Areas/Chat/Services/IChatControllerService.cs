using System.Collections.Generic;
using System.Security.Claims;
using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Models.Dto;
using RedPoint.Utilities.DtoFactories;

namespace RedPoint.Areas.Chat.Services
{
    public interface IChatControllerService
    {
        void AssignApplicationUser(ClaimsPrincipal user);
        List<ServerDto> GetUserServers(IChatDtoFactory<Server> dtoFactory);
        List<ChannelDto> GetServerChannels(int serverId, IChatDtoFactory<Channel> dtoFactory);
        List<MessageDto> GetChannelMessages(int channelId, int serverId, IChatDtoFactory<Message> dtoFactory);
        List<UserChatDto> GetServerUserList(int serverId, IChatDtoFactory<ApplicationUser> dtoFactory);
    }
}