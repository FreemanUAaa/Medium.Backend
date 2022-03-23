using FluentValidation;

namespace Medium.Users.Application.Handlers.BioPhotos.Queries.GetBioPhotoBytes
{
    public class GetBioPhotoBytesQueryValidator : AbstractValidator<GetBioPhotoBytesQuery>
    {
        public GetBioPhotoBytesQueryValidator()
        {
            RuleFor(x => x.BioPhotoId).NotEmpty();
        }
    }
}
