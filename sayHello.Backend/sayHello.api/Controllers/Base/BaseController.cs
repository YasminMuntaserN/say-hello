using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace sayHello.api.Controllers.Base;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected IActionResult HandleResponse<T>(T data, string message = "Success")
    {
        return Ok(new
        {
            Success = true,
            Message = message,
            Data = data
        });
    }

    protected IActionResult HandleError(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return StatusCode((int)statusCode, new
        {
            Success = false,
            Message = message
        });
    }
}