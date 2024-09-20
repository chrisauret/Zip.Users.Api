using Microsoft.Extensions.Options;
using Zip.Application.Abstractions;
using Zip.Application.Contracts;
using Zip.Domain.Entities;
using Zip.Domain.Result;

namespace Zip.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOptions<ApplicationOptions> _options;

        public AccountService(
            IAccountRepository accountRepository,
            IUserRepository userRepository,
            IOptions<ApplicationOptions> options)
        {
            _userRepository = userRepository;
            _accountRepository = accountRepository;
            _options = options;
        }

        public async Task<Result<Account>> Create(Account request)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user == null)
            {
                // TODO: Errors to be represented by specific error class, not strings.
                return new Result<Account>("User not found");
            }

            if (!HasSufficientCredit(user, _options.Value.CreditLimit, out var userCredit))
            {
                return new Result<Account>("Insufficient Credit");
            }

            var account = await _accountRepository.CreateAsync(request).ConfigureAwait(false); ;

            return new Result<Account>(account);
        }

        private static bool HasSufficientCredit(User user, decimal creditLimit, out decimal userCredit)
        {
            userCredit = user.Salary - user.Expenses;

            return userCredit >= creditLimit;
        }

        public async Task<Result<Account>> GetById(Guid id)
        {
            var account = await _accountRepository.GetByIdAsync(id).ConfigureAwait(false); ;

            if (account == null)
            {
                // TODO: Errors to be represented by specific error class, not strings.
                return new Result<Account>("Account not found");
            }

            return new Result<Account>(account);
        }
    }
}
