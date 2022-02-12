using Microsoft.EntityFrameworkCore;
using Priyosaj.Business.Data;

namespace Priyosaj.Api.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        // services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
        // services.AddScoped<IPhotoService, PhotoService>();
        // services.AddScoped<ITokenService, TokenService>();
        // services.AddScoped<IUserRepository, UserRepository>();
        // services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
        // services.AddDbContextPool<StoreContext>(options =>
        // {
        //     options.UseSqlite(config.GetConnectionString("DBConnectionString"));
        // });
        services.AddDbContextPool<StoreContext>(options =>
        {
            options.UseNpgsql(config.GetConnectionString("PostgresConnection"));
        });

        return services;
    }
}