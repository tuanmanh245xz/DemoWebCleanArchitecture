using Demo.Application.Interfaces;
using Demo.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;


namespace Demo.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, JsonProductRepository>();

            return services;
        }
    }
}
