using FluentValidation;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Domain.Validators;

public class ScheduledpaymentValidator : AbstractValidator<Scheduledpayment>
{
    public ScheduledpaymentValidator()
    {
        RuleFor(x => x.Value)
            .GreaterThanOrEqualTo(0).WithMessage("Value must be greater than or equal to 0")
            .NotEmpty().WithMessage("Value is required");
        
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required");
        
        RuleFor(x => x.CourseId)
            .NotEmpty().WithMessage("CourseId is required");

        RuleFor(x => x.Value)
            .GreaterThanOrEqualTo(0).WithMessage("Value must be greater than or equal to 0");
    }
}