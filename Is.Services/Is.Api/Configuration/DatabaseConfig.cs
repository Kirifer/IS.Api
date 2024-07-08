using Is.Core.Authentication;
using Is.Core.Config;
using Is.Core.Database;
using Is.Core.Database.Abstraction.Interface;
using Is.Core.Extensions;
using Is.Datalayer;
using Is.Datalayer.Implementation;
using Is.Datalayer.Interface;
using Is.DataLayer;

namespace Is.Api.Configurations
{
    public static class DatabaseConfig
    {
        public static IServiceCollection AddAtsDatabase(this IServiceCollection services, IMicroServiceConfig envConfig)
        {
            services.AddSingleton<IDbConfig, AtsDbConfig>(
                config => new AtsDbConfig()
                {
                    Host = envConfig.DatabaseConfig!.Host,
                    Port = Convert.ToUInt16(envConfig.DatabaseConfig.Port),
                    Database = envConfig.DatabaseConfig.DatabaseName,
                    User = envConfig.DatabaseConfig.User,
                    Password = envConfig.DatabaseConfig.Password,
                    Pooling = true
                }
            );

            services.AddTransient<IDbMigration, AtsDbMigration>();
            services.AddTransient<IDbUserContext>(provider =>
            {
                var userContext = provider.GetService<IUserContext>();
                return new DbUserContext(userContext!);
            });
            services.AddTransient(provider =>
            {
                var dbConfig = provider.GetRequiredService<IDbConfig>();
                var dbUserContext = provider.GetRequiredService<IDbUserContext>();
                var dbMigration = provider.GetRequiredService<IDbMigration>();
                return new AtsDbContext(dbConfig, dbUserContext, dbMigration);
            });

            services.AddScoped<IUserRepository, UserRepository>();


            //services.RegisterAssemblies<IDbQuerySingle>("Is.DataLayer", DependencyLifetime.Transient);
            //services.RegisterAssemblies<IDbQuery>("Is.DataLayer", DependencyLifetime.Transient);
            //services.RegisterAssemblies<IDbCommand>("Is.DataLayer", DependencyLifetime.Transient);

            return services;
        }

        public static IApplicationBuilder UseAtsDatabase(this IApplicationBuilder app)
        {
            var migrationRef = app.ApplicationServices.GetService<IDbMigration>();
            migrationRef!.ExecuteMigration();

            return app;
        }
    }
}
