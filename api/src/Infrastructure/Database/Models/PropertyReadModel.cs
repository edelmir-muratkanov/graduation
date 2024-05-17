namespace Infrastructure.Database.Models;

/// <summary>
/// Модель свойства для операций чтения
/// </summary>
internal sealed class PropertyReadModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}
