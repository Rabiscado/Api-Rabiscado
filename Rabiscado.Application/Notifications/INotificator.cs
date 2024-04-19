using FluentValidation.Results;
using Rabiscado.Application.Notifications.CustomerEntitiesError;

namespace Rabiscado.Application.Notifications;

public interface INotificator
{
    bool HasNotification { get; }
    bool IsNotFoundResource { get; }
    public void Handle(string message);
    public void Handle(List<ValidationFailure> failures);
    public void Handle(AssasError assasError);
    public void HandleNotFoundResourse();
    public IEnumerable<string> GetNotifications();
}