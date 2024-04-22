namespace Domain.Properties;

public static class PropertyErrors
{
    public static readonly Error NameNotUnique =
        Error.Conflict("Property.NameNotUnique", "Свойство с таким именем уже существует");

    public static readonly Error NotFound = Error.NotFound("Property.NotFound", "Свойство не найдено");
}