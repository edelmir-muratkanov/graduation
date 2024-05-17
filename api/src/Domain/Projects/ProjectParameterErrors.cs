namespace Domain.Projects;

/// <summary>
/// Представляет класс ошибок, связанных с параметрами проекта.
/// </summary>
public static class ProjectParameterErrors
{
    /// <summary>
    /// Ошибка: свойство параметра не найдено.
    /// </summary>
    public static readonly Error InvalidProperty =
        Error.NotFound("ProjectParameter.InvalidProperty", "Свойство параметра не найдено");
}
