using Rabiscado.Application.Dtos.V1.Courses;
using Rabiscado.Application.Dtos.V1.User;
using Rabiscado.Application.Dtos.V1.Users;

namespace Rabiscado.Application.Dtos.V1.Subscription;

public class SubscriptionDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CourseId { get; set; }
    public DateTime? SubscriptionEnd { get; set; }
    public UserDto User { get; set; } = null!;
    public CourseDto Course { get; set; } = null!;
    public bool Disabled { get; set; }
    public bool IsHide { get; set; }
}