using System.Collections.Generic;
using RedPoint.Chat.Models;
using RedPoint.Chat.Models.Dto;

namespace RedPoint.Chat.Services.DtoFactories
{
    /// <summary>
    /// Factory that creates server DTOs with internal server data.
    /// The amount of data included is based on user's server permissions.
    /// </summary>
    public class ServerDataDtoFactory : IChatDtoFactory<Server, ServerDataDto>
    {
        private readonly IChatDtoFactory<Channel, ChannelIconDto> _channelFactory;
        private readonly IChatDtoFactory<ChatUser, ChatUserDto> _userFactory;
        
        public ServerDataDtoFactory(IChatDtoFactory<Channel, ChannelIconDto> channelFactory,
            IChatDtoFactory<ChatUser, ChatUserDto> userFactory)
        {
            _channelFactory = channelFactory;
            _userFactory = userFactory;
        }

        /// <inheritdoc/>
        public ServerDataDto CreateDto(Server sourceObject)
        {
            var dto = new ServerDataDto
            {
                Id = sourceObject.Id,
                ChannelList = _channelFactory.CreateDtoList(sourceObject.Channels),
                UserList = _userFactory.CreateDtoList(sourceObject.Users)
            };

            return dto;
        }

        /// <inheritdoc/>
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