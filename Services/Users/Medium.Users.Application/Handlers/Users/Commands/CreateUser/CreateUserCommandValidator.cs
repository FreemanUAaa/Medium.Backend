using FluentValidation;

namespace Medium.Users.Application.Handlers.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(2);

            RuleFor(x => x.Email).EmailAddress();

            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        }
    }
}
