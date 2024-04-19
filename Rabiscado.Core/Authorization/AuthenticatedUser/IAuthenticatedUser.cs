namespace Rabiscado.Core.Authorization.AuthenticatedUser;

public interface IAuthenticatedUser
{
    public int Id { get; }
    public string Name { get; }
    public string Email { get; }
    public bool IsLoggedUser { get; }
    public bool IsAdminUser { get; }
    public bool IsProfessorUser { get; }
    public bool CommonUser { get; }
    public bool AdminUser { get; }
    public bool ProfessorUser { get; }
}