using API_Exercise.Models;
using FluentValidation;

namespace API_Exercise.Validators
{
    public class PartyValidator : AbstractValidator<Party>
    {
        public PartyValidator() {
            RuleFor(p => p.PartyName).NotEmpty().NotNull().WithMessage("Party Name must be required!!!");
        }
    }
}
