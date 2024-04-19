namespace Rabiscado.Domain.Entities;

public class CourseLevel : Entity
{
    public int CourseId { get; set; }
    public int LevelId { get; set; }
    public Course Course { get; set; } = null!;
    public Level Level { get; set; } = null!;
}