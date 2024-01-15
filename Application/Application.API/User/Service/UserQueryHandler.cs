using Application.API.User.Model;
using Application.API.User.Model.Dto;

namespace Application.API.User.Service
{
    public class UserQueryHandler : IUserQueryHandler
    {
        private readonly IUserRepository _userRepository;

        public UserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> Get()
        {
            return await _userRepository.Get();
        }

        public async Task<UserDto> Get(Guid id)
        {
            return await _userRepository.Get(id);
        }
    }
}
