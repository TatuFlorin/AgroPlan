using System;
using AgroPlan.Planification.Core.Model.Interfaces;
using core = AgroPlan.Planification.Core.Model.Aggregate;
using Microsoft.AspNetCore.Mvc;
using AgroPlan.Planification.Core.Model.ValueObjects;
using AgroPlan.Planification.Infrastructure.Repositories;
using System.Threading.Tasks;
using AgroPlan.Planification.Api.Application.Commands;
using AgroPlan.Planification.Api.Application.Queries;

namespace AgroPlan.Planification.Api.Controllers
{
    [Route("PlanApi/[controller]")]
    [ApiController]
    public class PlanificationController : BaseController
    {

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] MakePlanificationCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
                return Ok();

            return BadRequest();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterCrop([FromBody] RegisterCropCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result)
                return BadRequest();
            return Ok();
        }

        [HttpDelete]
        [Route("Unregister")]
        public async Task<IActionResult> UnregisterCrop([FromBody] UnregisterCropCommand command)
        {
            bool result = await _mediator.Send(command);
            if (result)
                return Ok(Guid.NewGuid());

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> PlanificationsByClient([FromQuery] string clientId)
        {
            var listOfPlanifications = await _mediator.Send(new GetPlanificationsByClientQuery(clientId));
            return Ok(listOfPlanifications);
        }

        [HttpGet]
        [Route("{planificationId}")]
        public async Task<IActionResult> GetPlanificationCrops(Guid planificationId)
        {
            var crops = await _mediator.Send(new GetCropsByPlanificationQuery(planificationId));
            return Ok(crops);
        }
    }
}