using System.Threading.Tasks;
using AgroPlan.Property.AgroPlan.Property.Api.Application.Commands;
using Microsoft.AspNetCore.Mvc;
using System;
using AgroPlan.Property.AgroPlan.Property.Api.Application.Queries;

namespace AgroPlan.Property.AgroPlan.Property.Api.Controllers
{
    [ApiController]
    [Route("ppy/[controller]")]
    public class PropertyController : BaseController 
    {
        //Commands
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(AddPropertyCommand command)
        {
            var response = await mediator.Send(command);

            if(response)
                return Ok();

            return BadRequest();
        }

        [HttpPost]
        [Route("Unregister")]
        public async Task<IActionResult> Unregister([FromBody] EliminatePropertyCommand command)
        {
            var response = await mediator.Send(command);

            if(response)
                return Ok();

            return BadRequest();
        }

        //Queries
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await mediator.Send(new GetPropertiesQuery());
            return Ok(result);
        }

        [HttpGet("ownerId")]
        public async Task<IActionResult> GetByOwnerId(string ownerId)
        {
            var result = await mediator.Send(new GetPropertiesByOwnerQuery(ownerId));
            return Ok(result);
        }

        [HttpGet("propertyId")]
        public async Task<IActionResult> GetDetails(Guid propertyId)
        {
            var result = await mediator.Send(new GetPropertyDetailsQuery(propertyId));
            return Ok(result);
        }
    }
}