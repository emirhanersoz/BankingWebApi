using DigitalBankApi.DTOs;
using FluentValidation;

namespace DigitalBankApi.Validation
{
    public class MoneyTransferDtoValidator : AbstractValidator<MoneyTransferDto>, IValidator<MoneyTransferDto>
    {
        public MoneyTransferDtoValidator()
        {
            RuleFor(moneyTransfer => moneyTransfer.AccountId)
                    .NotEmpty().WithMessage("AccountId can't be left blank");

            RuleFor(moneyTransfer => moneyTransfer.DestAccountId)
                    .NotEmpty().WithMessage("AccountId can't be left blank");

            RuleFor(moneyTransfer => moneyTransfer.Amount)
                    .NotEmpty().WithMessage("Amount can't be left blank")
                    .GreaterThan(0).WithMessage("Amount must be greater than 0.");

            RuleFor(moneyTransfer => moneyTransfer.Comment)
                    .MaximumLength(50).WithMessage("Comment can't exceed 50 characters.");
                    
        }
    }
}
