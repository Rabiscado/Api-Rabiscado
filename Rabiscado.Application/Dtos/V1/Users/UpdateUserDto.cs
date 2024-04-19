namespace Rabiscado.Application.Dtos.V1.Users;

public class UpdateUserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Cpf { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Cep { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool IsAdmin { get; set; }
    public bool IsProfessor { get; set; }
    public int? PlanId { get; set; }
}