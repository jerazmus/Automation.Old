using Application.API.User.Model;
using Application.API.User.Model.Dto;

namespace Application.API.User.Service
{
    public class UserCommandHandler : IUserCommandHandler
    {
        private readonly IUserRepository _userRepository;

        public UserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Add(AddUserDto user)
        {
            return await _userRepository.Add(user);
        }

        public async Task<UserDto> Update(Guid id, UpdateUserDto user)
        {
            return await _userRepository.Update(id, user);
        }

        public async Task Delete(Guid id)
        {
            await _userRepository.Delete(id);
        }
    }
}
