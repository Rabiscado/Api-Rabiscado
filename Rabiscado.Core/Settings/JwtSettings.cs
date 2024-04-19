namespace Rabiscado.Core.Settings;

public class JwtSettings
{
    public int ExpirationHours { get; set; }
    public string Issuer { get; set; } = string.Empty;
    public string CommonValidIn { get; set; } = string.Empty;

    public List<string> Audiences()
    {
        return new List<string> { CommonValidIn };
    }
}