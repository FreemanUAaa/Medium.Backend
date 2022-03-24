using MediatR;
using Medium.Users.Core.Interfaces.Caching;
using Medium.Users.Core.Redis;
using System;

namespace Medium.Users.Application.Handlers.UserPhotos.Queries.GetUserPhotoBytes
{
    public class GetUserPhotoBytesQuery : IRequest<byte[]>, ICacheableMediatrQuery
    {
        public Guid UserId { get; set; }

        public string CacheKey => RedisKeys.GetUserPhotoKey(UserId);
    }
}
