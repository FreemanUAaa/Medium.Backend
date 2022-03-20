using MediatR;
using Medium.Users.Core.Common.Password;
using Medium.Users.Core.Exceptions;
using Medium.Users.Core.Interfaces;
using Medium.Users.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medium.Users.Application.Handlers.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IDatabaseContext database;

        private readonly ILogger<CreateUserCommandHandler> logger;

        public CreateUserCommandHandler(IDatabaseContext database, ILogger<CreateUserCommandHandler> logger) => 
            (this.database, this.logger) = (database, logger);

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (database.Users.Any(x => x.Email == request.Email))
            {
                throw new Exception(ExceptionStrings.OccupiedEmail);
            }

            PasswordHashResponse hashPassword = PasswordHasher.HashPassword(request.Password, null);

            string username = request.Email.Split("@")[0];

            User user = new User()
            {
                UserName = username,
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                PasswordHash = hashPassword.Hash,
                PasswordSalt = hashPassword.Salt,
            };

            await database.Users.AddAsync(user);
            await database.SaveChangesAsync(cancellationToken);

            logger.LogInformation($"User: \"{user.Name}\" was successfully created");

            return user.Id;
        }
    }
}
