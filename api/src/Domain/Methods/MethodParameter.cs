﻿namespace Domain.Methods;

/// <summary>
/// <see cref="Method"/> parameter
/// </summary>
public class MethodParameter : Entity
{
    private MethodParameter(
        Guid id,
        Guid methodId,
        Guid propertyId,
        ParameterValueGroup? first,
        ParameterValueGroup? second) : base(id)
    {
        Id = id;
        MethodId = methodId;
        PropertyId = propertyId;
        FirstParameters = first;
        SecondParameters = second;
    }

    private MethodParameter()
    {
    }

    public Guid MethodId { get; private set; }
    public Guid PropertyId { get; private set; }

    public ParameterValueGroup? FirstParameters { get; private set; }

    public ParameterValueGroup? SecondParameters { get; private set; }

    public static Result<MethodParameter> Create(
        Guid methodId,
        Guid propertyId,
        ParameterValueGroup? first,
        ParameterValueGroup? second)
    {
        if (first is null && second is null)
        {
            return Result.Failure<MethodParameter>(MethodParameterErrors.MissingFirstAndSecond);
        }

        return new MethodParameter(Guid.NewGuid(), methodId, propertyId, first, second);
    }
}