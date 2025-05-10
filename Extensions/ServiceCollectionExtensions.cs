using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Patikadev_RestfulApi.Context;
using Patikadev_RestfulApi.Mapping;
using Patikadev_RestfulApi.Service;
using Patikadev_RestfulApi.Services;
using Patikadev_RestfulApi.Services.Interfaces;
using Patikadev_RestfulApi.Services.Validations;

namespace Patikadev_RestfulApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)  // extension method
    {
        services.AddDbContext<AppDbContext>(config =>
        {
            config.UseSqlServer(configuration.GetConnectionString("SqlServer"));
        });

        return services;
    }

    public static IServiceCollection AddValidationConfiguration(this IServiceCollection services)
    {
        services.AddControllers().AddFluentValidation(
            x => x.RegisterValidatorsFromAssemblyContaining<CreateBookValidator>());
        return services;
    }

    public static IServiceCollection AddServiceConfiguration(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }

    public static IServiceCollection AddMapperConfiguration(this IServiceCollection services)
    {
        services.AddSingleton(new MapperConfiguration(x => x.AddProfile(new MappingConfiguration())).CreateMapper());
        return services;
    }
}
