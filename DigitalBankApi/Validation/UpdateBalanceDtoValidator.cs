using DigitalBankApi.DTOs;
using FluentValidation;

namespace DigitalBankApi.Validator
{
    public class UpdateBalanceDtoValidator : AbstractValidator<UpdateBalanceDto>, IValidator<UpdateBalanceDto>
    {
        public UpdateBalanceDtoValidator()
        {
            RuleFor(account => account.AccountType).NotEmpty().WithMessage("Account type is required.")
                                                    .IsInEnum().WithMessage("Invalid account type.");

            RuleFor(account => account.Balance).NotEmpty().WithMessage("Balance is required.")
                                               .GreaterThanOrEqualTo(1000).WithMessage("Balance must be greater than 1000.");

            RuleFor(account => account.Salary).NotEmpty().WithMessage("Salarya is required.")
                                               .GreaterThan(0).WithMessage("Salarya must be greater than 0.");
        }
    }
}
