﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api;

public class ErrorController : ControllerBase
{
    [Route("/error-dev")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult HandleErrorDevelopment([FromServices] IHostEnvironment hostEnvironment)
    {
        if (!hostEnvironment.IsDevelopment()) return NotFound();

        var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
        return Problem(
            exceptionHandlerFeature?.Error.StackTrace,
            title: exceptionHandlerFeature?.Error.Message);
    }

    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult HandleError()
    {
        return Problem();
    }
}