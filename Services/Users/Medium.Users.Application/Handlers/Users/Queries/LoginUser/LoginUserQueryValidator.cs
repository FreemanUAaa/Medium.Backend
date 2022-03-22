using FluentValidation;

namespace Medium.Users.Application.Handlers.Users.Queries.LoginUser
{
    public class LoginUserQueryValidator : AbstractValidator<LoginUserQuery>
    {
        public LoginUserQueryValidator()
        {
            RuleFor(x => x.Email).EmailAddress();

            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        }
    }
}
