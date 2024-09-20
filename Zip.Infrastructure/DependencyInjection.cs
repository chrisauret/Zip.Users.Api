
using Microsoft.Extensions.DependencyInjection;
using Zip.Application.Contracts;
using Zip.Infrastructure.Data;
using Zip.Infrastructure.Repositories;

namespace Zip.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            return services;
        }
    }
}