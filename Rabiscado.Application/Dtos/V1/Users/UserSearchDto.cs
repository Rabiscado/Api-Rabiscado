using Rabiscado.Application.Dtos.V1.Base;
using Rabiscado.Core.Extensions;

namespace Rabiscado.Application.Dtos.V1.Users;

public class UserSearchDto : SearchPaginatedDto<Domain.Entities.User>
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Cpf { get; set; }
    public string? Phone { get; set; }
    public bool? Disabled { get; set; }
    public bool? IsAdmin { get; set; }
    public bool? IsProfessor { get; set; }

    public override void ApplyFilter(ref IQueryable<Domain.Entities.User> query)
    {
        if (!string.IsNullOrWhiteSpace(Name))
        {
            query = query.Where(u => u.Name.Contains(Name));
        }
        
        if (!string.IsNullOrWhiteSpace(Email))
        {
            query = query.Where(u => u.Email.Contains(Email));
        }
        
        if (!string.IsNullOrWhiteSpace(Cpf))
        {
            query = query.Where(u => u.Cpf.Contains(Cpf));
        }
        
        if (!string.IsNullOrWhiteSpace(Phone))
        {
            query = query.Where(u => u.Phone.Contains(Phone.OnlyNumbers()!));
        }

        if (Disabled.HasValue)
        {
            query = query.Where(u => u.Disabled);
        }
        
        if (IsAdmin.HasValue)
        {
            query = query.Where(u => u.IsAdmin);
        }
        
        if (IsProfessor.HasValue)
        {
            query = query.Where(u => u.IsProfessor);
        }
    }
    
    public override void ApplyOrdernation(ref IQueryable<Domain.Entities.User> query)
    {
        if (OrderDirection.EqualsIgnoreCase(""))
        {
            query = OrderBy switch
            {
                "Name" => query.OrderByDescending(c => c.Name),
                "Email" => query.OrderByDescending(c => c.Email),
                "Cpf" => query.OrderByDescending(c => c.Cpf),
                "Phone" => query.OrderByDescending(c => c.Phone),
                "IsAdmin" => query.OrderByDescending(c => c.IsAdmin),
                "IsProfessor" => query.OrderByDescending(c => c.IsProfessor),
                "Disabled" => query.OrderByDescending(c => c.Disabled),
                "Name" or _ => query.OrderByDescending(c => c.Name)
            };
            
            return;
        }
        
        query = OrderBy switch
        {
            "Name" => query.OrderBy(c => c.Name),
            "Email" => query.OrderBy(c => c.Email),
            "Cpf" => query.OrderBy(c => c.Cpf),
            "Phone" => query.OrderBy(c => c.Phone),
            "IsAdmin" => query.OrderBy(c => c.IsAdmin),
            "IsProfessor" => query.OrderBy(c => c.IsProfessor),
            "Disabled" => query.OrderBy(c => c.Disabled),
            "Name" or _ => query.OrderBy(c => c.Name)
        };
    }
}