using Bragil.Core.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bragil.Core.Interfaces
{
    /// <summary>
    /// Interface para repositório assíncrono.
    /// </summary>
    /// <typeparam name="T">Tipo da entidade</typeparam>
    /// <typeparam name="TId">Tipo do ID da entidade</typeparam>
    public interface IRepositoryAsync<T, TId> where T: class, IEntity<TId>
    {
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(TId id);
        Task Delete(T entity);
        Task<bool> Exists(Func<T, bool> filter);
        Task<T> GetById(TId id);
        Task<List<T>> GetAll();
        Task<Page<T>> GetAll(int pageSize, int pageNumber);
        Task<T> FindOne(Func<T, bool> predicate);
        Task<List<T>> FindAll(Func<T, bool> predicate);
        Task<Page<T>> FindAll(Func<T, bool> predicate, int pageSize, int pageNumber);
        Task<Page<T>> FindAll<TKey>(Func<T, bool> predicate, Func<T, TKey> fnOrderBy, Order order, int pageSize, int pageNumber);
        IQueryable<T> AsQueryable();
    }

    public enum Order { ASC, DESC }
}
