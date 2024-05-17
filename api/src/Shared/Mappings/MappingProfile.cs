using System.Reflection;
using AutoMapper;

namespace Shared.Mappings;

/// <summary>
/// Профиль маппинга для AutoMapper, позволяющий автоматически применять маппинги из определенной сборки.
/// </summary>
public class MappingProfile : Profile
{
    /// <summary>
    /// Применяет маппинги из всех типов, реализующих интерфейс <see cref="IMapFrom{T}"/> из указанной сборки.
    /// </summary>
    /// <param name="assembly">Сборка, из которой необходимо загрузить типы для маппинга.</param>
    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
            .ToList();

        foreach (Type? type in types)
        {
            object? instance = Activator.CreateInstance(type);
            MethodInfo? methodInfo = type.GetMethod("Mapping")
                                     ?? type.GetInterface("IMapFrom`1")!.GetMethod("Mapping");

            methodInfo?.Invoke(instance, new object[] { this });
        }
    }
}
