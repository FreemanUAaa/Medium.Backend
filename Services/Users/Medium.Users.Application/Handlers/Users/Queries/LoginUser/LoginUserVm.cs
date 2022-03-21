using System;

namespace Medium.Users.Application.Handlers.Users.Queries.LoginUser
{
    public class LoginUserVm
    {
        public string AccessToken { get; set; }

        public Guid UserId { get; set; }
    }
}
