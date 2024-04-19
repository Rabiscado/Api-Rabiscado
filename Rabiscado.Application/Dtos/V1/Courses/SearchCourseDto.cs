using Rabiscado.Application.Dtos.V1.Base;
using Rabiscado.Core.Enums;
using Rabiscado.Core.Extensions;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Application.Dtos.V1.Courses;

public class SearchCourseDto : SearchPaginatedDto<Course>
{
    public string? Name { get; set; }
    public ELevel? Level { get; set; }
    public string? Localization { get; set; }
    public EForWho? ForWho { get; set; }
    public string? School { get; set; }
    public string? Style { get; set; }
    public int? UserId { get; set; }
    public bool? Disabled { get; set; }
    public string? Email { get; set; }

    public override void ApplyFilter(ref IQueryable<Course> query)
    {
        if (!string.IsNullOrWhiteSpace(Name))
        {
            query = query.Where(c => c.Name.Contains(Name));
        }

        if (!string.IsNullOrWhiteSpace(Email))
        {
            query = query.Where(c => c.ProfessorEmail.Contains(Email));
        }
        
        if (Level is > 0)
        {
            query = query.Where(c => c.CourseLevels.Any(cl => cl.LevelId == (int)Level));
        }

        if (!string.IsNullOrWhiteSpace(Localization))
        {
            query = query.Where(c => c.Localization.Contains(Localization));
        }

        if (ForWho is > 0)
        {
            query = query.Where(c => c.CourseForWhos.Any(cfw => cfw.ForWhoId == (int)ForWho));
        }

        if (!string.IsNullOrWhiteSpace(School))
        {
            query = query.Where(c => c.School.Contains(School));
        }

        if (!string.IsNullOrWhiteSpace(Style))
        {
            query = query.Where(c => c.Style.Contains(Style));
        }
        
        if (UserId.HasValue)
        {
            query = query.Where(c => c.Subscriptions.Any(s => s.UserId == UserId));
        }
        
        if (Disabled.HasValue)
        {
            query = query.Where(c => c.Disabled == Disabled);
        }
    }

    public override void ApplyOrdernation(ref IQueryable<Course> query)
    {
        if (OrderDirection.EqualsIgnoreCase(""))
        {
            query = OrderBy switch
            {
                "Level" => query.OrderByDescending(c => c.CourseLevels.FirstOrDefault()!.Level.Name),
                "ForWho" => query.OrderByDescending(c => c.CourseForWhos.FirstOrDefault()!.ForWho.Name),
                "School" => query.OrderByDescending(c => c.School),
                "Style" => query.OrderByDescending(c => c.Style),
                "UserId" => query.OrderByDescending(c => c.Subscriptions.Any(s => s.UserId == UserId)),
                "Disabled" => query.OrderByDescending(c => c.Disabled),
                "Name" or _ => query.OrderByDescending(c => c.Name),
            };
            
            return;
        }
        
        query = OrderBy switch
        {
            "Level" => query.OrderBy(c => c.CourseLevels.FirstOrDefault()!.Level.Name),
            "ForWho" => query.OrderBy(c => c.CourseForWhos.FirstOrDefault()!.ForWho.Name),
            "School" => query.OrderBy(c => c.School),
            "Style" => query.OrderBy(c => c.Style),
            "UserId" => query.OrderBy(c => c.Subscriptions.Any(s => s.UserId == UserId)),
            "Disabled" => query.OrderBy(c => c.Disabled),
            "Name" or _ => query.OrderBy(c => c.Name),
        };
    }
}