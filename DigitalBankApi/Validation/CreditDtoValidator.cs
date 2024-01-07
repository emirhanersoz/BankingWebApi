using DigitalBankApi.DTOs;
using FluentValidation;

namespace DigitalBankApi.Validation
{
    public class CreditDtoValidator : AbstractValidator<CreditDto>, IValidator<CreditDto>
    {
        public CreditDtoValidator() 
        {
            RuleFor(credit => credit.TotalAmount)
                .NotEmpty().WithMessage("Total amount can't be left blank")
                .GreaterThan(0).WithMessage("Total amount must be greater than 0.");

            RuleFor(credit => credit.MontlyPayment)
                .NotEmpty().WithMessage("Montly payment amount can't be left blank")
                .GreaterThan(0).WithMessage("Montly payment must be greater than 0.");

            RuleFor(credit => credit.RepaymentPeriodMonths)
                .NotEmpty().WithMessage("Repayment period months can't be left blank")
                .GreaterThan(0).WithMessage("Repayment period months must be greater than 0.");
        }
    }
}
