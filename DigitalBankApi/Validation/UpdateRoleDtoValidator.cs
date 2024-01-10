using DigitalBankApi.DTOs;
using FluentValidation;

namespace DigitalBankApi.Validation
{
    public class UpdateRoleDtoValidator : AbstractValidator<UpdateRoleDto>, IValidator<UpdateRoleDto>
    {
        public UpdateRoleDtoValidator()
        {
            RuleFor(updateRole => updateRole.Role)
                .NotEmpty().WithMessage("Role is required.")
                .Must(IsRoleValid).WithMessage("Invalid role.");
        }

        private bool IsRoleValid(string role)
        {
            string[] roles = { "Admin", "Employee", "HighLevelUser", "User" };
            return roles.Contains(role);
        }
    }
}
