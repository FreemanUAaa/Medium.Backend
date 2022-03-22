using MediatR;
using System;

namespace Medium.Users.Application.Handlers.UserPhotos.Queries.GetUserPhotoBytes
{
    public class GetUserPhotoBytesQuery : IRequest<byte[]>
    {
        public Guid UserId { get; set; }
    }
}
