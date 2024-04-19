using Microsoft.AspNetCore.Http;
using Rabiscado.Core.Extensions;

namespace Rabiscado.Core.Authorization.AuthenticatedUser;

public class AuthenticatedUser : IAuthenticatedUser
{
    public AuthenticatedUser() { }
    public AuthenticatedUser(IHttpContextAccessor httpContextAccessor)
    {
        Id = httpContextAccessor.GetUserId()!.Value;
        Name = httpContextAccessor.GetUserName();
        Email = httpContextAccessor.GetUserEmail();
        IsAdminUser = httpContextAccessor.GetAdminType()!.Value;
        IsProfessorUser = httpContextAccessor.GetProfessorType()!.Value;
    }
    public int Id { get; } = -1;
    public string Name { get; } = string.Empty;
    public string Email { get; } = string.Empty;
    public bool IsLoggedUser => Id > 0;
    public bool CommonUser => !IsAdminUser && !IsProfessorUser;
    public bool AdminUser => IsAdminUser;
    public bool ProfessorUser => IsProfessorUser;
    public bool IsAdminUser { get; }
    public bool IsProfessorUser { get; }
}