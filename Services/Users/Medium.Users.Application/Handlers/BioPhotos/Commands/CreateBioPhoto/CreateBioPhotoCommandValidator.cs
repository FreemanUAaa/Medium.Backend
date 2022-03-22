using FluentValidation;

namespace Medium.Users.Application.Handlers.BioPhotos.Commands.CreateBioPhoto
{
    public class CreateBioPhotoCommandValidator : AbstractValidator<CreateBioPhotoCommand>
    {
        public CreateBioPhotoCommandValidator()
        {
            RuleFor(x => x.Photo).NotNull();

            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
