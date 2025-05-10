using Microsoft.EntityFrameworkCore;
using Patikadev_RestfulApi.Context;
using Patikadev_RestfulApi.Interfaces;
using Patikadev_RestfulApi.Middleware;
using Patikadev_RestfulApi.Service;
using Patikadev_RestfulApi.Services;

namespace Patikadev_RestfulApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ApplicationExtensions(this IServiceCollection services, IConfiguration configuration)  // extension method
    {
        services.AddDbContext<AppDbContext>(config =>
        {
            config.UseSqlServer(configuration.GetConnectionString("SqlServer"));
        });

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }

}
