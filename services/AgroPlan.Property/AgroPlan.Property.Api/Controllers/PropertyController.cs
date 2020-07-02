using System.Threading.Tasks;
using AgroPlan.Property.AgroPlan.Property.Api.Application.Commands;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AgroPlan.Property.AgroPlan.Property.Api.Controller
{
    [ApiController]
    [Route("ppy/[controller]")]
    public class PropertyController : BaseController 
    {
        //Commands
        [HttpPut]
        public async Task<IActionResult> Register(AddPropertyCommand command)
        {
            var response = await mediator.Send(command);

            if(response)
                return Ok();

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Unregister(EliminatePropertyCommand command)
        {
            var response = await mediator.Send(command);

            if(response)
                return Ok();

            return BadRequest();
        }

        //Queries
        // [HttpGet]
        // public async Task<IActionResult> GetAll()
        // {
        //     return Ok();
        // }

        // [HttpGet]
        // public async Task<IActionResult> GetByOwnerId(string ownerId)
        // {
        //     return Ok();
        // }

        // [HttpGet]
        // public async Task<IActionResult> GetDetails(Guid propertyId)
        // {
        //     // var resutl = await mediator.Send()

        //     return Ok();
        // }
    }
}