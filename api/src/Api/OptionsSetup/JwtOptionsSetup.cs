using Infrastructure.Authentication;
using Microsoft.Extensions.Options;

namespace Api.OptionsSetup;

/// <summary>
/// Настраивает параметры JWT, связывая их с соответствующим разделом конфигурации.
/// </summary>
/// <param name="configuration">Конфигурация приложения.</param>
public class JwtOptionsSetup(IConfiguration configuration) : IConfigureOptions<JwtOptions>
{
    /// <summary>
    /// Настраивает указанные параметры, связывая их с разделом JWT в конфигурации.
    /// </summary>
    /// <param name="options">Настраиваемые параметры JWT.</param>
    public void Configure(JwtOptions options)
    {
        configuration.GetSection(JwtOptions.SectionName).Bind(options);
    }
}
