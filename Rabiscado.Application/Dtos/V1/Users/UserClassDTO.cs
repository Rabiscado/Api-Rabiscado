namespace Rabiscado.Application.Dtos.V1.User;

public class UserClassDTO
{
    public int UserId { get; set; }
    public int ClassId { get; set; } 
    public bool Watched { get; set; }
    public bool Disabled { get; set; }
}