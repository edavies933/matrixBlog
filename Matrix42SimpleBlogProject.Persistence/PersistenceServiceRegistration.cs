using Matrix42SimpleBlogProject.Application.Contracts.Persistence;
using Matrix42SimpleBlogProject.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Microsoft.EntityFrameworkCore;

namespace Matrix42SimpleBlogProject.Persistence
{
    public static class PersistenceServiceRegistration
    {
        private static string? _connectionString;
        public const string DatabaseConfigurationKey = "DatabaseConfig";

        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseConfig = configuration.GetSection(DatabaseConfigurationKey).Get<DatabaseConfig>();
            if (databaseConfig is null)
            {
                throw new NullReferenceException("Can't find db configuration");
            }

            if (_connectionString is null)
            {
                var builder = new NpgsqlConnectionStringBuilder
                {
                    Host = databaseConfig.Host,
                    Port = databaseConfig.Port,
                    Database = databaseConfig.Database,
                    Username = databaseConfig.User,
                    SearchPath = databaseConfig.SearchPath,
                    Password = databaseConfig.Password,
                    TrustServerCertificate = databaseConfig.TrustServerCertificate,
                    SslMode = databaseConfig.SslMode,
                    MaxPoolSize = databaseConfig.MaxPoolSize
                };
                _connectionString = builder.ConnectionString;
            }

            services.AddDbContext<BlogProjectDbContext>(options => options.UseNpgsql(_connectionString));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            services.AddScoped<ITagsRepository, TagRepository>();
            services.AddScoped<ICommentsRepository, CommentsRepository>();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            return services;
        }
    }
}
