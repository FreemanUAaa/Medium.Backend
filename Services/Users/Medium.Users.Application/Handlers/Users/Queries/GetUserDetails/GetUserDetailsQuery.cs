using MediatR;
using Medium.Users.Core.Interfaces.Caching;
using Medium.Users.Core.Redis;
using System;

namespace Medium.Users.Application.Handlers.Users.Queries.GetUserDetails
{
    public class GetUserDetailsQuery : IRequest<GetUserDetailsVm>, ICacheableMediatrQuery
    {
        public Guid UserId { get; set; }


        public string CacheKey => RedisKeys.GetUserDetailsKey(UserId);
    }
}
