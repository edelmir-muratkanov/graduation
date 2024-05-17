namespace Application.Property;

/// <summary>
/// Исключение, выбрасываемое при отсутствии свойства.
/// </summary>
/// <param name="propertyId">Идентификатор свойства, которое не было найдено.</param>
public sealed class PropertyNotFoundException(Guid propertyId)
    : Exception($"Property with id = {propertyId} not found")
{
    public Guid PropertyId { get; set; } = propertyId;
}
