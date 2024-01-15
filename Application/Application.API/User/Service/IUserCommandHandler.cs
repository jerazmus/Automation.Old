using Application.API.User.Model.Dto;

namespace Application.API.User.Service
{
    public interface IUserCommandHandler
    {
        Task<UserDto> Add(AddUserDto user);
        Task<UserDto> Update(Guid id, UpdateUserDto user);
        Task Delete(Guid id);
    }
}
