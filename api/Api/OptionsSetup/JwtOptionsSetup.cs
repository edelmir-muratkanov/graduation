using Api.Infrastructure.Authentication;
using Microsoft.Extensions.Options;

namespace Api.OptionsSetup;

public class JwtOptionsSetup(IConfiguration configuration) : IConfigureOptions<JwtOptions>
{
    public void Configure(JwtOptions options)
    {
        configuration.GetSection(JwtOptions.SectionName).Bind(options);
    }
}