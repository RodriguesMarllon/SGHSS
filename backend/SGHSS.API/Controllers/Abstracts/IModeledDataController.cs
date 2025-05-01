using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Abstracts
{
    public interface IModeledDataController
    {
        Task<IActionResult> GetAllModeled();
        Task<IActionResult> GetAllModeledColumnFiltered([FromQuery] string? searchString);
    }
}
