using API_Exercise.Models;
using FluentValidation;

namespace API_Exercise.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator() { 
            RuleFor(p => p.ProductName).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(p => p.ProductRate).NotNull().GreaterThan(0).LessThanOrEqualTo(100000);
        }
    }
}
