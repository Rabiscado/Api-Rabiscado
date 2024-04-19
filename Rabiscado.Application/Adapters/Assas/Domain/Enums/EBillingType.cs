using System.ComponentModel;

namespace Rabiscado.Application.Adapters.Assas.Domain.Enums;

public enum EBillingType
{
    Boleto,
    [Description("CREDIT_CARD")]
    CreditCard,
    Pix
}