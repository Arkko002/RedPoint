using System.Collections.Generic;

namespace RedPoint.Chat.Services.DtoFactories
{
    public interface IChatDtoFactory<TEntity, TDto>
    {
        /// <summary>
        /// Creates a single DTO object from the source object.
        /// </summary>
        /// <param name="sourceObject">Non-DTO object with source information</param>
        /// <returns>DTO object</returns>
        TDto CreateDto(TEntity sourceObject);

        /// <summary>
        /// Creates a list of DTOs from the provided list of source objects.
        /// </summary>
        /// <param name="sourceList">List of non-DTO objects with source information</param>
        /// <returns>List of DTOs</returns>
        IEnumerable<TDto> CreateDtoList(IEnumerable<TEntity> sourceList);
    }
}