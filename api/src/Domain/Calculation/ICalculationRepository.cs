namespace Domain.Calculation;

public interface ICalculationRepository
{
    Task<Calculation?> GetByIdAsync(Guid calculationId, CancellationToken cancellationToken = default);

    Task<Calculation?> GetByProjectAndMethodAsync(
        Guid projectId,
        Guid methodId,
        CancellationToken cancellationToken = default);

    Task<List<Calculation>> GetByMethodAsync(Guid methodId, CancellationToken cancellationToken = default);

    Task<bool> Exists(Guid projectId, Guid methodId, CancellationToken cancellationToken = default);
    void Insert(Calculation calculation);
    void InsertRange(IEnumerable<Calculation> calculations);
    void Update(Calculation calculation);
    void Remove(Calculation calculation);
}
