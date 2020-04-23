using System.Collections.Generic;
using System.Linq;

namespace Bragil.Core.Paging
{
    public static class PageExtensions
    {
        /// <summary>
        /// Adiciona paginação de resultados à consulta.
        /// </summary>
        /// <typeparam name="T">Tipo do dado</typeparam>
        /// <param name="queryable"><![CDATA[Objeto IQueryable<T>]]></param>
        /// <param name="pageSize">Número de ítens por página</param>
        /// <param name="pageNumber">Número da página</param>
        /// <returns><![CDATA[IQueryable<T>]]></returns>
        public static IQueryable<T> Paged<T>(this IQueryable<T> queryable, int pageSize, int pageNumber)
            => queryable.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        /// <summary>
        /// Adiciona paginação de resultados à consulta.
        /// </summary>
        /// <typeparam name="T">Tipo do dado</typeparam>
        /// <param name="queryable"><![CDATA[Objeto IOrderedQueryable<T>]]></param>
        /// <param name="pageSize">Número de ítens por página</param>
        /// <param name="pageNumber">Número da página</param>
        /// <returns><![CDATA[IQueryable<T>]]></returns>
        public static IOrderedQueryable<T> Paged<T>(this IOrderedQueryable<T> queryable, int pageSize, int pageNumber)
            => (IOrderedQueryable<T>)queryable.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        /// <summary>
        /// Adiciona paginação de resultados à consulta.
        /// </summary>
        /// <typeparam name="T">Tipo do dado</typeparam>
        /// <param name="queryable"><![CDATA[Objeto IEnumerable<T>]]></param>
        /// <param name="pageSize">Número de ítens por página</param>
        /// <param name="pageNumber">Número da página</param>
        /// <returns><![CDATA[IEnumerable<T>]]></returns>
        public static IEnumerable<T> Paged<T>(this IEnumerable<T> queryable, int pageSize, int pageNumber)
            => queryable.Skip((pageNumber - 1) * pageSize).Take(pageSize);

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

        /// <summary>
        /// Retorna o objeto de paginação.
        /// </summary>
        /// <typeparam name="T">Tipo do dado</typeparam>
        /// <param name="queryable"><![CDATA[Objeto IOrderedQueryable<T>]]></param>
        /// <param name="pageSize">Número de ítens por página</param>
        /// <param name="pageNumber">Número da página</param>
        /// <returns><![CDATA[Page<T>]]></returns>
        public static Page<T> ToPagedList<T>(this IOrderedQueryable<T> queryable, int pageSize, int pageNumber)
        {
            var list = queryable.Paged(pageSize, pageNumber).ToList();
            int count = queryable.Count();

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
        public static Page<T> ToPagedList<T>(this IEnumerable<T> enumerable, int pageSize, int pageNumber)
        {
            var list = enumerable.Paged(pageSize, pageNumber).ToList();
            int count = enumerable.Count();

            if (list != null)
                return new Page<T>(pageNumber, pageSize, count) { Items = list };

            return new Page<T>(pageNumber, pageSize, 0);
        }

    }
}
