namespace Rabiscado.Domain.Contracts;

public interface ITracking
{
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
}