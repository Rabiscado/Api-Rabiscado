using Rabiscado.Application.Dtos.V1.User;
using Rabiscado.Application.Dtos.V1.Users;

namespace Rabiscado.Application.Dtos.V1.Scheduledpayments;

public class ScheduledpaymentDto
{
    public int Id { get; set; }
    public decimal Value { get; set; }
    public string Professor { get; set; } = null!;
    public string ProfessorName { get; set; } = null!;
    public DateTime Date { get; set; }
    public bool PaidOut { get; set; }
    public int UserId { get; set; }
    public UserDto User { get; set; } = null!;
}