using System.ComponentModel;

namespace Rabiscado.Core.Enums;

public enum EForWho
{
    [Description("Casal")]
    Couple = 1,
    [Description("Condutor")]
    Conductor = 2,
    [Description("Conduzido")]
    Led = 3,
}