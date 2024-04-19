namespace Rabiscado.Core.Settings;

public class AzureStorageSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string ContainerName { get; set; } = string.Empty;
    public string ContainerBodyAssestmentImages { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
}