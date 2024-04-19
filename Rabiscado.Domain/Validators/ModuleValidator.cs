using FluentValidation;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Domain.Validators;

public class ModuleValidator : AbstractValidator<Module>
{
    public ModuleValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(3, 100)
            .WithMessage("Name must be between 3 and 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(500)
            .WithMessage("Description must be less than 500 characters.");

        RuleFor(x => x.CourseId)
            .GreaterThan(0)
            .WithMessage("CourseId must be greater than 0.")
            .NotEmpty()
            .WithMessage("CourseId must be informed.");
    }
}