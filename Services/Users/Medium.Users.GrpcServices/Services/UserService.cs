using Google.Protobuf;
using Grpc.Core;
using Medium.Users.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Medium.Users.GrpcServices.Services
{
    public class UserService : UserSerivce.UserSerivceBase
    {

        private readonly ILogger<UserService> logger;

        private readonly IDatabaseContext database;

        public UserService(IDatabaseContext database, ILogger<UserService> logger) =>
            (this.database, this.logger) = (database, logger);

        public override async Task<UserResponse> GetUser(GetUserRequest request, ServerCallContext context)
        {
            Core.Models.User user = await database.Users.FindAsync(request.UserId);

            logger.LogInformation("Grpc: User successfully received");

            return new UserResponse()
            {
                Bio = user.Bio,
                Name = user.Name,
                Email = user.Email,
                Id = user.Id.ToString(),
                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                PasswordSalt = ByteString.CopyFrom(user.PasswordSalt),
            };
        }

        public override async Task<IsExistsResponse> IsExists(IsExistsRequest request, ServerCallContext context)
        {
            Guid userId = Guid.Parse(request.UserId);
            bool isExists = await database.Users.AnyAsync(x => x.Id == userId);

            logger.LogInformation("Grpc: User verified successfully");

            return new IsExistsResponse() { IsExists = isExists };
        }
    }
}
