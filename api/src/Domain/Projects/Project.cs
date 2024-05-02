﻿using Domain.Projects.Events;

namespace Domain.Projects;

public class Project : AuditableEntity
{
    private readonly List<ProjectMember> _members = [];
    private readonly List<ProjectMethod> _methods = [];
    private readonly List<ProjectParameter> _parameters = [];

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

    private Project()
    {
    }

    public string Name { get; private set; }
    public string Country { get; private set; }
    public string Operator { get; private set; }
    public ProjectType ProjectType { get; private set; }
    public CollectorType CollectorType { get; private set; }
    public List<ProjectParameter> Parameters => _parameters.ToList();
    public List<ProjectMember> Members => _members.ToList();
    public List<ProjectMethod> Methods => _methods.ToList();

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

    public Result IsOwner(string userId)
    {
        return CreatedBy != userId ? Result.Failure(ProjectErrors.OnlyForOwner) : Result.Success();
    }

    public Result IsMember(string userId)
    {
        ProjectMember? member = Members.FirstOrDefault(m => m.MemberId.ToString() == userId);
        return member is null ? Result.Failure(ProjectErrors.OnlyForMembers) : Result.Success();
    }
}