namespace Rabiscado.Domain.Contracts;

public interface IUnitOfWork
{
    Task<bool> Commit();
}