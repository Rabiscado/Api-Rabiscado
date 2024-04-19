using System.ComponentModel;

namespace Rabiscado.Application.Adapters.Assas.Domain.Enums;

public enum ESubcscriptionCycle
{
    Weekly,
    BiWeekly,
    [Description("MONTHLY")]
    Monthly,
    Quarterly,
    SemiAnnually,
    Yearly
}