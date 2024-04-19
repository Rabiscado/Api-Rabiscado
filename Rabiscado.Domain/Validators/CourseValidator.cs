using FluentValidation;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Domain.Validators;

public class CourseValidator : AbstractValidator<Course>
{
    public CourseValidator()
    {
        RuleFor(c => c.Name)
            .Length(3, 100)
            .WithMessage("Name must be between 3 and 100 characters");

        RuleFor(c => c.ProfessorEmail)
            .EmailAddress();
        
        RuleFor(c => c.Description)
            .MaximumLength(500)
            .WithMessage("Description must be less than 500 characters");
        
        RuleFor(c => c.Value)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Value must be greater than or equal to 0");
    }
}