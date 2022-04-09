using FluentValidation;

namespace Medium.Desings.Application.Handlers.Desings.Commands.UpdateDesing
{
    public class UpdateDesingCommandValidator : AbstractValidator<UpdateDesingCommand>
    {
        public UpdateDesingCommandValidator()
        {
            RuleFor(x => x.ShowBlogroll).NotEmpty();

            RuleFor(x => x.IsTextSelected).NotEmpty();
        }
    }
}
