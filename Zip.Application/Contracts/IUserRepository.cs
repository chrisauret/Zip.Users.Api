using Zip.Domain.Entities;

namespace Zip.Application.Contracts;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);

    Task<User?> GetByEmailAsync(string email);

    Task<IList<User>> GetAllAsync();

    Task<User> CreateAsync(User user);
}