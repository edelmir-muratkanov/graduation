namespace Domain.Projects;

public static class ProjectErrors
{
    public static readonly Error NotFound = Error.NotFound(
        "Project.NotFound",
        "Проект не найден");

    public static readonly Error AlreadyMember = Error.Conflict(
        "Project.AlreadyMember",
        "У данного пользователя уже есть доступ");

    public static readonly Error NotMember = Error.NotFound(
        "Project.NotMember",
        "Данный пользователь не имеет доступа к проекту");

    public static readonly Error DuplicateParameter = Error.Conflict(
        "Project.DuplicateParameter",
        "Проект уже содержит указанный параметр");

    public static readonly Error ParameterNotFound = Error.NotFound(
        "Project.ParameterNotFound",
        "Проект не содержит указанный параметр");

    public static readonly Error DuplicateMethod = Error.Conflict(
        "Project.DuplicateMethod",
        "Проект уже содержит указанный метод");

    public static readonly Error MethodNotFound = Error.NotFound(
        "Project.MethodNotFound",
        "Проект не содержит указанный метод");

    public static readonly Error OnlyForOwner = Error.Forbidden(
        "Project.OnlyForOwner",
        "Может изменить только владелец проекта");

    public static readonly Error OnlyForMembers = Error.Forbidden(
        "Project.OnlyForMembers",
        "Могут изменять только владелец или участники проекта");
}