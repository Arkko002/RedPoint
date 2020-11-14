using System.Collections.Generic;
using RedPoint.Chat.Models;
using RedPoint.Chat.Models.Dto;

namespace RedPoint.Chat.Services.DtoFactories
{
    /// <summary>
    /// Factory that creates server DTOs that don't contain internal server data.
    /// </summary>
    public class ServerIconDtoFactory : IChatDtoFactory<Server, ServerIconDto>
    {
        /// <inheritdoc/>
        public ServerIconDto CreateDto(Server sourceObject)
        {
            var serverDto = new ServerIconDto
            {
                Id = sourceObject.Id,
                Name = sourceObject.Name,
                Description = sourceObject.Description,
                Image = sourceObject.Image,
                IsVisible = sourceObject.IsVisible
            };

            return serverDto;
        }

        /// <inheritdoc/>
        public List<ServerIconDto> CreateDtoList(List<Server> sourceList)
        {
            var dtoList = new List<ServerIconDto>();
            foreach (var server in sourceList)
            {
                dtoList.Add(CreateDto(server));
            }

            return dtoList;
        }
    }
}