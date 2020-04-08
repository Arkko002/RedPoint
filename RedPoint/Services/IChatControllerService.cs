using System.Collections.Generic;
using System.Security.Claims;
using RedPoint.Areas.Chat.Models.Dto;
using RedPoint.Utilities.DtoFactories;

namespace RedPoint.Services
{
    public interface IChatControllerService
    {
        List<ServerDto> GetUserServers(ServerDtoFactory dtoFactory, ClaimsPrincipal user);
        List<ChannelDto> GetServerChannels(ChannelDtoFactory dtoFactory);
        List<MessageDto> GetChannelMessages(MessageDtoFactory dtoFactory);
        List<UserChatDto> GetServerUserList(UserDtoFactory dtoFactory);

        void ValidateChannelRequest(int channelId, int serverId, ClaimsPrincipal user);
        void ValidateServerRequest(int serverId, ClaimsPrincipal user);
    }
}