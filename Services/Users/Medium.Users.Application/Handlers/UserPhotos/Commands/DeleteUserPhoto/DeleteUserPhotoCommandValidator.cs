using FluentValidation;

namespace Medium.Users.Application.Handlers.UserPhotos.Commands.DeleteUserPhoto
{
    public class DeleteUserPhotoCommandValidator : AbstractValidator<DeleteUserPhotoCommand>
    {
        public DeleteUserPhotoCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
