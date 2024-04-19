using Rabiscado.Domain.Entities;

namespace Rabiscado.Domain.Contracts.Repositories;

public interface IExtractReceiptRepository : IRepository<ExtractReceipt>
{
    Task<ExtractReceipt?> GetById(int id);
    Task<List<ExtractReceipt>> GetAll();
    void Add(ExtractReceipt extractReceipt);
    void Update(ExtractReceipt extractReceipt);
}