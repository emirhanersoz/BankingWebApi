using DigitalBankApi.DTOs;
using FluentValidation;

namespace DigitalBankApi.Validation
{
    public class SupportRequestDtoValidator : AbstractValidator<SupportRequestDto>, IValidator<SupportRequestDto>
    {
        public SupportRequestDtoValidator()
        {
            RuleFor(supportRequest => supportRequest.Subject)
                .NotEmpty().WithMessage("Subject can't be left blank")
                .MinimumLength(3).WithMessage("Subject can't be less than 3 characters")
                .MaximumLength(50).WithMessage("Subject can'exceed 50 characters");

            RuleFor(supportRequest => supportRequest.Description)
                    .NotEmpty().WithMessage("Description can't be left blank")
                    .MinimumLength(3).WithMessage("Description can't be less than 3 characters")
                    .MaximumLength(100).WithMessage("Description can'exceed 100 characters");
        }
    }
}
