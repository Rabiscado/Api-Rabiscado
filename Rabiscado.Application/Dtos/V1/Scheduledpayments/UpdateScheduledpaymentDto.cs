﻿namespace Rabiscado.Application.Dtos.V1.Scheduledpayments;

public class UpdateScheduledpaymentDto
{
    public int Id { get; set; }
    public decimal Value { get; set; }
    public string Professor { get; set; } = null!;
    public DateTime Date { get; set; }
    public int UserId { get; set; }
}