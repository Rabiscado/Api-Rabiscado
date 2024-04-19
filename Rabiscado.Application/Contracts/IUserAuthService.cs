using Rabiscado.Application.Dtos.V1.Auth;

namespace Rabiscado.Application.Contracts;

public interface IUserAuthService
{
    Task<TokenDto?> Login(LoginDto loginDto);
    Task SendEmailRecoverPassword(string email);
    Task ChangePassword(ChangePasswordUserDto dto);
    Task ChangePassword(ChangePasswordAuthenticatedUserDto dto);
}