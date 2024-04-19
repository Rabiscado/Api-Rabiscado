using FluentValidation;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Domain.Validators;

public class ExtractValidator : AbstractValidator<Extract>
{
    public ExtractValidator()
    {
        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Type is required");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required");
        
        RuleFor(x => x.CourseId)
            .NotEmpty().WithMessage("CourseId is required");
        
        RuleFor(x => x.ProfessorId)
            .NotEmpty().WithMessage("ProfessorId is required");
    }
}