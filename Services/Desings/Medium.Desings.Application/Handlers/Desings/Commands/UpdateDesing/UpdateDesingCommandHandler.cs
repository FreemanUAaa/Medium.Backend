using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Medium.Desings.Application.Handlers.Desings.Commands.UpdateDesing
{
    public class UpdateDesingCommandHandler : IRequestHandler<UpdateDesingCommand>
    {
        public Task<Unit> Handle(UpdateDesingCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
