using Microsoft.AspNetCore.Http;

namespace Rabiscado.Core.Extensions;

public static class HttpContextAccessorExtension
{
    public static bool AuthenticatedUser(this IHttpContextAccessor? contextAccessor)
    {
        return contextAccessor?.HttpContext?.User.AuthenticatedUser() ?? false;
    }
    
    public static int? GetUserId(this IHttpContextAccessor? contextAccessor)
    {
        var id = contextAccessor?.HttpContext?.User.GetUserId() ?? string.Empty;
        return string.IsNullOrWhiteSpace(id) ? null : int.Parse(id);
    }
    
    public static string GetUserName(this IHttpContextAccessor? contextAccessor)
    {
        var name = contextAccessor?.HttpContext?.User.GetUserName() ?? string.Empty;
        return string.IsNullOrWhiteSpace(name) ? string.Empty : name;
    }
    
    public static string GetUserEmail(this IHttpContextAccessor? contextAccessor)
    {
        var email = contextAccessor?.HttpContext?.User.GetUserEmail() ?? string.Empty;
        return string.IsNullOrWhiteSpace(email) ? string.Empty : email;
    }
    
    public static bool? GetProfessorType(this IHttpContextAccessor? contextAccessor)
    {
        var professorType = contextAccessor?.HttpContext?.User.GetProfessorType() ?? string.Empty;
        return string.IsNullOrWhiteSpace(professorType) ? null : bool.Parse(professorType);
    }
    
    public static bool? GetAdminType(this IHttpContextAccessor? contextAccessor)
    {
        var adminType = contextAccessor?.HttpContext?.User.GetAdminType() ?? string.Empty;
        return string.IsNullOrWhiteSpace(adminType) ? null : bool.Parse(adminType);
    }
}