using Zip.Domain.Entities;
using Zip.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Zip.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _dbContext;

    public AccountRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Account> CreateAsync(Account account)
    {
        await _dbContext.Accounts.AddAsync(account).ConfigureAwait(false);

        await _dbContext.SaveChangesAsync();

        return account;
    }

    public async Task<Account?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Accounts
                .AsNoTracking()
                .FirstOrDefaultAsync(account => account.Id == id)
                .ConfigureAwait(false);
    }
}