namespace Rabiscado.Core.Utils;

public static class StringUtils
{
    public static string GenerateSixDigitToken()
    {
        return new Random().Next(10000, 999999).ToString("D6");
    }
}