using InOut.Infrastructure.Repositories.Interfaces;
using InOut.Service.Services.Interfaces;

namespace InOut.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    }
}