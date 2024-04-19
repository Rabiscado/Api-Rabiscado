using FluentValidation.Results;
using Rabiscado.Domain.Contracts;
using Rabiscado.Domain.Validators;

namespace Rabiscado.Domain.Entities;

public class Scheduledpayment : Entity, ISoftDelete, IAggregateRoot
{
    public decimal Value { get; set; }
    public bool PaidOut { get; set; }
    public int UserId { get; set; }
    public int CourseId { get; set; }
    public User User { get; set; } = null!;
    public Course Course { get; set; } = null!;
    public bool Disabled { get; set; }
    
    public override bool Validate(out ValidationResult validationResult)
    {
        validationResult = new ScheduledpaymentValidator().Validate(this);
        return validationResult.IsValid;
    }
}