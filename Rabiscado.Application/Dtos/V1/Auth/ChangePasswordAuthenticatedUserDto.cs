namespace Rabiscado.Application.Dtos.V1.Auth;

public class ChangePasswordAuthenticatedUserDto
{
    public string Password { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string ConfirmNewPassword { get; set; } = null!;
}