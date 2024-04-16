using System.Reflection;

namespace InvoiceR.Application.Mapping;

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private static void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes().Where(x =>
           typeof(IMapsterMap).IsAssignableFrom(x) && !x.IsInterface).ToList();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod("ConfigureMapping", BindingFlags.Public | BindingFlags.Instance);
            methodInfo?.Invoke(instance, new object[] { });
        }
    }
}
