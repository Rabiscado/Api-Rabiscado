using Rabiscado.Application.Dtos.V1.Courses;
using Rabiscado.Application.Dtos.V1.User;
using Rabiscado.Application.Dtos.V1.Users;
using Rabiscado.Core.Enums;

namespace Rabiscado.Application.Dtos.V1.Extracts;

public class ExtractDto
{
    public int Id { get; set; }
    public EExtractType Type { get; set; }
    public DateTime CreateAt { get; set; }
    public int UserId { get; set; }
    public UserDto User { get; set; } = null!;
    public int ProfessorId { get; set; }
    public UserDto Professor { get; set; } = null!;
    public int CourseId { get; set; }
    public CourseDto Course { get; set; } = null!;
}