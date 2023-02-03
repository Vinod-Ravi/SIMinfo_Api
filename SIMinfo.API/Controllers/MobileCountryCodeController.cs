using Microsoft.AspNetCore.Mvc;
using SIMinfo.API.Models;
using SIMinfo.API.Services.Class;
using SIMinfo.API.Services.Interface;

namespace SIMinfo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MobileCountryCodeController : Controller
    {
        private readonly IMobileCountryCodeService _mobileCountryCodeService;
        private readonly ILogger<MobileCountryCodeController> _logger;

        public MobileCountryCodeController(IMobileCountryCodeService mobileCountryCodeService, ILogger<MobileCountryCodeController> logger)
        {
            _mobileCountryCodeService = mobileCountryCodeService;
            _logger = logger;

        }

        [HttpGet]
        public async Task<IActionResult> GetMobileCountryCodes()
        {
            _logger.LogInformation("Getting Mobile country codes");

            var countryCodes = await _mobileCountryCodeService.GetMobileCountryCodes();
            if (countryCodes != null)
            {
                return Ok(countryCodes);
            }
            throw new ApplicationException("Invalid");
        }

        [HttpPost]
        public async Task<IActionResult> SaveMobileCountryCode([FromBody] MobileCountryCode mobileCountryCode)
        {
            _logger.LogInformation("Saving Mobile country codes");

            var countryCode = await _mobileCountryCodeService.SaveMobileCountryCode(mobileCountryCode);
            if (countryCode == true)
            {
                return Ok(countryCode);
            }
            throw new ApplicationException("Invalid");
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateMobileCountryCode([FromRoute] Guid id, [FromBody] MobileCountryCode mobileCountryCode)
        {
            _logger.LogInformation("Updating Mobile country codes");
            var existingCountryCode = await _mobileCountryCodeService.UpdateMobileCountryCode(id, mobileCountryCode);
            if (existingCountryCode == true)
            {
                return Ok(mobileCountryCode);
            }
            throw new ApplicationException("Invalid");
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteMobileCountryCode([FromRoute] Guid id)
        {
            _logger.LogInformation("Deleting Mobile country codes");

            var existingCountryCode = await _mobileCountryCodeService.DeleteMobileCountryCode(id);
            if (existingCountryCode == true)
            {
                return Ok(existingCountryCode);
            }
            throw new ApplicationException("Invalid");
        }
    }
}
