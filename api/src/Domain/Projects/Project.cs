using Domain.Projects.Events;

namespace Domain.Projects;

/// <summary>
/// Класс, представляющий проект.
/// </summary>
public class Project : AuditableEntity
{
    /// <summary>
    /// Список участников проекта.
    /// </summary>
    private readonly List<ProjectMember> _members = [];

    /// <summary>
    /// Список методов проекта.
    /// </summary>
    private readonly List<ProjectMethod> _methods = [];

    /// <summary>
    /// Список параметров проекта.
    /// </summary>
    private readonly List<ProjectParameter> _parameters = [];

    /// <summary>
    /// Создает экземпляр класса Project.
    /// </summary>
    /// <param name="id">Идентификатор проекта.</param>
    /// <param name="name">Название проекта.</param>
    /// <param name="country">Страна проекта.</param>
    /// <param name="operator">Оператор проекта.</param>
    /// <param name="projectType">Тип проекта.</param>
    /// <param name="collectorType">Тип коллектора проекта.</param>
    private Project(
        Guid id,
        string name,
        string country,
        string @operator,
        ProjectType projectType,
        CollectorType collectorType) : base(id)
    {
        Name = name;
        Country = country;
        Operator = @operator;
        ProjectType = projectType;
        CollectorType = collectorType;
    }

    /// <summary>
    /// Закрытый конструктор без параметров, предотвращающий создание экземпляров класса за его пределами.
    /// </summary>
    /// <remarks>
    /// Необходим для корректной работы EF Core
    /// </remarks>
    private Project()
    {
    }

    /// <summary>
    /// Название проекта.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Страна проекта.
    /// </summary>
    public string Country { get; private set; }

    /// <summary>
    /// Оператор (нефтедобывающая компания) проекта.
    /// </summary>
    public string Operator { get; private set; }

    /// <summary>
    /// Тип проекта.
    /// </summary>
    public ProjectType ProjectType { get; private set; }

    /// <summary>
    /// Тип коллектора проекта.
    /// </summary>
    public CollectorType CollectorType { get; private set; }

    /// <summary>
    /// Список параметров проекта.
    /// </summary>
    public List<ProjectParameter> Parameters => _parameters.ToList();

    /// <summary>
    /// Список участников проекта.
    /// </summary>
    public List<ProjectMember> Members => _members.ToList();

    /// <summary>
    /// Список методов проекта.
    /// </summary>
    public List<ProjectMethod> Methods => _methods.ToList();

    /// <summary>
    /// Создает новый экземпляр проекта.
    /// </summary>
    /// <param name="name">Название проекта.</param>
    /// <param name="country">Страна проекта.</param>
    /// <param name="operator">Оператор проекта.</param>
    /// <param name="projectType">Тип проекта.</param>
    /// <param name="collectorType">Тип коллектора проекта.</param>
    /// <returns>Результат создания проекта.</returns>
    public static Result<Project> Create(
        string name,
        string country,
        string @operator,
        ProjectType projectType,
        CollectorType collectorType)
    {
        var project = new Project(
            Guid.NewGuid(),
            name,
            country,
            @operator,
            projectType,
            collectorType);

        project.Raise(new ProjectCreatedDomainEvent(project.Id));

        return project;
    }

