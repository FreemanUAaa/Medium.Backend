using MediatR;
using System;

namespace Medium.Desings.Application.Handlers.Desings.Commands.CreateDesing
{
    public class CreateDesingCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
    }
}
