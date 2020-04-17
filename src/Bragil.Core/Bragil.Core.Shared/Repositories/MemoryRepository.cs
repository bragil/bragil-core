using Bragil.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Bragil.Core.Paging;

namespace Bragil.Core.Repositories
{
    /// <summary>
    /// Repositório em memória. Somente para testes.
    /// </summary>
    /// <typeparam name="T">Tipo da entidade</typeparam>
    /// <typeparam name="TId">Tipo do ID da entidade</typeparam>
    public class MemoryRepository<T, TId>: IRepository<T, TId> where T : IEntity<TId>
    {
        private readonly Dictionary<TId, T> collection;

        public MemoryRepository()
            => collection = new Dictionary<TId, T>();

        public void Add(T entity)
        {
            collection.Add(entity.Id, entity);
        }

        public IQueryable<T> AsQueryable()
            => collection.Values.AsQueryable();

        public void Delete(TId id)
        {
            collection.Remove(id);
        }

        public void Delete(T entity)
        {
            collection.Remove(entity.Id);
        }

        public bool Exists(Func<T, bool> filter)
            => collection.Values.Any(filter);

        public List<T> FindAll(Func<T, bool> predicate)
            => collection.Values.Where(predicate).ToList();

        public Page<T> FindAll(Func<T, bool> predicate, int pageSize, int pageNumber)
            => collection.Values.Where(predicate).ToPagedList(pageSize, pageNumber);

        public Page<T> FindAll<TKey>(Func<T, bool> predicate, Func<T, TKey> fnOrderBy,
                                     Order order, int pageSize, int pageNumber)
            => order == Order.ASC
                ? collection.Values.Where(predicate).OrderBy(fnOrderBy).ToPagedList(pageSize, pageNumber)
                : collection.Values.Where(predicate).OrderByDescending(fnOrderBy).ToPagedList(pageSize, pageNumber);

        public T FindOne(Func<T, bool> predicate)
            => collection.Values.FirstOrDefault(predicate);

        public List<T> GetAll()
            => collection.Values.ToList();

        public Page<T> GetAll(int pageSize, int pageNumber)
            => collection.Values.ToPagedList(pageSize, pageNumber);

        public T GetById(TId id)
            => collection.TryGetValue(id, out var obj) ? obj : default;

        public void Update(T entity)
        {
            if (collection.ContainsKey(entity.Id))
                collection[entity.Id] = entity;
        }
    }
}
