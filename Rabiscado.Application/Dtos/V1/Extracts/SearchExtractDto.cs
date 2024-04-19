using Rabiscado.Application.Dtos.V1.Base;
using Rabiscado.Core.Enums;
using Rabiscado.Core.Extensions;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Application.Dtos.V1.Extracts;

public class SearchExtractDto : SearchPaginatedDto<Extract>
{
    public int? UserId { get; set; }
    public int? ProfessorId { get; set; }
    public int? CourseId { get; set; }
    public DateTime? Date { get; set; }
    public decimal? Value { get; set; }
    public EExtractType? Type { get; set; }
    public bool? Disabled { get; set; }

    public override void ApplyFilter(ref IQueryable<Extract> query)
    {
        if (UserId.HasValue)
            query = query.Where(e => e.UserId == UserId);

        if (Date.HasValue)
            query = query.Where(e => Date <= e.CreateAt);

        if (Value.HasValue)
            query = query.Where(e => e.Course.Value == Value);

        if (Type > 0)
            query = query.Where(e => e.Type == (int)Type);

        if (Disabled.HasValue)
            query = query.Where(e => e.Disabled == Disabled);
        
        if (ProfessorId.HasValue)
            query = query.Where(e => e.ProfessorId == ProfessorId);
        
        if (CourseId.HasValue)
            query = query.Where(e => e.CourseId == CourseId);
    }

    public override void ApplyOrdernation(ref IQueryable<Extract> query)
    {
        if (OrderDirection.EqualsIgnoreCase("desc"))
        {
            query = OrderBy switch
            {
                "userId" => query.OrderByDescending(e => e.UserId),
                "professorId" => query.OrderByDescending(e => e.ProfessorId),
                "date" => query.OrderByDescending(e => e.CreateAt),
                "value" => query.OrderByDescending(e => e.Course.Value),
                "type" => query.OrderByDescending(e => e.Type),
                "courseId" => query.OrderByDescending(e => e.CourseId),
                "disabled" => query.OrderByDescending(e => e.Disabled),
                _ => query.OrderByDescending(e => e.Id)
            };
            
            return;
        }
        
        query = OrderBy switch
        {
            "userId" => query.OrderBy(e => e.UserId),
            "professorId" => query.OrderBy(e => e.ProfessorId),
            "date" => query.OrderBy(e => e.CreateAt),
            "value" => query.OrderBy(e => e.Course.Value),
            "type" => query.OrderBy(e => e.Type),
            "courseId" => query.OrderBy(e => e.CourseId),
            "disabled" => query.OrderBy(e => e.Disabled),
            _ => query.OrderBy(e => e.Id)
        };
    }
}