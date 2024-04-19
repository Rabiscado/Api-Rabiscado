namespace Rabiscado.Application.Dtos.V1.Auth;

public class ChangePasswordUserDto
{
    public string Email { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string ConfirmNewPassword { get; set; } = null!;
    public Guid? TokenRecoverPassword { get; set; }
}