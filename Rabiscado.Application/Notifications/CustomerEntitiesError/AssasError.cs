namespace Rabiscado.Application.Notifications.CustomerEntitiesError;

public class AssasError
{
    public List<Error> Errors { get; set; } = new ();
}

public class Error
{
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;
}