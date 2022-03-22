using FluentValidation;

namespace Medium.Users.Application.Handlers.UserPhotos.Commands.CreateUserPhoto
{
    public class CreateUserPhotoCommandValidator : AbstractValidator<CreateUserPhotoCommand>
    {
        public CreateUserPhotoCommandValidator()
        {
            RuleFor(x => x.Photo).NotNull();

            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
