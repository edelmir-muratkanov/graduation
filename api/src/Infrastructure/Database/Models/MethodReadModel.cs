using Domain;

namespace Infrastructure.Database.Models;

/// <summary>
/// Модель метода для операций чтения
/// </summary>
internal sealed class MethodReadModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<CollectorType> CollectorTypes { get; set; }
    public List<MethodParameterReadModel> Parameters { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}
