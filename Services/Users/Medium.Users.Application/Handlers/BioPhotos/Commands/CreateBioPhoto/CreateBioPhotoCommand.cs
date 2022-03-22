using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace Medium.Users.Application.Handlers.BioPhotos.Commands.CreateBioPhoto
{
    public class CreateBioPhotoCommand : IRequest<Guid>
    {
        public IFormFile Photo { get; set; }

        public Guid UserId { get; set; }
    }
}
