using FluentValidation;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Domain.Validators;

public class StepValidator : AbstractValidator<Step>
{
    public StepValidator()
    {
        RuleFor(x => x.Url)
            .MaximumLength(500)
            .WithMessage("Url must be less than 500 characters.")
            .NotEmpty()
            .WithMessage("Url is required.");

        RuleFor(x => x.ClassId)
            .GreaterThan(1)
            .WithMessage("ClassId is required.");
    }
}