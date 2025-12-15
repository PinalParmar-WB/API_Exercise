using API_Exercise.DTO;
using FluentValidation;

namespace API_Exercise.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterRequestDTO>
    {
        public RegisterValidator()
        {
            RuleFor(p => p.Email).NotEmpty().EmailAddress().WithMessage("Email should be in proper format");
            RuleFor(p => p.Password).NotEmpty();
            RuleFor(p => p.ConfirmPassword).NotEmpty();
        }
    }
}
