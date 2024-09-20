
using Microsoft.Extensions.DependencyInjection;
using Zip.Application.Contracts;
using Zip.Application.Services;

namespace Zip.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAccountService, AccountService>();

        return services;
    }
}
