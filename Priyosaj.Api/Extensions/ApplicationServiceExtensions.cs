using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Priyosaj.Api.Errors;
using Priyosaj.Api.Helpers;
using Priyosaj.Business.Data;
using Priyosaj.Business.Services;
using Priyosaj.Contacts.Interfaces;
using StackExchange.Redis;

namespace Priyosaj.Api.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<IConnectionMultiplexer>(c =>
        {
            var configuration = ConfigurationOptions.Parse(config.GetConnectionString("Redis"), true);
            return ConnectionMultiplexer.Connect(configuration);
        });
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IProductCategoryService, ProductCategoryService>();
        services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
        services.AddScoped<ITokenService, TokenService>();
        
        // services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
        // services.AddScoped<IPhotoService, PhotoService>();
        
        // services.AddDbContextPool<StoreContext>(options =>
        // {
        //     options.UseSqlite(config.GetConnectionString("DBConnectionString"));
        // });
        
        services.AddDbContextPool<StoreContext>(options =>
        {
            options.UseNpgsql(config.GetConnectionString("PostgresConnection"));
        });
        
        services.Configure<ApiBehaviorOptions>(options => 
        {
            options.InvalidModelStateResponseFactory = actionContext => 
            {
                var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();

                var errorResponse = new ApiValidationErrorResponse
                {
                    Errors = errors
                };

                return new BadRequestObjectResult(errorResponse);
            };
        });

        return services;
    }
}