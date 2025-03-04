using Macaner.Ecomerce.Domain.Entity;
using Macaner.Ecomerce.Domain.Interface;
using Macaner.Ecomerce.Infrastructure.Interface;

namespace Macaner.Ecomerce.Domain.Core
{
    public class UserDomain : IUserDomain
    {
        private readonly IUserRepository _userRepository;

        public UserDomain(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Users Authenticate(string login, string password)
        {
            return _userRepository.Authenticate(login, password);
        }
    }
}
