using MediatR;
using Medium.Drafts.Core.Interfaces.Caching;
using Medium.Drafts.Core.Redis;
using System;

namespace Medium.Drafts.Application.Handlers.Drafts.Queries.GetDraftListByUserId
{
    public class GetDraftListByUserIdQuery : IRequest<GetDraftListVm>, ICacheableMediatrQuery
    {
        public Guid UserId { get; set; }


        public string CacheKey => RedisKeys.GetDraftListKey(UserId);
    }
}
