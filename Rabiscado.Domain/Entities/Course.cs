using FluentValidation.Results;
using Rabiscado.Domain.Contracts;
using Rabiscado.Domain.Validators;

namespace Rabiscado.Domain.Entities;

public class Course : Entity, ISoftDelete, IAggregateRoot
{
    public string Name { get; set; } = null!;
    public string ProfessorEmail { get; set; } = null!;
    public string? Description { get; set; }
    public string? Image { get; set; }
    public string? Video { get; set; }
    public decimal Value { get; set; }
    public string? Style { get; set; }
    public string? School { get; set; }
    public string? Localization { get; set; }
    public bool Disabled { get; set; }
    
    public List<CourseLevel> CourseLevels { get; set; } = new();
    public List<CourseForWho> CourseForWhos { get; set; } = new();
    public List<Subscription> Subscriptions { get; set; } = new();
    public List<Module> Modules { get; set; } = new();
    public List<Extract> Extracts { get; set; } = new();
    public List<Scheduledpayment> Scheduledpayments { get; set; } = new();
    
    public override bool Validate(out ValidationResult validationResult)
    {
        validationResult = new CourseValidator().Validate(this);
        return validationResult.IsValid;
    }
}