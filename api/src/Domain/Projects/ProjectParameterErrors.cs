namespace Domain.Projects;

public static class ProjectParameterErrors
{
    public static readonly Error InvalidProperty =
        Error.NotFound("ProjectParameter.InvalidProperty", "Свойство параметра не найдено");
}
