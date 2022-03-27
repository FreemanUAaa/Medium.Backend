using FluentValidation;

namespace Medium.Users.Application.Handlers.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.Email).EmailAddress();

            RuleFor(x => x.UserName).NotEmpty().MinimumLength(2);

            RuleFor(x => x.Name).NotEmpty().MinimumLength(2);
        }
    }
}
