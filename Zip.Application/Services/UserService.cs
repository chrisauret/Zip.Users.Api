using Zip.Application.Contracts;
using Zip.Domain.Entities;
using Zip.Domain.Result;

namespace Zip.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<IEnumerable<User>>> GetAll()
        {
            var users = await _userRepository.GetAllAsync().ConfigureAwait(false); ;
            return new Result<IEnumerable<User>>(users);
        }

        public async Task<Result<User>> GetById(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id).ConfigureAwait(false);

            if (user == null)
            {
                return new Result<User>("User not found");
            }

            return new Result<User>(user);
        }

        public async Task<Result<User>> Create(User user)
        {
            var existingUser = await _userRepository.GetByIdAsync(user.Id).ConfigureAwait(false);

            var existingEmail = await _userRepository.GetByEmailAsync(user.Email).ConfigureAwait(false);

            if (existingEmail != null)
            {
                return new Result<User>("Email already exists");
            }

            var createdUser = await _userRepository.CreateAsync(user).ConfigureAwait(false); ;

            return new Result<User>(createdUser);
        }
    }
}
