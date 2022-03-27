using MediatR;
using System;

namespace Medium.Users.Application.Handlers.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Bio { get; set; }

        public string Email { get; set; }
    }
}
