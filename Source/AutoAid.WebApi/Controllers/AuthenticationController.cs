using AutoAid.Application.Service;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AutoAid.WebApi.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Generate a new access token for the user
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [HttpGet("access-token")]
        public async Task<IActionResult> GenerateAccessToken(string uid)
        {
            var result = await _authenticationService.GenerateAccessToken(uid);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Validate the access token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("validate-access-token")]
        public async Task<IActionResult> ValidateAccessToken(string token)
        {
            var result = await _authenticationService.ValidateAccessToken(token);
            return StatusCode((int)result.StatusCode, result);

        }
    }
}
