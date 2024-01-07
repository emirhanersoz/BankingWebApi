using DigitalBankApi.Dtos;
using DigitalBankApi.DTOs;
using FluentValidation;
using System.Text.RegularExpressions;

namespace DigitalBankApi.Validation
{
    public class CustomerDtoValidator : AbstractValidator<CustomerDto>, IValidator<CustomerDto>
    {
        public CustomerDtoValidator()
        {
            RuleFor(customer => customer.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(2).WithMessage("Name must be at least 2 characters.")
                .MaximumLength(50).WithMessage("Name can't exceed 50 characters.");

            RuleFor(customer => customer.PhoneNumber)
                .NotEmpty().WithMessage("PhoneNumber is required.")
                .MaximumLength(20).WithMessage("Phone number can't exceed 20 numbers.");

            RuleFor(customer => customer.Email)
                .NotEmpty().EmailAddress().WithMessage("Invalid email address.")
                .MaximumLength(50).WithMessage("Email can't exceed 50 characters")
                .EmailAddress().WithMessage("Email must be in e-mail format");

            RuleFor(customer => customer.DateOfBirth)
                .NotEmpty().WithMessage("Birthday is required.")
                .Must(BeAValidDate).WithMessage("Invalid DateOfBirth.");

            RuleFor(customer => customer.City)
                .NotEmpty().WithMessage("City is required.")
                .MinimumLength(2).WithMessage("City must be at least 2 characters.")
                .MaximumLength(30).WithMessage("City can't exceed 30 characters.");

            RuleFor(customer => customer.State)
                .NotEmpty().WithMessage("State is required.")
                .MinimumLength(2).WithMessage("State must be at least 2 characters.")
                .MaximumLength(30).WithMessage("State can't exceed 30 characters.");

            RuleFor(customer => customer.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MinimumLength(10).WithMessage("Address must be at least 10 characters.")
                .MaximumLength(100).WithMessage("Address can't exceed 100 characters.");

            RuleFor(customer => customer.PostCode)
                .NotEmpty().WithMessage("Postal code is required.")
                .MinimumLength(2).WithMessage("Postal code must be at least 2 numbers.")
                .MaximumLength(5).WithMessage("Postal code can't exceed 5 numbers.");
        }

        private bool BeAValidDate(DateTime date)
        {
            return date < DateTime.Now;
        }
    }
}
