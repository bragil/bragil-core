using Bragil.Core.Paging;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bragil.Core.Repositories.EntityCore
{
    public static class EntityCoreExtensions
    {
        /// <summary>
        /// Retorna o objeto de paginação.
        /// </summary>
        /// <typeparam name="T">Tipo do dado</typeparam>
        /// <param name="queryable"><![CDATA[Objeto IQueryable<T>]]></param>
        /// <param name="pageSize">Número de ítens por página</param>
        /// <param name="pageNumber">Número da página</param>
        /// <returns><![CDATA[Page<T>]]></returns>
        public static async Task<Page<T>> ToPagedListAsync<T>(this IQueryable<T> queryable, int pageSize, int pageNumber, CancellationToken cancelToken = default)
        {
            var list = await queryable.Paged(pageSize, pageNumber).ToListAsync(cancelToken);
            int count = await queryable.CountAsync(cancelToken);

            if (list != null)
                return new Page<T>(pageNumber, pageSize, count) { Items = list };

            return new Page<T>(pageNumber, pageSize, 0);
        }

        /// <summary>
        /// Retorna o objeto de paginação.
        /// </summary>
        /// <typeparam name="T">Tipo do dado</typeparam>
        /// <param name="enumerable"><![CDATA[Objeto IEnumerable<T>]]></param>
        /// <param name="pageSize">Número de ítens por página</param>
        /// <param name="pageNumber">Número da página</param>
        /// <returns><![CDATA[Page<T>]]></returns>
        public static async Task<Page<T>> ToPagedListAsync<T>(this IEnumerable<T> enumerable, int pageSize, int pageNumber, CancellationToken cancelToken = default)
        {
            var list = await enumerable.Paged(pageSize, pageNumber).AsQueryable().ToListAsync(cancelToken);
            int count = await enumerable.AsQueryable().CountAsync(cancelToken);

            if (list != null)
                return new Page<T>(pageNumber, pageSize, count) { Items = list };

            return new Page<T>(pageNumber, pageSize, 0);
        }

        /// <summary>
        /// Retorna o objeto de paginação.
        /// </summary>
        /// <typeparam name="T">Tipo do dado</typeparam>
        /// <param name="queryable"><![CDATA[Objeto IQueryable<T>]]></param>
        /// <param name="pageSize">Número de ítens por página</param>
        /// <param name="pageNumber">Número da página</param>
        /// <returns><![CDATA[Page<T>]]></returns>
        public static Page<T> ToPagedList<T>(this IQueryable<T> queryable, int pageSize, int pageNumber)
        {
            var list = queryable.Paged(pageSize, pageNumber).ToList();
            int count = queryable.Count();

            if (list != null)
                return new Page<T>(pageNumber, pageSize, count) { Items = list };

            return new Page<T>(pageNumber, pageSize, 0);
        }
    }
}
