using Bragil.Core.Interfaces;
using Bragil.Core.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bragil.Core.Repositories.EntityCore
{
    /// <summary>
    /// Repositório genérico para o Entity Framework Core.
    /// </summary>
    /// <typeparam name="T">Tipo da entidade</typeparam>
    /// <typeparam name="TId">Tipo do ID da entidade</typeparam>
    public class EntityCoreRepository<T, TId> : IRepositoryAsync<T, TId>, IUnitOfWorkAsync
        where T : class, IEntity<TId>
    {
        private readonly DbContext context;

        public EntityCoreRepository(DbContext context)
            => this.context = context;

        public async Task Add(T entity)
            => await context.Set<T>().AddAsync(entity);

        public IQueryable<T> AsQueryable()
            => context.Set<T>().AsQueryable();

        public async Task Delete(TId id)
        {
            var entity = context.Set<T>().Where(e => e.Id.Equals(id)).FirstOrDefault();
            context.Set<T>().Remove(entity);
            await Task.CompletedTask;
        }

        public async Task Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            await Task.CompletedTask;
        }

        public async Task<bool> Exists(Func<T, bool> filter)
            => await context.Set<T>().AnyAsync(x => filter(x));

        public async Task<List<T>> FindAll(Func<T, bool> predicate)
            => await context.Set<T>().Where(x => predicate(x)).AsQueryable().ToListAsync();

        public async Task<Page<T>> FindAll(Func<T, bool> predicate, int pageSize, int pageNumber)
            => await context.Set<T>().Where(predicate).ToPagedListAsync(pageSize, pageNumber);

        public async Task<Page<T>> FindAll<TKey>(Func<T, bool> predicate, Func<T, TKey> fnOrderBy, Order order, int pageSize, int pageNumber)
            => order == Order.ASC
                ? await context.Set<T>().Where(predicate).OrderBy(fnOrderBy).ToPagedListAsync(pageSize, pageNumber)
                : await context.Set<T>().Where(predicate).OrderByDescending(fnOrderBy).ToPagedListAsync(pageSize, pageNumber);

        public async Task<T> FindOne(Func<T, bool> predicate)
            => await context.Set<T>().FirstOrDefaultAsync(x => predicate(x));

        public async Task<List<T>> GetAll()
            => await context.Set<T>().ToListAsync();

        public async Task<Page<T>> GetAll(int pageSize, int pageNumber)
            => await context.Set<T>().ToPagedListAsync(pageSize, pageNumber);

        public async Task<T> GetById(TId id)
            => await context.Set<T>().FirstOrDefaultAsync(e => e.Id.Equals(id));

        public async Task RejectChanges()
        {
            foreach (var entry in context.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged))
            {
                if (entry.State == EntityState.Added)
                    entry.State = EntityState.Detached;
                else if (entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
                    entry.Reload();
            }
            await Task.CompletedTask;
        }

        public async Task SaveChanges()
            => await context.SaveChangesAsync();

        public async Task Update(T entity)
        {
            context.Set<T>().Update(entity);
            await Task.CompletedTask;
        }
    }
}
