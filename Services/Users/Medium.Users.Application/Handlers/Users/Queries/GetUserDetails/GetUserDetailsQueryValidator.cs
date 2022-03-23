using FluentValidation;

namespace Medium.Users.Application.Handlers.Users.Queries.GetUserDetails
{
    public class GetUserDetailsQueryValidator : AbstractValidator<GetUserDetailsQuery>
    {
        public GetUserDetailsQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
