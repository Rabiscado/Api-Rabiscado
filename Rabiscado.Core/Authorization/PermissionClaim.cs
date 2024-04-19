namespace Rabiscado.Core.Authorization;

public class PermissionClaim
{
    public string Nome { get; set; } = null!;
    public string Tipo { get; set; } = null!;

    public const string Separador = ":";
    
    public override string ToString() => $"{Nome}{Separador}{Tipo}";

    public static bool Verify(string claim, string nome, string tipo)
    {
        var parts = claim.Split(Separador);
        var claimNome = (string)(parts.GetValue(0) ?? string.Empty);
        var claimTipo = (string)(parts.GetValue(1) ?? string.Empty);

        return claimNome == nome && claimTipo.Contains(tipo);
    }
}