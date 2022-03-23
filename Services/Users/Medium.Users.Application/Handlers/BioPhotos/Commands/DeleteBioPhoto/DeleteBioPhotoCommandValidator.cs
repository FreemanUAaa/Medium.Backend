using FluentValidation;

namespace Medium.Users.Application.Handlers.BioPhotos.Commands.DeleteBioPhoto
{
    public class DeleteBioPhotoCommandValidator : AbstractValidator<DeleteBioPhotoCommand>
    {
        public DeleteBioPhotoCommandValidator()
        {
            RuleFor(x => x.BioPhotoId).NotEmpty();
        }
    }
}
