using DigitalBankApi.DTOs;
using FluentValidation;

namespace DigitalBankApi.Validation
{
    public class UpdatePasswordDtoValidator : AbstractValidator<UpdatePasswordDto>, IValidator<UpdatePasswordDto>
    {
        public UpdatePasswordDtoValidator()
        {
            RuleFor(user => user.Password).NotEmpty().WithMessage("Password is required.")
                                                    .MinimumLength(5).WithMessage("Password must be at least 5 characters.")
                                                    .MaximumLength(30).WithMessage("Password can't exceed 30 characters.");
        }
    }
}
