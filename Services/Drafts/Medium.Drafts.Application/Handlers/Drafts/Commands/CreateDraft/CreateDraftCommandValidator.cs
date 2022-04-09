using FluentValidation;

namespace Medium.Drafts.Application.Handlers.Drafts.Commands.CreateDraft
{
    public class CreateDraftCommandValidator : AbstractValidator<CreateDraftCommand>
    {
        public CreateDraftCommandValidator()
        {
            RuleFor(x => x.Details).NotEmpty().MinimumLength(10);

            RuleFor(x => x.Title).NotEmpty().MinimumLength(5);

            RuleFor(x => x.Body).NotEmpty();

            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
