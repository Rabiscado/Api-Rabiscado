using FluentValidation.Results;
using Rabiscado.Domain.Contracts;
using Rabiscado.Domain.Validators;

namespace Rabiscado.Domain.Entities;

public class Class : Entity, ISoftDelete, IAggregateRoot
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Video { get; set; }
    public string? Music { get; set; }
    public string? Tumb { get; set; }
    public string? Gif { get; set; }
    public int ModuleId { get; set; }
    public bool Disabled { get; set; }
    
    public Module Module { get; set; } = null!;
    public List<Step> Steps { get; set; } = new();
    public List<UserClass> UserClasses { get; set; } = new();

    public override bool Validate(out ValidationResult validationResult)
    {
        validationResult = new ClassValidator().Validate(this);
        return validationResult.IsValid;
    }
}