using Bragil.Core.Exceptions;
using Bragil.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Bragil.Core.Repositories.EntityCore
{
    public static class ServiceCollectionExtensions
    {
        /// <summary><![CDATA[
        /// Configuração do Entity Framework Core.
        /// Exemplo:
        ///     services.AddEntityCoreRepository<MyDbContext>(o => 
        ///     {
        ///         o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        ///     });
        /// ]]></summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="optionsBuilder">Função anônima para configuração das opções do Entity Framework Core</param>
        public static void AddEntityCoreRepository<TContext>(
                                this IServiceCollection services,
                                Action<DbContextOptionsBuilder> optionsBuilder) where TContext: DbContext
        {
            if (optionsBuilder == null)
                throw new InvalidConfigException("É necessário fornecer a função anônima de configuração do Entity Framework Core.");

            services.AddDbContext<TContext>(optionsBuilder);
            services.AddScoped(typeof(IRepositoryAsync<,>), typeof(EntityCoreRepository<,>));
        }
    }
}
