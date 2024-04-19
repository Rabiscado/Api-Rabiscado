using Rabiscado.Application.Adapters.Assas.Application.Dtos.V1.Subscriptions;
using Rabiscado.Application.Dtos.V1.Base;
using Rabiscado.Application.Dtos.V1.Plan;
using Rabiscado.Application.Dtos.V1.Subscription;
using Rabiscado.Application.Dtos.V1.Users;

namespace Rabiscado.Application.Contracts;

public interface IUserService
{
    Task<UserDto?> GetById(int id);
    Task<List<UserDto>> GetAll();
    Task<PagedDto<UserDto>> Search(UserSearchDto dto);
    Task<UserDto?> Create(CreateUserDto dto);
    Task<UserDto?> Update(int id, UpdateUserDto dto);
    Task<bool> Disable(int id);
    Task<bool> Active(int id);
    Task<bool> ToggleAdmin(int id);
    Task<bool> ToggleInstructor(int id);
    Task<SubscriptionResponseDto?> Subscribe(SubscribePlanDto dto);
    Task<SubscriptionUnsubscribreResponseDto?> Unsubscribe(string email);
    Task<UserDto?> Subscribe(SubscribeDto dto);
    Task<UserDto?> Unsubscribe(UnsubscribeDto dto);
    Task<DashboardAdminDto> DashboardAdmin();
    Task<DashboardProfessorDto> DashboardProfessor();
    Task<string?> GenerateReceiptsPdf();
    Task MarkAsWatched(int classId);
    Task MarkAsUnWatched(int classId);
    Task<string?> GenerateProfessorsPdf();
    Task<string?> GenerateProfessorsReceiptPdf();
    Task<string?> GenerateProfessorMonthlyReceiptPdf();
    Task<UserDto?> HideSubscription(int id);
    Task GenerateCourseBilling();
    Task GenerateScheduledPayment();
}