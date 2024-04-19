using FluentValidation;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Domain.Validators;

public class PlanValidator : AbstractValidator<Plan>
{
    public PlanValidator()
    {
        RuleFor(x => x.Name)
            .Length(3, 100)
            .WithMessage("Name must be between 3 and 100 characters.");
        
        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage("Description must be less than 500 characters.");
        
        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0.");
        
        RuleFor(x => x.CoinValue)
            .GreaterThan(0)
            .WithMessage("CoinValue must be greater than 0.");
    }
}