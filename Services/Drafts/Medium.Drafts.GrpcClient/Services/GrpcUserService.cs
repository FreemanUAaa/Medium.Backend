using Grpc.Net.Client;
using Medium.Drafts.GrpcClient.Interfaces;
using Medium.Drafts.GrpcClient.Options;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Medium.Drafts.GrpcClient.Services
{
    public class GrpcUserService : IGrpcUserService
    {
        private readonly GrpcConnectionOptions options;

        public GrpcUserService(IOptions<GrpcConnectionOptions> options) => this.options = options.Value;

        public async Task<bool> IsExistsAsync(Guid userId)
        {
            using GrpcChannel channel = GrpcChannel.ForAddress(options.UserGrpcServicesUrl);
            UserService.UserServiceClient userService = new UserService.UserServiceClient(channel);
            IsExistsResponse response = await userService.IsExistsAsync(
                new IsExistsRequest() { UserId = userId.ToString() }
            );

            return response.IsExists;
        
        }
    }
}
