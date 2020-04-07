using System.Collections.Generic;
using RedPoint.Areas.Chat.Models.Dto;
using RedPoint.Data;
using RedPoint.Utilities.DtoFactories;

namespace RedPoint.Services.DtoManager
{
    public class DtoManager : IDtoManager
    {
        public List<TDto> CreateDtoList<TEntity, TDto>(List<TEntity> sourceList, IChatDtoFactory<TEntity> dtoFactory)
            where TEntity : IEntity
            where TDto : IDto
        {
            List<TDto> dtoList = new List<TDto>();
            foreach (TEntity item in sourceList)
            {
                dtoList.Add((TDto)dtoFactory.GetDto(item));
            }

            return dtoList;
        }
    }
}