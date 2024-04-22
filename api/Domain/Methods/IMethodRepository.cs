﻿namespace Domain.Methods;

public interface IMethodRepository
{
    Task<Method?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> IsNameUniqueAsync(string name);
    void Insert(Method method);
    void Update(Method method);
}