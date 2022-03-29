using MediatR;
using Medium.Users.Core.Exceptions;
using Medium.Users.Core.Interfaces;
using Medium.Users.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Medium.Users.Application.Handlers.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
    {
        private readonly ILogger<UpdateUserCommandHandler> logger;

        private readonly IDatabaseContext database;

        public UpdateUserCommandHandler(IDatabaseContext database, ILogger<UpdateUserCommandHandler> logger) =>
            (this.database, this.logger) = (database, logger);

        public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User user = await database.Users.FindAsync(request.UserId);

            if (user == null)
            {
                throw new Exception(ExceptionStrings.UserNotFound);
            }

            (user.Name, user.Email, user.Bio, user.UserName) =
            (request.Name, request.Email, request.Bio, request.UserName);

            database.Users.Update(user);
            await database.SaveChangesAsync(cancellationToken);

            logger.LogInformation("User successfully updated");

            return user.Id;
        }
    }
}
