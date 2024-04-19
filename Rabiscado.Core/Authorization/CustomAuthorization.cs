using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Rabiscado.Core.Extensions;

namespace Rabiscado.Core.Authorization;

public static class CustomAuthorization
{
    public static bool ValidateUserAdminType(HttpContext context, string claimValue)
    {
        var adminType = context.User.GetAdminType();
        return context.User.Identity!.IsAuthenticated && adminType?.ToLower() == claimValue.ToLower();
    }
    
    public static bool ValidateUserProfessorType(HttpContext context, string claimValue)
    {
        var professorType = context.User.GetProfessorType();
        return context.User.Identity!.IsAuthenticated && professorType?.ToLower() == claimValue.ToLower();
    }
}

public class ClaimsAuthorizeAttribute : TypeFilterAttribute
{
    public ClaimsAuthorizeAttribute(string claimName, bool claimValue = true) : base(typeof(RequirementClaimFilter))
    {
        Arguments = new object[] { new Claim(claimName, claimValue.ToString()) };
    }
}

public class RequirementClaimFilter : IAuthorizationFilter
{
    private readonly Claim _claim;

    public RequirementClaimFilter(Claim claim)
    {
        _claim = claim;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var hasError = false;
        if (_claim.Type == "AdminType")
        {
            hasError = !CustomAuthorization.ValidateUserAdminType(context.HttpContext, _claim.Value);
        }
        
        if (_claim.Type == "ProfessorType")
        {
            hasError = !CustomAuthorization.ValidateUserProfessorType(context.HttpContext, _claim.Value);
        }
        
        if (hasError)
        {
            context.Result = new StatusCodeResult(403);
            return;
        }
        
        if (!context.HttpContext.User.Identity!.IsAuthenticated)
        {
            context.Result = new StatusCodeResult(401);
        }
    }
}