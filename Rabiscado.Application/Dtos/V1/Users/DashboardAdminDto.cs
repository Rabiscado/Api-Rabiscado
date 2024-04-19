namespace Rabiscado.Application.Dtos.V1.Users;

public class DashboardAdminDto
{
    public decimal TotalReceiptsPerMonth { get; set; }
    public int Subscribes { get; set; }
    public int Reimbursement { get; set; }
    public List<ReceiptPlanDto> TotalReceiptPlansPerMonth { get; set; } = new();
}

public class ReceiptPlanDto
{
    public int PlanId { get; set; }
    public string Name { get; set; } = null!;
    public int Subscribes { get; set; }
    public decimal Receipts { get; set; }
}