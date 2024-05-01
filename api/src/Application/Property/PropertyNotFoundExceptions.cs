namespace Application.Property;

public sealed class PropertyNotFoundException(Guid propertyId)
    : Exception($"Property with id = {propertyId} not found")
{
    public Guid PropertyId { get; set; } = propertyId;
}
