namespace Domain.Methods;

/// <summary>
/// Интерфейс репозитория для работы с параметрами метода.
/// </summary>
public interface IMethodParameterRepository
{
    /// <summary>
    /// Вставляет коллекцию параметров метода.
    /// </summary>
    /// <param name="parameters">Коллекция параметров метода для вставки.</param>
    void InsertRange(IEnumerable<MethodParameter> parameters);

    /// <summary>
    /// Удаляет коллекцию параметров метода.
    /// </summary>
    /// <param name="parameters">Коллекция параметров метода для удаления.</param>
    void RemoveRange(IEnumerable<MethodParameter> parameters);

    /// <summary>
    /// Обновляет коллекцию параметров метода.
    /// </summary>
    /// <param name="parameters">Коллекция параметров метода для обновления.</param>
    void UpdateRange(IEnumerable<MethodParameter> parameters);
}
