using Bragil.Core.Paging;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Bragil.Core.Repositories.MongoDb
{
    public static class MongoExtensions
    {
        /// <summary>
        /// Adiciona paginação de resultados à consulta.
        /// </summary>
        /// <typeparam name="T">Tipo do dado</typeparam>
        /// <param name="cursor"><![CDATA[Objeto IAsyncCursor<T>]]></param>
        /// <param name="pageSize">Número de ítens por página</param>
        /// <param name="pageNumber">Número da página</param>
        /// <returns><![CDATA[IEnumerable<T>]]></returns>
        public static IEnumerable<T> Paged<T>(this IAsyncCursor<T> cursor, int pageSize, int pageNumber)
            => cursor.Current.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        /// <summary>
        /// Retorna o objeto de paginação.
        /// </summary>
        /// <typeparam name="T">Tipo do dado</typeparam>
        /// <param name="cursor"><![CDATA[Objeto IAsyncCursor<T>]]></param>
        /// <param name="pageSize">Número de ítens por página</param>
        /// <param name="pageNumber">Número da página</param>
        /// <returns><![CDATA[Page<T>]]></returns>
        public static Page<T> ToPagedList<T>(this IAsyncCursor<T> cursor, int pageSize, int pageNumber)
        {
            var list = cursor.Paged(pageSize, pageNumber).ToList();
            int count = cursor.Current.Count();

            if (list != null)
                return new Page<T>(pageNumber, pageSize, count) { Items = list };

            return new Page<T>(pageNumber, pageSize, 0);
        }
    }
}
