using DigitalBankApi.DTOs;
using FluentValidation;

namespace DigitalBankApi.Validation
{
    public class PayeeDtoValidator : AbstractValidator<PayeeDto>, IValidator<PayeeDto>
    {
        public PayeeDtoValidator() 
        {
            RuleFor(payee => payee.PayeeType)
                    .NotEmpty().WithMessage("Payee Type can't be left blank")
                    .IsInEnum().WithMessage("Invalid Payee Type.");

            RuleFor(payee => payee.Amount)
                    .NotEmpty().WithMessage("Amount can't be left blank")
                    .GreaterThan(0).WithMessage("Amount must be greater than 0.");
        }
    }
}