    /// <summary>
    /// Обновляет основную информацию о проекте.
    /// </summary>
    /// <param name="name">Новое название проекта.</param>
    /// <param name="country">Новая страна проекта.</param>
    /// <param name="operator">Новый оператор проекта.</param>
    /// <param name="projectType">Новый тип проекта.</param>
    /// <param name="collectorType">Новый тип коллектора проекта.</param>
    /// <returns>Результат обновления основной информации о проекте.</returns>
    public Result UpdateBaseInfo(
        string? name,
        string? country,
        string? @operator,
        ProjectType? projectType,
        CollectorType? collectorType)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            Name = name;
        }

        if (!string.IsNullOrWhiteSpace(country))
        {
            Country = country;
        }

        if (!string.IsNullOrWhiteSpace(@operator))
        {
            Operator = @operator;
        }

        if (projectType is not null)
        {
            ProjectType = (ProjectType)projectType;
        }

        if (collectorType is not null)
        {
            CollectorType = (CollectorType)collectorType;
        }

        Raise(new ProjectBaseInfoUpdatedDomainEvent(Id));

        return Result.Success();
    }

    /// <summary>
    /// Добавляет нового участника в проект.
    /// </summary>
    /// <param name="memberId">Идентификатор участника.</param>
    /// <returns>Результат добавления участника в проект.</returns
    public Result<ProjectMember> AddMember(Guid memberId)
    {
        if (_members.Any(m => m.MemberId == memberId))
        {
            return Result.Failure<ProjectMember>(ProjectErrors.AlreadyMember);
        }

        var projectMember = new ProjectMember(Id, memberId);

        _members.Add(projectMember);

        Raise(new ProjectMemberAddedDomainEvent(Id, projectMember.MemberId));

        return Result.Success(projectMember);
    }

    /// <summary>
    /// Удаляет участника из проекта.
    /// </summary>
    /// <param name="memberId">Идентификатор участника.</param>
    /// <returns>Результат удаления участника из проекта.</returns>
    public Result RemoveMember(Guid memberId)
    {
        ProjectMember? member = _members.FirstOrDefault(m => m.MemberId == memberId);

        if (member is null)
        {
            return Result.Failure(ProjectErrors.NotMember);
        }

        _members.Remove(member);

        Raise(new ProjectMemberRemovedDomainEvent(Id, member.MemberId));

        return Result.Success();
    }

    /// <summary>
    /// Добавляет параметр в проект.
    /// </summary>
    /// <param name="propertyId">Идентификатор свойства.</param
    /// <param name="value">Значение параметра.</param>
    /// <returns>Результат добавления параметра в проект.</returns>
    public Result<ProjectParameter> AddParameter(Guid propertyId, double value)
    {
        if (_parameters.Any(p => p.PropertyId == propertyId))
        {
            return Result.Failure<ProjectParameter>(ProjectErrors.DuplicateParameter);
        }

        Result<ProjectParameter>? parameterResult = ProjectParameter.Create(Id, propertyId, value);

        if (parameterResult.IsFailure)
        {
            return parameterResult;
        }

        _parameters.Add(parameterResult.Value);

        Raise(new ProjectParameterAddedDomainEvent(Id, parameterResult.Value.Id));

        return Result.Success(parameterResult.Value);
    }

    /// <summary>
    /// Удаляет параметр из проекта.
    /// </summary>
    /// <param name="parameterId">Идентификатор параметра.</param>
    /// <returns>Результат удаления параметра из проекта.</returns>
    public Result RemoveParameter(Guid parameterId)
    {
        ProjectParameter? parameter = _parameters.FirstOrDefault(p => p.Id == parameterId);

        if (parameter is null)
        {
            return Result.Failure(ProjectErrors.ParameterNotFound);
        }

        _parameters.Remove(parameter);
        Raise(new ProjectParameterRemovedDomainEvent(Id, parameter.PropertyId));

        return Result.Success();
    }

    /// <summary>
    /// Добавляет метод в проект.
    /// </summary>
    /// <param name="methodId">Идентификатор метода.</param>
    /// <returns>Результат добавления метода в проект.</returns>
    public Result<ProjectMethod> AddMethod(Guid methodId)
    {
        if (_methods.Any(m => m.MethodId == methodId))
        {
            return Result.Failure<ProjectMethod>(ProjectErrors.DuplicateMethod);
        }

        var projectMethod = new ProjectMethod(Id, methodId);

        _methods.Add(projectMethod);

        Raise(new ProjectMethodAddedDomainEvent(Id, projectMethod.MethodId));

        return Result.Success(projectMethod);
    }

    /// <summary>
    /// Удаляет метод из проекта.
    /// </summary>
    /// <param name="methodId">Идентификатор метода.</param>
    /// <returns>Результат удаления метода из проекта.</returns>
    public Result RemoveMethod(Guid methodId)
    {
        ProjectMethod? method = _methods.FirstOrDefault(m => m.MethodId == methodId);

        if (method is null)
        {
            return Result.Failure(ProjectErrors.MethodNotFound);
        }

        _methods.Remove(method);
        Raise(new ProjectMethodRemovedDomainEvent(Id, method.MethodId));

        return Result.Success();
    }

    /// <summary>
    /// Проверяет, является ли пользователь создателем проекта.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <returns>Результат проверки на принадлежность к создателю проекта.</returns>
    public Result IsOwner(string userId)
    {
        return CreatedBy != userId ? Result.Failure(ProjectErrors.OnlyForOwner) : Result.Success();
    }

    /// <summary>
    /// Проверяет, является ли пользователь участником проекта.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <returns>Результат проверки на принадлежность к участнику проекта.</returns>
    public Result IsMember(string userId)
    {
        ProjectMember? member = Members.FirstOrDefault(m => m.MemberId.ToString() == userId);
        return member is null ? Result.Failure(ProjectErrors.OnlyForMembers) : Result.Success();
    }
}
