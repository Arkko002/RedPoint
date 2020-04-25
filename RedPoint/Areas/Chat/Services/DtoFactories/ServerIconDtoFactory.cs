using System.Collections.Generic;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Models.Dto;

namespace RedPoint.Areas.Chat.Services.DtoFactories
{
    public class ServerIconDtoFactory : IChatDtoFactory<Server, ServerIconDto>
    {
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