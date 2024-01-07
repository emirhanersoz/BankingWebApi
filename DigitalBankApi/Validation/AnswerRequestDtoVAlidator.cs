using DigitalBankApi.DTOs;
using FluentValidation;

namespace DigitalBankApi.Validation
{
    public class AnswerRequestDtoVAlidator : AbstractValidator<AnswerRequestDto>, IValidator<AnswerRequestDto>
    {
        public AnswerRequestDtoVAlidator()
        {
            RuleFor(answerRequest => answerRequest.CustomerId).NotEmpty().WithMessage("CustomerId is required.")
                                                  .GreaterThan(0).WithMessage("CustomerId must be greater than 0.");

            RuleFor(answerRequest => answerRequest.Subject).NotEmpty().WithMessage("Subject is required.")
                                                  .MinimumLength(3).WithMessage("Subject can't be less than 3 characters")
                                                  .MaximumLength(50).WithMessage("Subject can'exceed 50 characters");

            RuleFor(answerRequest => answerRequest.Description).NotEmpty().WithMessage("Description is required.")
                                                  .MinimumLength(3).WithMessage("Description can't be less than 3 characters")
                                                  .MaximumLength(200).WithMessage("Description can'exceed 200 characters");

            RuleFor(answerRequest => answerRequest.Answer)
                                                  .MinimumLength(3).WithMessage("Answer can't be less than 3 characters")
                                                  .MaximumLength(200).WithMessage("Answer can'exceed 200 characters");
        }
    }
}
