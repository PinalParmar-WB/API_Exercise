using API_Exercise.DTO;
using FluentValidation;

namespace API_Exercise.Validators
{
    public class InvoiceValidator : AbstractValidator<InvoiceInputDTO>
    {
        public InvoiceValidator()
        {
            RuleFor(p => p.InvoiceDate).NotEmpty().NotNull();
            RuleFor(p => p.PartyId).NotEmpty().NotNull();
            RuleFor(p => p.ProductId).NotEmpty().NotNull();
            RuleFor(p => p.Quantity).NotEmpty().NotNull().GreaterThan(0).LessThanOrEqualTo(100000);
            RuleFor(p => p.TotalAmount).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
