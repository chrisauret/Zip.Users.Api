using Zip.Domain.Entities;
using Zip.Domain.Result;

namespace Zip.Application.Contracts;

public interface IUserService
{
    Task<Result<IEnumerable<User>>> GetAll();

    Task<Result<User>> GetById(Guid id);

    Task<Result<User>> Create(User user);
}