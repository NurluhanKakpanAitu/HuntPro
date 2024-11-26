using System.Net;
using System.Security.Claims;
using AuthServer.Common.Wrappers;
using AuthServer.Core.Application.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AuthServer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected readonly string? UserId;
    
    protected BaseController(IHttpContextAccessor context)
    {
        var user = context.HttpContext?.User;
        if (user == null || !user.Claims.Any())
            return;

        UserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
    
    protected IActionResult ResponseOk() => Ok(ApiResponse.Ok());

    protected IActionResult ResponseOk<T>(T data) => Ok(ApiResponse.Ok(data));

    private IActionResult ResponseError(HttpStatusCode statusCode, IEnumerable<ApiErrorDetail> errorDetails)
    {
        return statusCode switch
        {
            HttpStatusCode.BadRequest => BadRequest(ApiResponse.Error((int)statusCode, errorDetails)),
            HttpStatusCode.Unauthorized => Unauthorized(ApiResponse.Error((int)statusCode, errorDetails)),
            HttpStatusCode.Forbidden => Forbid(),
            HttpStatusCode.NotFound => NotFound(ApiResponse.Error((int)statusCode, errorDetails)),
            HttpStatusCode.Conflict => Conflict(ApiResponse.Error((int)statusCode, errorDetails)),
            HttpStatusCode.UnprocessableEntity => UnprocessableEntity(ApiResponse.Error((int)statusCode, errorDetails)),
            _ => throw new InvalidOperationException(),
        };
    }

    protected IActionResult ResponseError(HttpStatusCode statusCode) =>
        ResponseError(statusCode, []);

    protected IActionResult ResponseError(HttpStatusCode statusCode, ApiErrorDetail errorDetail) =>
        ResponseError(statusCode, [errorDetail]);

    protected IActionResult ResponseError(ModelStateDictionary modelState)
    {
        var errorDetails = modelState.SelectMany(x =>
            x.Value?.Errors.Select(e => new ApiErrorDetail(e.ErrorMessage, x.Key, e.ErrorMessage)) ??
            Array.Empty<ApiErrorDetail>());

        return ResponseError(HttpStatusCode.BadRequest, errorDetails);
    }
}