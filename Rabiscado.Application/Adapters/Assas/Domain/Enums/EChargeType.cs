using System.ComponentModel;

namespace Rabiscado.Application.Adapters.Assas.Domain.Enums;

public enum EChargeType
{
    Detached,
    [Description("RECURRENT")]
    Recurrent,
    Installment
}