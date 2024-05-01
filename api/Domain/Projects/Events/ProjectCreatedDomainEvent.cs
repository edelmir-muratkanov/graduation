﻿namespace Domain.Projects.Events;

public sealed record ProjectCreatedDomainEvent(Guid ProjectId) : IDomainEvent
{
    public Guid ProjectId { get; set; } = ProjectId;
}
