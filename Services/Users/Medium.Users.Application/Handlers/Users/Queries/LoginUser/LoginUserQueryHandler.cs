using MediatR;
using Medium.Users.Application.Common;
using Medium.Users.Core.Common.Password;
using Medium.Users.Core.Exceptions;
using Medium.Users.Core.Interfaces;
using Medium.Users.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Medium.Users.Application.Handlers.Users.Queries.LoginUser
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, LoginUserVm>
    {
        private readonly ILogger<LoginUserQueryHandler> logger;

        private readonly IDatabaseContext database;

        private readonly AuthOptions authOptions;

        public LoginUserQueryHandler(IDatabaseContext database, ILogger<LoginUserQueryHandler> logger, IOptions<AuthOptions> authOptions) =>
            (this.database, this.logger, this.authOptions) = (database, logger, authOptions.Value);

        public async Task<LoginUserVm> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            User user = await database.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            ClaimsIdentity identity = GetIdentity(user);

            if (identity == null)
            {
                throw new Exception(ExceptionStrings.UserNotFound);
            }

            string passwordHash = PasswordHasher.HashPassword(request.Password, user.PasswordSalt).Hash;

            if (user.PasswordHash != passwordHash)
            {
                throw new Exception(ExceptionStrings.FailedLogIn);
            }

            DateTime now = DateTime.UtcNow;
            JwtSecurityToken jwt = new JwtSecurityToken(
                    issuer: authOptions.Issuer,
                    audience: authOptions.Audience,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(authOptions.Lifetime)),
                    signingCredentials: new SigningCredentials(authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            string token = new JwtSecurityTokenHandler().WriteToken(jwt);

            logger.LogInformation("The user has successfully entered");

            return new LoginUserVm() { AccessToken = token, UserId = user.Id };
        }

        private ClaimsIdentity GetIdentity(User user)
        {
            if (user == null)
            {
                return null;
            }

            List<Claim> claims = new List<Claim> { new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()), };
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}
