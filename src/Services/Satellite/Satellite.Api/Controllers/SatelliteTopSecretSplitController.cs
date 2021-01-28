using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Satellite.Service.EventHandlers.Commands;
using Satellite.Service.Queries;
using Satellite.Service.Queries.DTOs;
using Service.Common.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Satellite.Api.Controllers
{
    [ApiController]
    [Route("topsecret_split")]
    public class SatelliteTopSecretSplitController : ControllerBase
    {
        private readonly ISatelliteQueryService _satelliteQueryService;
        private readonly ILogger<SatelliteTopSecretSplitController> _logger;
        private IMediator _mediator;
        public SatelliteTopSecretSplitController(
            ILogger<SatelliteTopSecretSplitController> logger,
            ISatelliteQueryService satelliteQueryService,
            IMediator mediator)
        {
            _logger = logger;
            _satelliteQueryService = satelliteQueryService;
            _mediator = mediator;
        }
                
        [HttpPost("{name}")]
        public async Task<IActionResult> Update(string name, [FromBody] SatelliteUpdateCommand command)
        {
            command.Name = name;

            await _mediator.Publish(command);

            return Ok();
            
        }

        [HttpGet]
        public ActionResult<SatelliteDto> Get()
        {
            var satelliteSource = _satelliteQueryService.GetSource();

            if (satelliteSource == null)
            {
                return NotFound();
            }
            return  _satelliteQueryService.GetSource();
        }
    }
}
