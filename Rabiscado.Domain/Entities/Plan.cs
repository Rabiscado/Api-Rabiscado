using FluentValidation.Results;
using Rabiscado.Domain.Contracts;
using Rabiscado.Domain.Validators;

namespace Rabiscado.Domain.Entities;

public class Plan : Entity, ISoftDelete, IAggregateRoot
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal CoinValue { get; set; }
    public bool Disabled { get; set; }
    public List<UserPlanSubscription> UserPlanSubscriptions { get; set; } = new();
    public List<ExtractReceipt> ExtractReceipts { get; set; } = new();
    public List<User> Users { get; set; } = new();
    
    public override bool Validate(out ValidationResult validationResult)
    {
        validationResult = new PlanValidator().Validate(this);
        return validationResult.IsValid;
    }
}