using System.Collections.Generic;
using RedPoint.Chat.Models;
using RedPoint.Chat.Models.Dto;
using RedPoint.Chat.Services.DtoFactories;

namespace RedPoint.Chat.Services
{
    public interface IChatControllerService
    {
        List<ServerIconDto> GetUserServers(IChatDtoFactory<Server, ServerIconDto> dtoFactory);
        ServerDataDto GetServerData(int serverId, IChatDtoFactory<Server, ServerDataDto> dtoFactory);

        List<MessageDto> GetChannelMessages(int channelId, int serverId,
            IChatDtoFactory<Message, MessageDto> dtoFactory);
    }
}