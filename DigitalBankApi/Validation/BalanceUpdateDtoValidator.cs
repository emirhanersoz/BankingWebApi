using DigitalBankApi.DTOs;
using FluentValidation;

namespace DigitalBankApi.Validator
{
    public class BalanceUpdateDtoValidator : AbstractValidator<BalanceUpdateDto>, IValidator<BalanceUpdateDto>
    {
        public BalanceUpdateDtoValidator()
        {
            RuleFor(account => account.AccountType).NotEmpty().WithMessage("Account type is required.")
                                                    .IsInEnum().WithMessage("Invalid account type.");
            RuleFor(account => account.Balance).NotEmpty().WithMessage("Balance is required.")
                                               .GreaterThanOrEqualTo(0).WithMessage("Balance must be non-negative.");
        }
    }
}
