using System.Threading.Tasks;
using AgroPlan.Property.AgroPlan.Property.Api.Application.Commands;
using AgroPlan.Property.AgroPlan.Property.Api.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace AgroPlan.Property.AgroPlan.Property.Api.Controllers
{
    [ApiController]
    [Route("owner/[controller]")]
    public class OwnerController : BaseController
    {
        //Commands
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterOwnerCommand command)
        {
            var result = await mediator.Send(command);

            if(result)
                return Ok();

            return BadRequest();
        }

        [HttpPost]
        [Route("Unregister")]
        public async Task<IActionResult> Unregister(UnregisterOwnerCommand command)
        {
            var result = await mediator.Send(command);

            if(result)
                return Ok();

            return BadRequest();
        }

        // Query
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await mediator.Send(new GetAllOwnersQuery());
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetDetails(string id)
        {
            var result = await mediator.Send(new GetOwnerByIdQuery(id));
            return Ok(result);
        }
    }
}