using Bragil.Core.Interfaces;
using Bragil.Core.Paging;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bragil.Core.Repositories.NHibernate
{
    /// <summary>
    /// Repositório do NHibernate.
    /// </summary>
    /// <typeparam name="T">Tipo da entidade</typeparam>
    /// <typeparam name="TId">Tipo do ID da entidade</typeparam>
    public class NHibernateRepository<T, TId> : IRepositoryAsync<T, TId>, ITransactionalAsync, IDisposable
        where T : class, IEntity<TId>
    {
        private readonly ISession session;
        private ITransaction transaction;

        public NHibernateRepository(ISession session)
            => this.session = session;

        public async Task Add(T entity)
            => await session.SaveAsync(entity);

        public IQueryable<T> AsQueryable()
            => session.Query<T>();

        public async Task BeginTransaction()
        {
            transaction = session.BeginTransaction();
            await Task.CompletedTask;
        }

        public async Task Commit()
            => await transaction.CommitAsync();

        public async Task Delete(TId id)
        {
            var entity = await session.GetAsync<T>(id);
            await session.DeleteAsync(entity);
        }

        public async Task Delete(T entity)
            => await session.DeleteAsync(entity);

        public void Dispose()
        {
            transaction?.Dispose();
            session.Dispose();
        }

        public async Task<bool> Exists(Func<T, bool> filter)
            => await Task.FromResult(AsQueryable().Any(x => filter(x)));

        public async Task<List<T>> FindAll(Func<T, bool> predicate)
            => await Task.FromResult(AsQueryable().Where(x => predicate(x)).ToList());

        public async Task<Page<T>> FindAll(Func<T, bool> predicate, int pageSize, int pageNumber)
            => await Task.FromResult(AsQueryable().Where(x => predicate(x)).ToPagedList(pageSize, pageNumber));

        public async Task<Page<T>> FindAll<TKey>(Func<T, bool> predicate, Func<T, TKey> fnOrderBy, Order order, int pageSize, int pageNumber)
            => order == Order.ASC
                ? await Task.FromResult(AsQueryable().Where(x => predicate(x)).OrderBy(x => fnOrderBy(x)).ToPagedList(pageSize, pageNumber))
                : await Task.FromResult(AsQueryable().Where(x => predicate(x)).OrderByDescending(x => fnOrderBy(x)).ToPagedList(pageSize, pageNumber));

        public async Task<T> FindOne(Func<T, bool> predicate)
            => await Task.FromResult(AsQueryable().FirstOrDefault(x => predicate(x)));

        public async Task<List<T>> GetAll()
            => await Task.FromResult(AsQueryable().ToList());

        public async Task<Page<T>> GetAll(int pageSize, int pageNumber)
            => await Task.FromResult(AsQueryable().ToPagedList(pageSize, pageNumber));

        public async Task<T> GetById(TId id)
            => await session.GetAsync<T>(id);

        public async Task Rollback()
            => await transaction.RollbackAsync();

        public async Task Update(T entity)
            => await session.UpdateAsync(entity);
    }
}
