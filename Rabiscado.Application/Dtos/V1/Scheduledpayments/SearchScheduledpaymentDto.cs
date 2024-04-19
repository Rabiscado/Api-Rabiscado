using Rabiscado.Application.Dtos.V1.Base;
using Rabiscado.Core.Extensions;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Application.Dtos.V1.Scheduledpayments;

public class SearchScheduledpaymentDto : SearchPaginatedDto<Scheduledpayment>
{
    public string? Professor { get; set; }
    public bool? PaidOut { get; set; }
    public int? UserId { get; set; }
    public bool? Disabled { get; set; }

    public override void ApplyFilter(ref IQueryable<Scheduledpayment> query)
    {
        if (!string.IsNullOrWhiteSpace(Professor))
        {
            query = query.Where(x => x.User.Email.Contains(Professor));
        }
        
        if (PaidOut.HasValue)
        {
            query = query.Where(x => x.PaidOut == PaidOut.Value);
        }
        
        if (UserId.HasValue)
        {
            query = query.Where(x => x.UserId == UserId.Value);
        }
        
        if (Disabled.HasValue)
        {
            query = query.Where(x => x.User.Disabled == Disabled.Value);
        }
    }

    public override void ApplyOrdernation(ref IQueryable<Scheduledpayment> query)
    {
        if (OrderDirection.EqualsIgnoreCase("desc"))
        {
            query = OrderBy switch
            {
                "professor" => query.OrderByDescending(x => x.User.Email),
                "paidOut" => query.OrderByDescending(x => x.PaidOut),
                "userId" => query.OrderByDescending(x => x.UserId),
                _ => query.OrderByDescending(x => x.Id)
            };
            
            return;
        }
        
        query = OrderBy switch
        {
            "professor" => query.OrderBy(x => x.User.Email),
            "paidOut" => query.OrderBy(x => x.PaidOut),
            "userId" => query.OrderBy(x => x.UserId),
            _ => query.OrderBy(x => x.Id)
        };
    }
}