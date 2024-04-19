using Microsoft.AspNetCore.Http;

namespace Rabiscado.Application.Dtos.V1.Courses;

public class CreateCourseDto
{
    public string Name { get; set; } = null!;
    public string ProfessorEmail { get; set; } = null!;
    public string? Description { get; set; }
    public IFormFile? Tumb { get; set; }
    public string? Video { get; set; }
    public decimal Value { get; set; }
    public string? Style { get; set; }
    public string? School { get; set; }
    public string? Localization { get; set; }
    public bool Disabled { get; set; }
    public List<int> LevelIds { get; set; } = new();
    public List<int> ForWhoIds { get; set; } = new();
}