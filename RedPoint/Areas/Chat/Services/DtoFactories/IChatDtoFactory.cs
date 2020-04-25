using System.Collections.Generic;

namespace RedPoint.Areas.Chat.Services.DtoFactories
{
    public interface IChatDtoFactory<TEntity, TDto>
    {
        TDto CreateDto(TEntity sourceObject);
        List<TDto> CreateDtoList(List<TEntity> sourceList);
    }
}