using MediatR;
using System;

namespace Medium.Users.Application.Handlers.Users.Queries.GetUserDetails
{
    public class GetUserDetailsQuery : IRequest<GetUserDetailsVm>
    {
        public Guid UserId { get; set; }
    }
}
