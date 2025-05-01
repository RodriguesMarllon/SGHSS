using System.Diagnostics.CodeAnalysis;

namespace Application.Models.Abstracts;

[ExcludeFromCodeCoverage]
public class ListResponseBase<T>
{
    public int PageIndex { get; set; } = 0;
    public int TotalCount { get; set; } = 0;
    public int PageSize { get; set; } = 1;
    public decimal TotalPages { get; private set; } = 0;
    public  ICollection<T> Items { get; set; }

    public ListResponseBase(int pageIndex, int totalCount, int pageSize, ICollection<T> items)
    {
        PageIndex = pageIndex;
        TotalCount = totalCount;
        PageSize = pageSize;
        TotalPages = Math.Ceiling((decimal)totalCount / PageSize);
        Items = items;
    }
}
