using Customers.API.Models;
using FluentValidation;

namespace Customers.API.Validators
{
    public class CustomerInputModelValidator : AbstractValidator<CustomerInputModel>
    {
        public CustomerInputModelValidator()
        {
            RuleForEach(inputModel => inputModel.Customers).SetValidator(new CustomerValidator());
        }
    }
}
