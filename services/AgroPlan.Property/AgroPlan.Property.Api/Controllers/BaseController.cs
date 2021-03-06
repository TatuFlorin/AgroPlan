using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AgroPlan.Property.AgroPlan.Property.Api.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IMediator mediator => HttpContext.RequestServices.GetService<IMediator>();
    }
}