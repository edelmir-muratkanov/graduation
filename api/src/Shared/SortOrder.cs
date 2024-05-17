using System.Text.Json.Serialization;

namespace Shared;

/// <summary>
/// Определяет порядок сортировки.
/// </summary>
/// <remarks>
/// Атрибут <see cref="JsonConverterAttribute"/> с типом <see cref="JsonStringEnumConverter"/>
/// используется для сериализации и десериализации значений перечисления <see cref="SortOrder"/>
/// в строковом формате при работе с JSON.
/// </remarks>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SortOrder
{
    /// <summary>
    /// Сортировка по возрастанию.
    /// </summary>
    Asc = 1,

    /// <summary>
    /// Сортировка по убыванию.
    /// </summary>s
    Desc = 2
}
