using FluentValidation.Results;
using Rabiscado.Domain.Contracts;
using Rabiscado.Domain.Validators;

namespace Rabiscado.Domain.Entities;

public class Module : Entity, ISoftDelete, IAggregateRoot
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool Disabled { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;
    public List<Class> Classes { get; set; } = new();

    public override bool Validate(out ValidationResult validationResult)
    {
        validationResult = new ModuleValidator().Validate(this);
        return validationResult.IsValid;
    }
}