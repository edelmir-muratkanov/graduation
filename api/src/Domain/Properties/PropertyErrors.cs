namespace Domain.Properties;

/// <summary>
/// Содержит стандартные ошибки, связанные с операциями над сущностями свойств.
/// </summary>
public static class PropertyErrors
{
    /// <summary>
    /// Ошибка, указывающая на конфликт из-за неуникального названия свойства.
    /// </summary>
    public static readonly Error NameNotUnique =
        Error.Conflict("Property.NameNotUnique", "Свойство с таким именем уже существует");

    /// <summary>
    /// Ошибка, указывающая на то, что свойство не было найдено.
    /// </summary>
    public static readonly Error NotFound = Error.NotFound("Property.NotFound", "Свойство не найдено");
}
