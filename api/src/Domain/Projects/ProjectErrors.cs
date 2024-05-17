namespace Domain.Projects;

/// <summary>
/// Представляет набор ошибок, связанных с проектом.
/// </summary>
public static class ProjectErrors
{
    /// <summary>
    /// Ошибка: проект не найден.
    /// </summary>
    public static readonly Error NotFound = Error.NotFound(
        "Project.NotFound",
        "Проект не найден");

    /// <summary>
    /// Ошибка: пользователь уже является участником проекта.
    /// </summary>
    public static readonly Error AlreadyMember = Error.Conflict(
        "Project.AlreadyMember",
        "У данного пользователя уже есть доступ");

    /// <summary>
    /// Ошибка: пользователь не является участником проекта.
    /// </summary>
    public static readonly Error NotMember = Error.NotFound(
        "Project.NotMember",
        "Данный пользователь не имеет доступа к проекту");

    /// <summary>
    /// Ошибка: дублирующийся параметр проекта.
    /// </summary>
    public static readonly Error DuplicateParameter = Error.Conflict(
        "Project.DuplicateParameter",
        "Проект уже содержит указанный параметр");

    /// <summary>
    /// Ошибка: параметр проекта не найден.
    /// </summary>
    public static readonly Error ParameterNotFound = Error.NotFound(
        "Project.ParameterNotFound",
        "Проект не содержит указанный параметр");

    /// <summary>
    /// Ошибка: дублирующийся метод проекта.
    /// </summary>
    public static readonly Error DuplicateMethod = Error.Conflict(
        "Project.DuplicateMethod",
        "Проект уже содержит указанный метод");

    /// <summary>
    /// Ошибка: метод проекта не найден.
    /// </summary>
    public static readonly Error MethodNotFound = Error.NotFound(
        "Project.MethodNotFound",
        "Проект не содержит указанный метод");

    /// <summary>
    /// Ошибка: доступ к изменению информации только для владельца проекта.
    /// </summary>
    public static readonly Error OnlyForOwner = Error.Forbidden(
        "Project.OnlyForOwner",
        "Может изменить только владелец проекта");

    /// <summary>
    /// Ошибка: доступ к изменению информации только для владельца или участников проекта.
    /// </summary>
    public static readonly Error OnlyForMembers = Error.Forbidden(
        "Project.OnlyForMembers",
        "Могут изменять только владелец или участники проекта");
}
