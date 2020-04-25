using System.Collections.Generic;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Models.Dto;

namespace RedPoint.Areas.Chat.Services.DtoFactories
{
    public class ChannelDataDtoFactory : IChatDtoFactory<Channel, ChannelDataDto>
    {
        public ChannelDataDto CreateDto(Channel sourceObject)
        {
            throw new System.NotImplementedException();
        }

        public List<ChannelDataDto> CreateDtoList(List<Channel> sourceList)
        {
            throw new System.NotImplementedException();
        }
    }
}