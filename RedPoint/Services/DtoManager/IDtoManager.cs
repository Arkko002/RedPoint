using RedPoint.Utilities.DtoFactories;

using System.Collections.Generic;
using RedPoint.Areas.Chat.Models.Dto;
using RedPoint.Data;

namespace RedPoint.Services.DtoManager
{
    public interface IDtoManager
    {
        List<TDto> CreateDtoList<TEntity, TDto>(List<TEntity> sourceList, IChatDtoFactory<TEntity> dtoFactory)
        where TDto : IDto
        where TEntity : IEntity;
    }
}