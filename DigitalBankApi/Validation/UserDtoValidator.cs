using DigitalBankApi.DTOs;
using DigitalBankApi.Models;
using FluentValidation;

namespace DigitalBankApi.Validation
{
    public class UserDtoValidator : AbstractValidator<UserDto>, IValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(user => user.IdentificationNumber).NotEmpty().WithMessage("Identification number is required.")
                                                  .MinimumLength(11).WithMessage("Identification number must be 11 numbers")
                                                  .MaximumLength(11).WithMessage("Identification number must be 11 numbers");
            RuleFor(user => user.Password).NotEmpty().WithMessage("Password is required.")
                                                    .MinimumLength(5).WithMessage("Password must be at least 5 characters.")
                                                    .MaximumLength(20).WithMessage("Password can't exceed 20 characters.");
        }
    }
}
