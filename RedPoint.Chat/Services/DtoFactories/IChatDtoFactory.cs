using System.Collections.Generic;

namespace RedPoint.Chat.Services.DtoFactories
{
    public interface IChatDtoFactory<TEntity, TDto>
    {
        TDto CreateDto(TEntity sourceObject);
        List<TDto> CreateDtoList(List<TEntity> sourceList);
    }
}