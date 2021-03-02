using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator:AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(c => c.Email).NotEmpty();
            RuleFor(c => c.Email).EmailAddress();

            RuleFor(c => c.FirstName).MinimumLength(2);
            RuleFor(c => c.LastName).MinimumLength(2);

            RuleFor(c => c.PasswordHash).NotEmpty();
        }
    }
}