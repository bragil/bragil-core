using Bragil.Core.Paging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bragil.Core.Interfaces
{
    /// <summary>
    /// Interface para repositório.
    /// </summary>
    /// <typeparam name="T">Tipo da entidade</typeparam>
    /// <typeparam name="TId">Tipo do ID da entidade</typeparam>
    public interface IRepository<T, TId> where T: IEntity<TId>
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(TId id);
        void Delete(T entity);
        bool Exists(Func<T, bool> filter);
        T GetById(TId id);
        List<T> GetAll();
        Page<T> GetAll(int pageSize, int pageNumber);
        T FindOne(Func<T, bool> predicate);
        List<T> FindAll(Func<T, bool> predicate);
        Page<T> FindAll(Func<T, bool> predicate, int pageSize, int pageNumber);
        Page<T> FindAll<TKey>(Func<T, bool> predicate, Func<T, TKey> fnOrderBy, Order order, int pageSize, int pageNumber);
        IQueryable<T> AsQueryable();
    }
}
