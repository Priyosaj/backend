using System.Reflection;
using AutoMapper;

namespace Priyosaj.Service.Helpers.MapperProfile;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => a.FullName.ToLower().Contains("evv"))
            .ToList();
        ApplyMappingsFromAssembly(assemblies);
    }

    private void ApplyMappingsFromAssembly(List<Assembly> assemblies)
    {
        var types = assemblies
            .SelectMany(assembly => assembly.GetExportedTypes())
            .Where(t => t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
            .ToArray();

        foreach (var type in types)
        {
            var iMapType = typeof(IMapFrom<>);
            var mapperInvocationParam = new object[] { this };
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod("Mapping");
            if (methodInfo != null)
                methodInfo.Invoke(instance, mapperInvocationParam);
            else
            {
                var interfaces = type
                    .GetInterfaces()
                    .Where(i => i.Name == iMapType.Name)
                    .ToArray();
                var methodInfos = interfaces
                    .Select(i => i.GetMethod("Mapping"))
                    .Where(m => m != null)
                    .ToArray();
                foreach (var mf in methodInfos)
                    mf?.Invoke(instance, mapperInvocationParam);
            }
        }
    }
}