using FluentValidation;

namespace Email.Application.Features.Orders.Commands.SendEmail
{
    public class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
    {
        public SendEmailCommandValidator()
        {
            RuleFor(p => p.OrderId)
                .NotEmpty().WithMessage("{OrderId} is required.")
                .NotNull();
            
            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("{UserName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{UserName} must not exceed 50 characters.");

            RuleFor(p => p.EmailAddress)
                .EmailAddress()
               .NotEmpty().WithMessage("{EmailAddress} is required.");

            RuleFor(p => p.Amount)
                .NotEmpty().WithMessage("{Amount} is required.")
                .GreaterThan(0).WithMessage("{Amount} should be greater than zero.");
        }
    }
}
