using FluentValidation;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Domain.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(c => c.Name)
            .Length(3, 255)
            .WithMessage("Name must be between 3 and 255 characters");

        RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(c => c.Cpf)
            .Length(11, 14)
            .WithMessage("CPF must be between 11 and 14 characters");

        RuleFor(c => c.Phone)
            .Length(9, 14)
            .WithMessage("Phone must be between 9 and 14 characters");

        RuleFor(c => c.Cep)
            .Length(8, 9)
            .WithMessage("CEP must be between 8 and 9 characters");

        RuleFor(c => c.Password)
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters long");
        
        RuleFor(c => c.Coin)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Coin must be greater than or equal to 0");
    }
}