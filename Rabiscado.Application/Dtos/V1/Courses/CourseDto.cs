using Rabiscado.Application.Dtos.V1.ForWho;
using Rabiscado.Application.Dtos.V1.Level;
using Rabiscado.Application.Dtos.V1.Module;

namespace Rabiscado.Application.Dtos.V1.Courses;

public class CourseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ProfessorEmail { get; set; } = null!;
    public string? Description { get; set; }
    public string? Image { get; set; }
    public string? Video { get; set; }
    public decimal Value { get; set; }
    public string? Style { get; set; }
    public string? School { get; set; }
    public string? Localization { get; set; }
    public bool Disabled { get; set; }
    public int Subscribe { get; set; }
    public List<CourseForWhoDto> CourseForWhos { get; set; } = new();
    public List<CourseLevelDto> CourseLevels { get; set; } = new();
    public List<ModuleDto> Modules { get; set; } = new();
}

public class CourseForWhoDto
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int ForWhoId { get; set; }
    public CourseDto Course { get; set; } = null!;
    public ForWhoDto ForWho { get; set; } = null!;
}

public class CourseLevelDto
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int LevelId { get; set; }
    public CourseDto Course { get; set; } = null!;
    public LevelDto Level { get; set; } = null!;
}