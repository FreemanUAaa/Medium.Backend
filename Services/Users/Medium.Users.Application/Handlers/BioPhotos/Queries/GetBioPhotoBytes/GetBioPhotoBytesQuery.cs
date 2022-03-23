using MediatR;
using System;

namespace Medium.Users.Application.Handlers.BioPhotos.Queries.GetBioPhotoBytes
{
    public class GetBioPhotoBytesQuery : IRequest<byte[]>
    {
        public Guid BioPhotoId { get; set; }
    }
}
