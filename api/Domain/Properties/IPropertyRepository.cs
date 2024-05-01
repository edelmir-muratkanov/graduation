namespace Domain.Properties;

public interface IPropertyRepository
{
    Task<Property?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> IsNameUniqueAsync(string name);
    Task<bool> Exists(Guid id);
    void Insert(Property property);
    void Remove(Property property);
    void Update(Property property);
}
