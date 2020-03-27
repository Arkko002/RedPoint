using System.Collections.Generic;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Models.Dto;

namespace RedPoint.Areas.Utilities.DtoFactories
{
    public class ServerDtoFactory : IChatDtoFactory<Server>
    {
        public IDto GetDto(Server sourceObject)
        {
            var serverDto = new ServerDto()
            {
                Name = sourceObject.Name,
                Description = sourceObject.Description,
                ImagePath = sourceObject.ImagePath,
                IsVisible = sourceObject.IsVisible
            };

            return serverDto;
        }
    }
}