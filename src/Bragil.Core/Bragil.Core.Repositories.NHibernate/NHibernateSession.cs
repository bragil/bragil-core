using Bragil.Core.Interfaces;
using NHibernate;
using System;
using System.Threading.Tasks;

namespace Bragil.Core.Repositories.NHibernate
{
    /// <summary>
    /// Classe para 
    /// </summary>
    public class NHibernateSession : ITransactionalAsync
    {
        private ITransaction transaction;
        public ISession Session { get; private set; }

        public NHibernateSession(ISession session)
            => Session = session;

        public async Task BeginTransaction()
        {
            transaction = Session.BeginTransaction();
            await Task.CompletedTask;
        }

        public async Task Commit()
            => await transaction?.CommitAsync();

        public async Task Rollback()
            => await transaction.RollbackAsync();
    }
}
