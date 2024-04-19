﻿namespace Rabiscado.Application.Adapters.Assas.Domain.Entities;

public class PaymentLink
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal? Value { get; set; }
    public bool Active { get; set; }
    public string ChangeType { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string? BillingType { get; set; }
    public string? SubscriptionCycle { get; set; }
    public string? Description { get; set; }
    public DateTime? EndDate { get; set; }
    public bool Deleted { get; set; }
    public int ViewCount { get; set; }
    public int MaxInstallmentCount { get; set; }
    public int? DueDateLimitDays { get; set; }
    public bool NotificationEnabled { get; set; }
}