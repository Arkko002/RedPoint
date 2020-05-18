using System.Collections.Generic;
using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Models.Dto;

namespace RedPoint.Areas.Chat.Services.DtoFactories
{
    public class ServerDataDtoFactory : IChatDtoFactory<Server, ServerDataDto>
    {
        private readonly IChatDtoFactory<Channel, ChannelIconDto> _channelFactory;
        private readonly IChatDtoFactory<ApplicationUser, UserChatDto> _userFactory;

        public ServerDataDtoFactory(IChatDtoFactory<Channel, ChannelIconDto> channelFactory,
            IChatDtoFactory<ApplicationUser, UserChatDto> userFactory)
        {
            _channelFactory = channelFactory;
            _userFactory = userFactory;
        }

        public ServerDataDto CreateDto(Server sourceObject)
        {
            var dto = new ServerDataDto();
            dto.Id = sourceObject.Id;
            dto.ChannelList = _channelFactory.CreateDtoList(sourceObject.Channels);
            dto.UserList = _userFactory.CreateDtoList(sourceObject.Users);

            return dto;
        }

        public List<ServerDataDto> CreateDtoList(List<Server> sourceList)
        {
            var dtoList = new List<ServerDataDto>();

            foreach (var server in sourceList)
            {
                dtoList.Add(CreateDto(server));
            }

            return dtoList;
        }
    }
}