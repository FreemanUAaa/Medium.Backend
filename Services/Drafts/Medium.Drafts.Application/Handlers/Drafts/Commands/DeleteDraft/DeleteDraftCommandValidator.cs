using FluentValidation;

namespace Medium.Drafts.Application.Handlers.Drafts.Commands.DeleteDraft
{
    public class DeleteDraftCommandValidator : AbstractValidator<DeleteDraftCommand>
    {
        public DeleteDraftCommandValidator()
        {
            RuleFor(x => x.DraftId).NotEmpty();

            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
