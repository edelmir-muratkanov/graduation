using AutoMapper;

namespace Shared.Mappings;

/// <summary>
/// Интерфейс, представляющий собой маркер для маппинга объекта типа <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">Тип объекта, из которого происходит маппинг.</typeparam>
public interface IMapFrom<T>
{
    /// <summary>
    /// Настраивает маппинг объекта типа <typeparamref name="T"/> на текущий тип.
    /// </summary>
    /// <param name="profile">Профиль маппинга AutoMapper, в который добавляются настройки маппинга.</param>
    void Mapping(Profile profile)
    {
        profile.CreateMap(typeof(T), GetType());
    }
}
