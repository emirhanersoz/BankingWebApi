using DigitalBankApi.DTOs;
using FluentValidation;

namespace DigitalBankApi.Validation
{
    public class AccountDtoValidator : AbstractValidator<AccountDto>, IValidator<AccountDto>
    {
        public AccountDtoValidator()
        {
            RuleFor(account => account.CustomerId).NotEmpty().WithMessage("CustomerId is required.")
                                                  .GreaterThan(0).WithMessage("CustomerId must be greater than 0.");
            RuleFor(account => account.AccountType).NotEmpty().WithMessage("AccountType is required.")
                                                    .IsInEnum().WithMessage("Invalid AccountType.");
            RuleFor(account => account.Balance).NotEmpty().WithMessage("Balance is required.")
                                               .GreaterThanOrEqualTo(0).WithMessage("Balance must be non-negative.");
            RuleFor(account => account.Salary).GreaterThanOrEqualTo(0).WithMessage("Salary must be non-negative.");
        }
    }
}
