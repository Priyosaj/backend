using Microsoft.AspNetCore.Mvc;
using Priyosaj.Api.Errors;

namespace Priyosaj.Api.Controllers;

[Route("errors/{code}")]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : Controller
{
    public IActionResult Error(int code)
    {
        return new ObjectResult(new ApiResponse(code));
    }
}