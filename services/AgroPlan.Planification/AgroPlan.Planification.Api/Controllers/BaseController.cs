using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace AgroPlan.Planification.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        //private readonly IMediator mediator;
        protected IMediator _mediator => HttpContext.RequestServices.GetService<IMediator>();
    }
}