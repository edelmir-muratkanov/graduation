﻿namespace Domain.Projects.Events;

public sealed record ProjectMethodAddedDomainEvent(Guid ProjectId, Guid MethodId) : IDomainEvent
{
    public Guid ProjectId { get; set; } = ProjectId;
    public Guid MethodId { get; set; } = MethodId;
}