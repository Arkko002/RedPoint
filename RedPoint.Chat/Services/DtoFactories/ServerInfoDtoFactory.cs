using System.Collections.Generic;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Chat.Dto;

namespace RedPoint.Chat.Services.DtoFactories
{
    /// <summary>
    /// Factory that creates server DTOs that don't contain internal server data.
    /// </summary>
    public class ServerInfoDtoFactory : IChatDtoFactory<Server, ServerInfoDto>
    {
        /// <inheritdoc/>
        public ServerInfoDto CreateDto(Server sourceObject)
        {
            var serverDto = new ServerInfoDto
            {
                Id = sourceObject.Id,
                Name = sourceObject.Name,
                Description = sourceObject.Description,
                Image = sourceObject.Image,
                IsVisible = sourceObject.IsVisible,
                HubGroupId = sourceObject.GroupId
            };

            return serverDto;
        }

        /// <inheritdoc/>
        public IEnumerable<ServerInfoDto> CreateDtoList(IEnumerable<Server> sourceList)
        {
            var dtoList = new List<ServerInfoDto>();
            foreach (var server in sourceList)
            {
                dtoList.Add(CreateDto(server));
            }

            return dtoList;
        }
    }
}