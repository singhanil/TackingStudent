using AutoMapper;
using StudentTracking.Application.Models;
using System.Collections.Generic;
using System.Linq;

namespace StudentTracking.Application.Main.Extensions
{
    public static class TypeAdapterExtensions
    {
        /// <summary>
        /// Maps a type using a model.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="entity">The source entity to map.</param>
        /// <returns>The mapped type.</returns>
        public static TModel MapAs<TEntity, TModel>(this TEntity entity)
            where TModel : class,new()
        {
            return entity != null
                       ? Mapper.Map<TModel>(entity)
                       : null;
        }

        /// <summary>
        /// Maps a type using an entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="model">The model.</param>
        /// <returns>The mapped type.</returns>
        public static TEntity MapAs<TEntity>(this ModelBase model)
            where TEntity : class,new()
        {
            return model != null
                       ? Mapper.Map<TEntity>(model)
                       : null;
        }

        /// <summary>
        /// Maps a enumerable collection of items.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="entities">The collection of entity items.</param>
        /// <returns>The mapped collection.</returns>
        public static ICollection<TModel> MapAsCollection<TEntity, TModel>(this IEnumerable<TEntity> entities)
            where TModel : class,new()
        {
            return entities.Any()
                ? Mapper.Map<List<TModel>>(entities)
                : null;
        }
    }
}
