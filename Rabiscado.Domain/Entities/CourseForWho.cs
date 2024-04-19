namespace Rabiscado.Domain.Entities;

public class CourseForWho : Entity
{
    public int CourseId { get; set; }
    public int ForWhoId { get; set; }
    public Course Course { get; set; } = null!;
    public ForWho ForWho { get; set; } = null!;
}