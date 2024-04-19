namespace Rabiscado.Domain.Contracts.Pagination;

public interface IResultPaginated<T>
{
    public IList<T> Itens { get; set; }
    public IPagination Pagination { get; set; }
}