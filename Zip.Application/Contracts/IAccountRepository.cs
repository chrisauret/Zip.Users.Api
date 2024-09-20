
using Zip.Domain.Entities;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(Guid id);
    Task<Account> CreateAsync(Account account);
}
