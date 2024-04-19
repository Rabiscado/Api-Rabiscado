using FluentValidation;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Domain.Validators;

public class ClassValidator : AbstractValidator<Class>
{
    public ClassValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("The name cannot be empty")
            .Length(3, 255).WithMessage("The name must have between 3 and 255 characters");
        
        RuleFor(c => c.Description)
            .MaximumLength(500).WithMessage("The description must have a maximum of 500 characters");
        
        RuleFor(c => c.Video)
            .MaximumLength(500).WithMessage("The description must have a maximum of 500 characters");
        
        RuleFor(c => c.Music)
            .MaximumLength(500).WithMessage("The description must have a maximum of 500 characters");

        RuleFor(c => c.ModuleId)
            .NotEmpty().WithMessage("The moduleId cannot be empty")
            .GreaterThanOrEqualTo(0)
            .WithMessage("The module id must be greater than or equal to 0");
    }
}