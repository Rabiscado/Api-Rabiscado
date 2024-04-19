using Rabiscado.Application.Dtos.V1.Base;
using Rabiscado.Application.Dtos.V1.Scheduledpayments;

namespace Rabiscado.Application.Contracts;

public interface IScheduledpaymentService
{
    Task<List<ScheduledpaymentDto>> GetAll();
    Task<PagedDto<ScheduledpaymentDto>> Search(SearchScheduledpaymentDto dto);
    Task<ScheduledpaymentDto?> GetById(int id);
    Task<ScheduledpaymentDto?> Create(CreateScheduledpaymentDto dto);
    Task<ScheduledpaymentDto?> Update(int id, UpdateScheduledpaymentDto dto);
    Task<bool> Disable(int id);
    Task<bool> Active(int id);
    Task<bool> MarkAsPaid(int id);
    Task<bool> MarkAsUnPaid(int id);
}