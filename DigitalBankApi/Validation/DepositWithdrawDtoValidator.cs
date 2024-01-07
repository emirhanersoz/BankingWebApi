using DigitalBankApi.DTOs;
using FluentValidation;

namespace DigitalBankApi.Validation
{
    public class DepositWithdrawDtoValidator : AbstractValidator<DepositWithdrawDto>, IValidator<DepositWithdrawDto>
    {
        public DepositWithdrawDtoValidator() 
        {
            RuleFor(DepositWithdraw => DepositWithdraw.AccountId)
                .NotEmpty().WithMessage("AccountId is required.");

            RuleFor(DepositWithdraw => DepositWithdraw.Amount)
                    .NotEmpty().WithMessage("Amount email address.")
                    .GreaterThanOrEqualTo(0).WithMessage("Amount must be non-negative.");

            RuleFor(DepositWithdraw => DepositWithdraw.TransactionType)
                    .NotEmpty().WithMessage("Transaction type is required.");

            RuleFor(customer => customer.isSucceded)
                    .NotEmpty().WithMessage("is Succeded is required.");  
        }
    }
}
