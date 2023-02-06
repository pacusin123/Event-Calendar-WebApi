using Event_Calendar_WebApi.Models;
using FluentValidation;

namespace Event_Calendar_WebApi.Business.Validations
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("First name cannot be empty");
            RuleFor(p => p.LastName).NotEmpty().WithMessage("Last name cannot be empty");
            RuleFor(p=> p.Email).EmailAddress().WithMessage("The email format is incorrect");
            RuleFor(p => p.UserName).NotEmpty().WithMessage("Last name cannot be empty")
                .MaximumLength(20).WithMessage("UserName not longer than 20 characters")
                .Matches(@"^[a-zA-Z]+$").WithMessage("UserName only can contain letters");
        }
    }
}
