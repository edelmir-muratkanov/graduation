namespace Application.Property.GetById;

/// <summary>
/// Ответ на запрос <see cref="GetPropertyByIdQuery"/>
/// </summary>
public record GetPropertyByIdResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
}

/// <summary>
/// Запрос на получение свойства по его идентификатору.
/// </summary>
public record GetPropertyByIdQuery(Guid Id) : IQuery<GetPropertyByIdResponse>;
