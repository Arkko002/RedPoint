using RedPoint.Areas.Chat.Models.Dto;

namespace RedPoint.Utilities.DtoFactories
{
    public interface IChatDtoFactory<T>
    {
        IDto GetDto(T sourceObject);
    }
}