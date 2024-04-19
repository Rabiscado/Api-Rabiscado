using FluentValidation.Results;
using Rabiscado.Domain.Contracts;
using Rabiscado.Domain.Validators;

namespace Rabiscado.Domain.Entities;

public class Extract : Entity, ISoftDelete, IAggregateRoot
{
    public decimal Value { get; set; }
    public int Type { get; set; }
    public int UserId { get; set; }
    public int ProfessorId { get; set; }
    public int CourseId { get; set; }
    public User User { get; set; } = null!;
    public User Professor { get; set; } = null!;
    public Course Course { get; set; } = null!;
    public bool Disabled { get; set; }

    public override bool Validate(out ValidationResult validationResult)
    {
        var validator = new ExtractValidator();
        validationResult = validator.Validate(this);
        return validationResult.IsValid;
    }
}