using Microsoft.AspNetCore.Mvc;
using SGA.Domain.Base;

namespace SGA.API.Controllers;

[ApiController]
public abstract class BaseApiController : ControllerBase
{
    protected IActionResult ToHttpResponse<T>(OperationResult<T> result)
    {
        if (!result.Success)
            return BadRequest(new { result.Errors, result.Message });

        if (result.Data == null)
            return NotFound(new { Message = "Recurso no encontrado." });

        return Ok(result.Data);
    }

    protected IActionResult ToHttpResponse(OperationResult result)
    {
        if (!result.Success)
            return BadRequest(new { result.Errors, result.Message });

        return Ok(new { result.Message });
    }
}
