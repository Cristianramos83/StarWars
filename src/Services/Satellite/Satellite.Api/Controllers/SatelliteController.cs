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
    [Route("topsecret")]
    public class SatelliteController : ControllerBase
    {
        private readonly ISatelliteQueryService _satelliteQueryService;
        private readonly ILogger<SatelliteController> _logger;
        private IMediator _mediator;
        public SatelliteController(
            ILogger<SatelliteController> logger,
            ISatelliteQueryService satelliteQueryService,
            IMediator mediator)
        {
            _logger = logger;
            _satelliteQueryService = satelliteQueryService;
            _mediator = mediator;
        }
        //satellites
        [HttpGet]
        public async Task<DataCollection<SatelliteDto>> GetAll(int page = 1, int take = 10, string ids = null)
        {
            IEnumerable<int> satellites = null;

            if (!string.IsNullOrEmpty(ids))
            {
                satellites = ids.Split(',').Select(x => Convert.ToInt32(x));
            }                       
            
            
            return await _satelliteQueryService.GetAllAsync(page, take, satellites);
        }
        [HttpPost]
        public async Task<ActionResult<SatelliteDto>> Update(SatellitesUpdateCommand command)
        {
            await _mediator.Publish(command);
            var satelliteSource = _satelliteQueryService.GetSource();

            if (satelliteSource == null)
            {
                return NotFound();
            }
            return _satelliteQueryService.GetSource();
        }
    }
}
