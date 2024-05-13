using BirdFood.Application.Abstractions;
using BirdFood.Infrastructure.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace BirdFood.Infrastructure.DependencyInjection.Extension
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
            => services.AddTransient<IJwtTokenService, JwtTokenService>();
    }
}
