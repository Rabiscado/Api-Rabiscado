using FluentValidation.Results;
using Rabiscado.Domain.Contracts;

namespace Rabiscado.Domain.Entities;

public class Entity : BaseEntity, ITracking 
{
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
    
    public virtual bool Validate(out ValidationResult validationResult)
    {
        validationResult = new ValidationResult();
        return validationResult.IsValid;
    }
}