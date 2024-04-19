using FluentValidation.Results;
using Rabiscado.Application.Notifications.CustomerEntitiesError;

namespace Rabiscado.Application.Notifications;

public class Notificator : INotificator
{
    private readonly List<string> _erros;
    private bool _isNotFoundResource;

    public Notificator()
    {
        _erros = new List<string>();
    }

    public bool HasNotification => _erros.Any();
    public bool IsNotFoundResource => _isNotFoundResource;

    public void Handle(string message)
    {
        if (_isNotFoundResource)
            throw new InvalidOperationException("Cannot call Handle when there are NotFoundResouce!");
        _erros.Add(message);
    }

    public void Handle(List<ValidationFailure> failures)
    {
        failures.ForEach(c => Handle(c.ErrorMessage));
    }

    public void Handle(AssasError assasError)
    {
        assasError.Errors.ForEach(c => Handle($"Assas error: {c.Code} - {c.Description}"));
    }

    public void HandleNotFoundResourse()
    {
        if (HasNotification)
            throw new InvalidOperationException("Cannot call HandleNotFoundResource when there are notifications!");
        
        _isNotFoundResource = true;
    }

    public IEnumerable<string> GetNotifications() => _erros;
}