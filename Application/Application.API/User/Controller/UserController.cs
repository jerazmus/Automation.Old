using Application.API.User.Model.Dto;
using Application.API.User.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.User.Controller
{
    [Route("v1/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserCommandHandler _userCommandHandler;
        private readonly IUserQueryHandler _userQueryHandler;

        public UserController(IUserCommandHandler userCommandHandler, IUserQueryHandler userQueryHandler)
        {
            _userCommandHandler = userCommandHandler;
            _userQueryHandler = userQueryHandler;
        }

        [HttpGet]
        public async Task<IEnumerable<UserDto>> Get()
        {
            return await _userQueryHandler.Get();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> Get(Guid id)
        {
            return await _userQueryHandler.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Add([FromBody] AddUserDto user)
        {
            return await _userCommandHandler.Add(user);
        }

        [HttpPut]
        public async Task<ActionResult<UserDto>> Update(Guid id, [FromBody] UpdateUserDto user)
        {
            return await _userCommandHandler.Update(id, user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _userCommandHandler.Delete(id);
            return NoContent();
        }
    }
}
