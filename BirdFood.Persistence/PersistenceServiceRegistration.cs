using BirdFood.Application.Data;
using BirdFood.Domain.Abstraction.Repositories;
using BirdFood.Persistence.Extension;
using BirdFood.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BirdFood.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureOptions<DatabaseOptionSetup>();
            services.AddDbContext<BirdFoodDBContext>(
                (serviceProvider, dbContextOptionBuilder) =>
                {
                    var databaseOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;
                    dbContextOptionBuilder.UseSqlServer(databaseOptions.ConnectionString, sqlServerAction =>
                    {
                        sqlServerAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                        sqlServerAction.CommandTimeout(databaseOptions.CommandTimeout);
                    });
                    dbContextOptionBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
                    dbContextOptionBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
                });

            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddScoped<IUnitOfWork>(sp =>
                sp.GetRequiredService<BirdFoodDBContext>());
            services.AddScoped<IApplicationDBContext>(sp =>
                sp.GetRequiredService<BirdFoodDBContext>());
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IShopRepository, ShopRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<ITokenUsedRepository, TokenUsedRepository>();
            //services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
