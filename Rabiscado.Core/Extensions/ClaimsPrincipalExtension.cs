using System.Security.Claims;
using Rabiscado.Core.Authorization;

namespace Rabiscado.Core.Extensions;

public static class ClaimsPrincipalExtension
{
    public static bool VerifyPermission(this ClaimsPrincipal? user, string claimName, string claimValue)
    {
        if (user is null)
        {
            return false;
        }
        
        return user
            .Claims
            .Where(p => p.Type == "permissions")
            .Any(p => PermissionClaim.Verify(p.Value, claimName, claimValue));
    }
    public static bool AuthenticatedUser(this ClaimsPrincipal? principal)
    {
        return principal?.Identity?.IsAuthenticated ?? false;
    }

    public static string? GetUserId(this ClaimsPrincipal? principal) => GetClaim(principal, ClaimTypes.NameIdentifier);
    public static string? GetUserName(this ClaimsPrincipal? principal) => GetClaim(principal, ClaimTypes.Name);
    public static string? GetUserEmail(this ClaimsPrincipal? principal) => GetClaim(principal, ClaimTypes.Email);
    public static string? GetProfessorType(this ClaimsPrincipal? principal) => GetClaim(principal, "ProfessorType");
    public static string? GetAdminType(this ClaimsPrincipal? principal) => GetClaim(principal, "AdminType");

    private static string? GetClaim(ClaimsPrincipal? principal, string claimName)
    {
        if (principal == null)
        {
            throw new ArgumentException(null, nameof(principal));
        }

        var claim = principal.FindFirst(claimName);
        return claim?.Value;
    }
}