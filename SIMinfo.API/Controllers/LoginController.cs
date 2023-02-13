using Microsoft.AspNetCore.Mvc;
using SIMinfo.API.Models;
using SIMinfo.API.Services.Class;
using SIMinfo.API.Services.Interface;

namespace SIMinfo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly ILogger<LoginController> _logger;
        private readonly IAuthenticationServices _authenticationService;

        public LoginController(ILoginService loginService, ILogger<LoginController> logger, IAuthenticationServices authenticationService)//, Messages messages)
        {
            _loginService = loginService;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        [HttpPost("userRegistration")]
        public async Task<IActionResult> SubmitUserRegistration([FromBody] User user)//
        {
            _logger.LogInformation("User Registration");
            var msg = await _loginService.SubmitUserRegistration(user);
            if (msg != null)
            {
                return Ok(msg);
            }
            throw new ApplicationException("Invalid");
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> UserAuthentication([FromBody] User user)
        {
            _logger.LogInformation("User Authentication");
            var msg = await _authenticationService.CheckUserAuthentication(user);
            if (msg != null)
            {
                return Ok(msg);
            }
            throw new ApplicationException("Invalid");
        }
    }
}
