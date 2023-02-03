using Microsoft.AspNetCore.Mvc;
using SIMinfo.API.Models;
using SIMinfo.API.Services.Interface;

namespace SIMinfo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SimInformationController : Controller
    {
        private readonly ISimInfoService _simInfoService;
        private readonly ILogger<SimInformationController> _logger;

        public SimInformationController(ISimInfoService simInfoService, ILogger<SimInformationController> logger)
        {
            _simInfoService = simInfoService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSimInformation()
        {
            _logger.LogInformation("Getting SIM information");
            var simInfo = await _simInfoService.GetSimInformation();
            if (simInfo != null)
            {
                return Ok(simInfo);
            }
            throw new ApplicationException("Invalid");
        }

        [HttpPost]
        public async Task<IActionResult> SaveSimInformation([FromBody] SimInformation simInformation)
        {
            _logger.LogInformation("Saving SIM information");

            var simInfo = await _simInfoService.SaveSimInformation(simInformation);
            if (simInfo == true)
            {
                return Ok(simInformation);
            }
            throw new ApplicationException("Invalid");
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateSimInformation([FromRoute] Guid id, [FromBody] SimInformation simInformation)
        {
            _logger.LogInformation("Updating SIM information");
            var existingSiminformation = await _simInfoService.UpdateSimInformation(id, simInformation);
            if (existingSiminformation == true)
            {
                return Ok(simInformation);
            }
            throw new ApplicationException("Invalid");
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteSimInformation([FromRoute] Guid id)
        {
            _logger.LogInformation("Deleting SIM information");

            var existingSiminformation = await _simInfoService.DeleteSimInformation(id);
            if (existingSiminformation == true)
            {
                return Ok(existingSiminformation);
            }
            throw new ApplicationException("Invalid");
        }
    }
}
