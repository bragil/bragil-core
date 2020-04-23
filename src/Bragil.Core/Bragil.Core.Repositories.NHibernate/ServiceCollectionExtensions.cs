using Bragil.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Cfg;
using NHibernate.Cfg.Loquacious;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;

namespace Bragil.Core.Repositories.NHibernate
{
    public static class ServiceCollectionExtensions
    {
        /// <summary><![CDATA[
        /// Configuração do NHibernate.
        /// Exemplo:
        ///     services.AddNHibernateRepository(
        ///         typeof(MyClassMap).Assembly.ExportedTypes,
        ///         c =>
        ///         {
        ///             c.Dialect<MsSql2012Dialect>();
        ///             c.ConnectionString = connectionString;
        ///             c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
        ///             c.SchemaAction = SchemaAutoAction.Validate;
        ///             c.LogFormattedSql = true;
        ///             c.LogSqlInConsole = true;
        ///         });
        /// ]]></summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="mappingTypes">Coleção contendo todos os tipos (classes) de mapeamento.</param>
        /// <param name="actionDbIntegration">Função anônima para configurar o banco de dados</param>
        /// <returns></returns>
        public static void AddNHibernateRepository(
                                    this IServiceCollection services, 
                                    IEnumerable<Type> mappingTypes,
                                    Action<IDbIntegrationConfigurationProperties> actionDbIntegration)
        {
            var mapper = new ModelMapper();
            mapper.AddMappings(mappingTypes);
            HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            var configuration = new Configuration();
            configuration.DataBaseIntegration(actionDbIntegration);
            configuration.AddMapping(domainMapping);

            var sessionFactory = configuration.BuildSessionFactory();

            services.AddSingleton(sessionFactory);
            services.AddScoped(factory => sessionFactory.OpenSession());
            services.AddScoped(typeof(IRepositoryAsync<,>), typeof(NHibernateRepository<,>));
        }
    }
}
