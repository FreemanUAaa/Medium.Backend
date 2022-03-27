using Medium.Users.Api.Controllers.Base;
using Medium.Users.Application.Handlers.Users.Commands.CreateUser;
using Medium.Users.Application.Handlers.Users.Commands.DeleteUser;
using Medium.Users.Application.Handlers.Users.Queries.GetUserDetails;
using Medium.Users.Application.Handlers.Users.Queries.LoginUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Medium.Users.Api.Controllers
{
    public class UsersController : BaseController
    {
        [HttpGet("{userId}")]
        [ApiVersion("1.0")]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(GetUserDetailsVm), 200)]
        public async Task<ActionResult<GetUserDetailsVm>> GetDetails([FromRoute] Guid userId)
        {
            GetUserDetailsQuery query = new GetUserDetailsQuery() { UserId = userId };

            return Ok(await Mediator.Send(query));
        }

        [HttpPost("login")]
        [ApiVersion("1.0")]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(LoginUserVm), 200)]
        public async Task<ActionResult<LoginUserVm>> Login([FromForm] LoginUserQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost("register")]
        [ApiVersion("1.0")]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(Guid), 200)]
        public async Task<ActionResult<Guid>> Register([FromForm] CreateUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [Authorize]
        [HttpDelete()]
        [ApiVersion("1.0")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult> Delete()
        {
            DeleteUserCommand command = new DeleteUserCommand() { UserId = UserId };

            await Mediator.Send(command);

            return NoContent();
        }
    }
}
