using Customers.API.Model;
using FluentValidation;

namespace Customers.API.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.Id).NotEmpty().NotNull();
            RuleFor(customer => customer.FirstName).NotEmpty().NotNull();
            RuleFor(customer => customer.LastName).NotEmpty().NotNull();

            RuleFor(customer => customer.Age)
                .GreaterThan(18)
                .NotEmpty()
                .NotNull();
        }
    }
}
