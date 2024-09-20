using Zip.Domain.Entities;
using Zip.Domain.Result;

namespace Zip.Application.Contracts;

public interface IAccountService
{
    Task<Result<Account>> GetById(Guid id);
    Task<Result<Account>> Create(Account createAccountRequest);
}
