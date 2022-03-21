using MediatR;
using Medium.Users.Application.Common;
using Medium.Users.Core.Common.Password;
using Medium.Users.Core.Exceptions;
using Medium.Users.Core.Interfaces;
using Medium.Users.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Medium.Users.Application.Handlers.Users.Queries.LoginUser
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, LoginUserVm>
    {
        private readonly ILogger<LoginUserQueryHandler> logger;

        private readonly IOptions<AuthOptions> authOptions;

        private readonly IDatabaseContext database;

        public LoginUserQueryHandler(IDatabaseContext database, ILogger<LoginUserQueryHandler> logger, IOptions<AuthOptions> authOptions) =>
            (this.database, this.logger, this.authOptions) = (database, logger, authOptions);

        public async Task<LoginUserVm> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            User user = await database.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user == null)
            {
                throw new Exception(ExceptionStrings.UserNotFound);
            }

            string passwordHash = PasswordHasher.HashPassword(request.Password, user.PasswordSalt).Hash;

            if (user.PasswordHash != passwordHash)
            {
                throw new Exception(ExceptionStrings.FailedLogIn);
            }

            logger.LogInformation("The user has successfully entered");
        }
    }
}
