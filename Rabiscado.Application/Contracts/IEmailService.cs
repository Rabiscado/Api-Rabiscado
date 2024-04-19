using Rabiscado.Domain.Entities;

namespace Rabiscado.Application.Contracts;

public interface IEmailService
{
    void SendEmailRecoverPassword(User user);
}