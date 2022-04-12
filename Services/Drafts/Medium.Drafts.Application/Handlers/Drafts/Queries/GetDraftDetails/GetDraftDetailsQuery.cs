using MediatR;
using Medium.Drafts.Core.Interfaces.Caching;
using Medium.Drafts.Core.Models;
using Medium.Drafts.Core.Redis;
using System;

namespace Medium.Drafts.Application.Handlers.Drafts.Queries.GetDraftDetails
{
    public class GetDraftDetailsQuery : IRequest<Draft>, ICacheableMediatrQuery
    {
        public Guid DraftId { get; set; }

        public Guid UserId { get; set; }


        public string CacheKey => RedisKeys.GetDraftDetailsKey(DraftId);
    }
}
