using Bragil.Core.Exceptions;
using Bragil.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Bragil.Core.Repositories.MongoDb
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configuração do MongoRepository.
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="config">IConfiguration</param>
        /// <param name="databaseName">Nome do banco de dados, se fornecido. Se não fornecido, será obtido de "MongoDb.DbName" do appSettings.json.</param>
        /// <param name="connectionStringName">Nome da chave da connection string se fornecido. Se não fornecido, será obtido "MongoDb"</param>
        public static void AddMongoDbRepository(
                                this IServiceCollection services, 
                                string databaseName,
                                string connectionString)
        {
            if (databaseName == null)
                throw new InvalidConfigException("É necessário fornecer o nome da base de dados MongoDB.");

            if (connectionString == null)
                throw new InvalidConfigException("É necessário fornecer a Connection string do MongoDB.");

            services.AddScoped(sp => new MongoClient(connectionString).GetDatabase(databaseName));
            services.AddScoped(typeof(IRepositoryAsync<,>), typeof(MongoRepository<,>));
        }
    }
}
