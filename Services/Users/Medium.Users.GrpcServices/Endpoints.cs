using Medium.Users.GrpcServices.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Medium.Users.GrpcServices
{
    public static class Endpoints
    {
        public static IEndpointRouteBuilder AddGrpcEndpoint(this IEndpointRouteBuilder endpoint)
        {
            endpoint.MapGrpcService<UserService>();
 
            return endpoint;
        }
    }
}
