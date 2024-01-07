using DigitalBankApi.DTOs;
using FluentValidation;

namespace DigitalBankApi.Validation
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>, IValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(login => login.UserId)
                    .NotEmpty().WithMessage("UserId can't be left blank");
        }
    }
}
