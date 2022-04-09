using FluentValidation;

namespace Medium.Drafts.Application.Handlers.Drafts.Commands.UpdateDraft
{
    public class UpdateDraftCommandValidator : AbstractValidator<UpdateDraftCommand>
    {
        public UpdateDraftCommandValidator()
        {
            RuleFor(x => x.Details).NotEmpty().MinimumLength(10);

            RuleFor(x => x.Title).NotEmpty().MinimumLength(5);

            RuleFor(x => x.Body).NotEmpty();

            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
