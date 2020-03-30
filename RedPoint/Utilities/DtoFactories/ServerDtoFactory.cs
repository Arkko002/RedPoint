using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Models.Dto;

namespace RedPoint.Utilities.DtoFactories
{
    public class ServerDtoFactory : IChatDtoFactory<Server>
    {
        public IDto GetDto(Server sourceObject)
        {
            var serverDto = new ServerDto()
            {
                Id = sourceObject.Id,
                Name = sourceObject.Name,
                Description = sourceObject.Description,
                ImagePath = sourceObject.ImagePath,
                IsVisible = sourceObject.IsVisible
            };

            return serverDto;
        }
    }
}