using FluentValidation;

namespace Medium.Drafts.Application.Handlers.Drafts.Queries.GetDraftDetails
{
    public class GetDraftDetailsQueryValidator : AbstractValidator<GetDraftDetailsQuery>
    {
        public GetDraftDetailsQueryValidator()
        {
            RuleFor(x => x.DraftId).NotEmpty();

            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
