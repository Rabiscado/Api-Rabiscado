namespace Rabiscado.Application.Dtos.V1.Auth;

public class VerifyTokenDto
{
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
}