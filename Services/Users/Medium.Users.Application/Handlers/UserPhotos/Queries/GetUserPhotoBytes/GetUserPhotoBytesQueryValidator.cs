using FluentValidation;

namespace Medium.Users.Application.Handlers.UserPhotos.Queries.GetUserPhotoBytes
{
    public class GetUserPhotoBytesQueryValidator : AbstractValidator<GetUserPhotoBytesQuery>
    {
        public GetUserPhotoBytesQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
