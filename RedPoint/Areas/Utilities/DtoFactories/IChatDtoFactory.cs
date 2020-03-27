using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Models.Dto;

namespace RedPoint.Areas.Utilities.DtoFactories
{
    public interface IChatDtoFactory<T>
    {
        IDto GetDto(T sourceObject);
    }
}