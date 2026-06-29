using Demo.Application.Services.ProductService;
using Demo.Application.Services.UserService;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ProductService>();
            services.AddScoped<UserService>();
            return services;
        }
    }
}
