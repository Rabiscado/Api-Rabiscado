namespace Rabiscado.Application.Dtos.V1.Users;

public class DashboardProfessorDto
{
    public int Subscribes { get; set; }
    public int Unsubscribes { get; set; }
    public decimal TotalReceipt { get; set; }
    public decimal ToReceive { get; set; }
    public decimal Received { get; set; }
}