using System.Threading.Tasks;
using AgroPlan.Property.AgroPlan.Property.Api.Application.Commands;
using AgroPlan.Property.AgroPlan.Property.Api.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace AgroPlan.Property.AgroPlan.Property.Api.Controller
{
    [ApiController]
    [Route("ppy/[controller]")]
    public class OwnerRepository : BaseController
    {
        //Commands
        [HttpPut]
        [Route("/register")]
        public async Task<IActionResult> Register(RegisterOwnerCommand command)
        {
            var result = await mediator.Send(command);

            if(result)
                return Ok();

            return BadRequest();
        }

        [HttpPost]
        [Route("/unregister")]
        public async Task<IActionResult> Unregister(UnregisterOwnerCommand command)
        {
            var result = await mediator.Send(command);

            if(result)
                return Ok();

            return BadRequest();
        }

        //Query
        // [HttpGet]
        // public async Task<IActionResult> GetAll()
        // {
        //     //var result = await mediator.Send();

        //     return Ok();
        // }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(string ownerId)
        {
            var result = await mediator.Send(new GetOwnerByIdQuery(ownerId));

            return Ok(result);
        }
    }
}