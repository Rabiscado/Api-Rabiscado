﻿using Rabiscado.Core.Enums;

namespace Rabiscado.Application.Dtos.V1.Extracts;

public class UpdateExtractDto
{
    public int Id { get; set; }
    public EExtractType Type { get; set; }
    public int UserId { get; set; }
    public int ProfessorId { get; set; }
    public int CourseId { get; set; }
}