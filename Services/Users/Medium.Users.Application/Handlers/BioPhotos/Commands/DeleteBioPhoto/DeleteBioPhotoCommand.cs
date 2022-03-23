using MediatR;
using System;

namespace Medium.Users.Application.Handlers.BioPhotos.Commands.DeleteBioPhoto
{
    public class DeleteBioPhotoCommand : IRequest
    {
        public Guid BioPhotoId { get; set; }
    }
}
