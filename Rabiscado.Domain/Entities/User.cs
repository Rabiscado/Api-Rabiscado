using FluentValidation.Results;
using Rabiscado.Domain.Contracts;
using Rabiscado.Domain.Validators;

namespace Rabiscado.Domain.Entities;

public class User : Entity, ISoftDelete, IAggregateRoot
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Cpf { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Cep { get; set; } = null!;
    public decimal Coin { get; private set; }
    public bool Disabled { get; set; }
    public int? PlanId { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsProfessor { get; set; }
    public Guid? TokenRecoverPassword { get; set; }
    public DateTime? TokenExpires { get; set; }
    public Plan Plan { get; set; } = null!;
    public List<UserPlanSubscription> UserPlanSubscriptions { get; set; } = new();
    public List<Extract> Extracts { get; set; } = new();
    public List<Subscription> Subscriptions { get; set; } = new();
    public List<Scheduledpayment> Scheduledpayments { get; set; } = new();
    public List<Reimbursement> Reimbursements { get; set; } = new();
    public List<ExtractReceipt> ExtractReceipts { get; set; } = new();
    public List<UserClass> UserClasses { get; set; } = new();

    public override bool Validate(out ValidationResult validationResult)
    {
        validationResult = new UserValidator().Validate(this);
        return validationResult.IsValid;
    }
    
    public void AddCoin(decimal value)
    {
        Coin += value;
    }
    
    public void RemoveCoin(decimal value)
    {
        Coin -= value;
    }
}