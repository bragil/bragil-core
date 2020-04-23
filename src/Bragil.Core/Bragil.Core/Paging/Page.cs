using System;
using System.Collections.Generic;

namespace Bragil.Core.Paging
{
    /// <summary>
    /// Classe para paginação de resultados.
    /// </summary>
    /// <typeparam name="T">Tipo da entidade</typeparam>
    public class Page<T>
    {
        /// <summary>
        /// Quantidade total de registros.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Número da página atual
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Quantidade de itens por página
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Quantidade de páginas
        /// </summary>
        public int PageCount
        {
            get
            {
                var c = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(Count) / Convert.ToDecimal(PageSize)));
                return (int)c;
            }
        }

        /// <summary>
        /// Lista de itens da página
        /// </summary>
        public List<T> Items { get; set; }

        /// <summary>
        /// Retorna true se a quantidade total de registros for igual a zero.
        /// </summary>
        public bool IsEmpty => Count == 0;


        public Page(int pageNumber, int pageSize, int count)
        {
            Count = count;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public Page(int pageNumber, int pageSize, int count, List<T> itens)
        {
            Count = count;
            PageNumber = pageNumber;
            PageSize = pageSize;
            Items = itens;
        }


    }
}
