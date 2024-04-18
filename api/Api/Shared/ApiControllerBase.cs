using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Shared;

[ApiController]
public class ApiControllerBase : ControllerBase
{
    private ISender? _sender;
    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetService<ISender>()!;
}