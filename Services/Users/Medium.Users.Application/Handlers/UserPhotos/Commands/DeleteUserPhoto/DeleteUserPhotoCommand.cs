using MediatR;
using System;

namespace Medium.Users.Application.Handlers.UserPhotos.Commands.DeleteUserPhoto
{
    public class DeleteUserPhotoCommand : IRequest
    {
        public Guid UserId { get; set; }
    }
}
