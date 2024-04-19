namespace Rabiscado.Application.Dtos.V1.Users;

public class CreateUserDto
{
    public string Name { get; set; } = null!;
    public string Cpf { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Cep { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int? PlanId { get; set; }
}