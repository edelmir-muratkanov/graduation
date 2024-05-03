using System.Linq.Expressions;

namespace Domain.Calculation;

public interface ICalculationRepository
{
    Task<Calculation?> GetOne(
        Expression<Func<Calculation, bool>> predicate,
        CancellationToken cancellationToken = default);

    Task<List<Calculation>> Get(Expression<Func<Calculation, bool>>? predicate,
        CancellationToken cancellationToken = default);

    void Insert(Calculation calculation);
    void Update(Calculation calculation);
    void Remove(Calculation calculation);
}
