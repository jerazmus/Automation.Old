using Application.API.User.Model.Dto;

namespace Application.API.User.Service
{
    public interface IUserQueryHandler
    {
        Task<IEnumerable<UserDto>> Get();
        Task<UserDto> Get(Guid id);
    }
}
