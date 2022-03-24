using MediatR;
using Medium.Users.Core.Interfaces.Caching;
using Medium.Users.Core.Redis;
using System;

namespace Medium.Users.Application.Handlers.BioPhotos.Queries.GetBioPhotoBytes
{
    public class GetBioPhotoBytesQuery : IRequest<byte[]>, ICacheableMediatrQuery
    {
        public Guid BioPhotoId { get; set; }

        public string CacheKey => RedisKeys.GetUserBioKey(BioPhotoId);
    }
}
