using Rabiscado.Core.Enums;

namespace Rabiscado.Application.Dtos.V1.Extracts;

public class CreateExtractDto
{
    public EExtractType Type { get; set; }
    public int UserId { get; set; }
    public int ProfessorId { get; set; }
    public int CourseId { get; set; }
}