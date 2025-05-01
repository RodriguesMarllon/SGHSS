using System.Diagnostics.CodeAnalysis;

namespace Application.Models.Abstracts;

[ExcludeFromCodeCoverage]
public class ListRequestBase
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}
