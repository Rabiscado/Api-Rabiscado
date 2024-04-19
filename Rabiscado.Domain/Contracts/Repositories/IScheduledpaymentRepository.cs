using Rabiscado.Domain.Entities;

namespace Rabiscado.Domain.Contracts.Repositories;

public interface IScheduledpaymentRepository : IRepository<Scheduledpayment>
{
    Task<Scheduledpayment?> GetById(int id);
    Task<List<Scheduledpayment>> GetAll();
    void Create(Scheduledpayment scheduledpayment);
    void Update(Scheduledpayment scheduledpayment);
    void Disable(Scheduledpayment scheduledpayment);
    void Active(Scheduledpayment scheduledpayment);
    void MarkAsPaidOut(Scheduledpayment scheduledpayment);
    void MarkAsUnPaid(Scheduledpayment scheduledpayment);
    Task<(decimal TotalReceipt, decimal ToReceive, decimal Received)> TotalReceipt(string professorEmail);
}