using FluentValidation;

namespace Medium.Drafts.Application.Handlers.Drafts.Queries.GetDraftListByUserId
{
    public class GetDraftListByUserIdQueryValidator : AbstractValidator<GetDraftListByUserIdQuery>
    {
        public GetDraftListByUserIdQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
