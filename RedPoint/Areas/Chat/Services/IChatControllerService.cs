using System.Collections.Generic;
using System.Security.Claims;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Models.Dto;
using RedPoint.Areas.Chat.Services.DtoFactories;

namespace RedPoint.Areas.Chat.Services
{
    public interface IChatControllerService
    {
        void AssignApplicationUser(ClaimsPrincipal user);
        List<ServerIconDto> GetUserServers(IChatDtoFactory<Server, ServerIconDto> dtoFactory);
        ServerDataDto GetServerData(int serverId, IChatDtoFactory<Server, ServerDataDto> dtoFactory);

        List<MessageDto> GetChannelMessages(int channelId, int serverId,
            IChatDtoFactory<Message, MessageDto> dtoFactory);
    }
}