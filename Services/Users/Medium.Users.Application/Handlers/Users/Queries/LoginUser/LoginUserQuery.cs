using MediatR;

namespace Medium.Users.Application.Handlers.Users.Queries.LoginUser
{
    public class LoginUserQuery : IRequest<LoginUserVm>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
