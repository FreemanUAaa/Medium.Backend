using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace Medium.Users.Application.Handlers.UserPhotos.Commands.CreateUserPhoto
{
    public class CreateUserPhotoCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }

        public IFormFile Photo { get; set; }
    }
}
