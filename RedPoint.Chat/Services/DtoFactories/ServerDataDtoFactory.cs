using System.Collections.Generic;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Chat.Dto;

namespace RedPoint.Chat.Services.DtoFactories
{
    /// <summary>
    /// Factory that creates server DTOs with internal server data.
    /// The amount of data included is based on user's server permissions.
    /// </summary>
    public class ServerDataDtoFactory : IChatDtoFactory<Server, ServerDataDto>
    {
        private readonly IChatDtoFactory<Channel, ChannelInfoDto> _channelFactory;
        private readonly IChatDtoFactory<ChatUser, UserInfoDto> _userFactory;
        
        public ServerDataDtoFactory(IChatDtoFactory<Channel, ChannelInfoDto> channelFactory,
            IChatDtoFactory<ChatUser, UserInfoDto> userFactory)
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
        public IEnumerable<ServerDataDto> CreateDtoList(IEnumerable<Server> sourceList)
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