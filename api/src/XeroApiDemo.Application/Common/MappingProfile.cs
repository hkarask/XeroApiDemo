using System.Reflection;

namespace XeroApiDemo.Application.Common;

public class MappingProfile : Profile
{
    private const string MappingMethodName = nameof(IMapFrom<object>.Mapping);

    public MappingProfile()
    {
        Assembly
            .GetExecutingAssembly()
            .GetExportedTypes()
            .Where(HasMappingInterface)
            .ToList()
            .ForEach(InvokeMappingMethod);
    }

    private void InvokeMappingMethod(Type type)
    {
        var method = GetTypeMappingMethod(type) ?? GetInterfaceMappingMethod(type);
        var instance = Activator.CreateInstance(type);
        method?.Invoke(instance, new object[] { this });
    }

    private static bool HasMappingInterface(Type type)
    {
        return type.GetInterfaces().Any(IsMapping);
    }

    private static bool IsMapping(Type type)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IMapFrom<>);
    }

    private static MethodInfo GetTypeMappingMethod(Type type)
    {
        return type.GetMethod(MappingMethodName);
    }

    private static MethodInfo GetInterfaceMappingMethod(Type type)
    {
        return type.GetInterface(typeof(IMapFrom<>).Name)?
            .GetMethod(MappingMethodName);
    }
}
