using System.Collections.Generic;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Models.Dto;

namespace RedPoint.Areas.Chat.Services.DtoFactories
{
    public class ServerDataDtoFactory : IChatDtoFactory<Server, ServerDataDto>
    {
        public ServerDataDto CreateDto(Server sourceObject)
        {
            throw new System.NotImplementedException();
        }

        public List<ServerDataDto> CreateDtoList(List<Server> sourceList)
        {
            throw new System.NotImplementedException();
        }
    }
}