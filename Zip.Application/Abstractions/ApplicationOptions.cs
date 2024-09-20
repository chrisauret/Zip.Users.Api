using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Zip.Application.Abstractions;

public class ApplicationOptions
{
    /// <summary>
    /// The minimum credit limit needed.
    /// </summary>
    public decimal CreditLimit { get; set; }
}

public class ApplicationOptionsSetup : IConfigureOptions<ApplicationOptions>
{
    private readonly IConfiguration _configuration;

    public ApplicationOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(ApplicationOptions options)
    {
        _configuration.GetSection(nameof(ApplicationOptions))
            .Bind(options);
    }
}
