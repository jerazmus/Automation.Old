using Application.API.User.Model.Dto;

namespace Application.API.User.Model
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDto>> Get();
        Task<UserDto> Get(Guid id);
        Task<UserDto> Add(AddUserDto user);
        Task<UserDto> Update(Guid id, UpdateUserDto user);
        Task Delete(Guid id);
    }
}
