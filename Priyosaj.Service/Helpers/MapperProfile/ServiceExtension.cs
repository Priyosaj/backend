using Microsoft.Extensions.DependencyInjection;

namespace Priyosaj.Service.Helpers.MapperProfile;

public static class ServiceExtension
{
    public static void AddAutoMapperFromProfile(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}