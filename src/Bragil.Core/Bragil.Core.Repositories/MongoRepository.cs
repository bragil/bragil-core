using Bragil.Core.Interfaces;
using Bragil.Core.Paging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bragil.Core.Repositories.MongoDb
{
    /// <summary>
    /// Repositório para MongoDB.
    /// </summary>
    /// <typeparam name="T">Tipo da entidade</typeparam>
    /// <typeparam name="TId">Tipo do ID da entidade</typeparam>
    public class MongoRepository<T, TId> : IRepositoryAsync<T, TId> where T : IEntity<TId>
    {
        private readonly IMongoCollection<T> collection;

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="database">Objeto IMongoDatabase</param>
        /// <param name="collectionName">Nome da collection (default: null)</param>
        public MongoRepository(IMongoDatabase database, string collectionName = null)
        {
            if (collectionName == null)
                collection = database.GetCollection<T>(typeof(T).Name);
            else
                collection = database.GetCollection<T>(collectionName);
        }

        public async Task Add(T entity)
            => await collection.InsertOneAsync(entity);

        public IQueryable<T> AsQueryable()
            => collection.AsQueryable();

        public async Task Delete(TId id)
            => await collection.DeleteOneAsync(x => x.Id.Equals(id));

        public async Task Delete(T entity)
            => await collection.DeleteOneAsync(x => x.Id.Equals(entity.Id));

        public async Task<bool> Exists(Func<T, bool> filter)
            => (await collection.CountDocumentsAsync(x => filter(x))) > 0;

        public async Task<List<T>> FindAll(Func<T, bool> predicate)
            => await (await collection.FindAsync(x => predicate(x))).ToListAsync();

        public async Task<Page<T>> FindAll(Func<T, bool> predicate, int pageSize, int pageNumber)
            => (await collection.FindAsync(x => predicate(x))).ToPagedList(pageSize, pageNumber);

        public async Task<Page<T>> FindAll<TKey>(Func<T, bool> predicate, Func<T, TKey> fnOrderBy, Order order, int pageSize, int pageNumber)
            => order == Order.ASC
                ? (await collection.FindAsync(x => predicate(x))).Current.OrderBy(fnOrderBy).ToPagedList(pageSize, pageNumber)
                : (await collection.FindAsync(x => predicate(x))).Current.OrderByDescending(fnOrderBy).ToPagedList(pageSize, pageNumber);

        public async Task<T> FindOne(Func<T, bool> predicate)
            => await (await collection.FindAsync(x => predicate(x))).FirstOrDefaultAsync();

        public async Task<List<T>> GetAll()
            => await (await collection.FindAsync(x => true)).ToListAsync();

        public async Task<Page<T>> GetAll(int pageSize, int pageNumber)
            => (await collection.FindAsync(x => true)).ToPagedList(pageSize, pageNumber);

        public async Task<T> GetById(TId id)
            => await (await collection.FindAsync(x => x.Id.Equals(id))).FirstOrDefaultAsync();

        public async Task Update(T entity)
            => await collection.ReplaceOneAsync(x => x.Id.Equals(entity.Id), entity);
    }
}
