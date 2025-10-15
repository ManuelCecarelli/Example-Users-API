using Application.Interfaces;
using Application.Models.RequestDTO;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ICustomAuthenticationService _customAuthenticationService;

        public AuthenticationController(ICustomAuthenticationService customAuthenticationService)
        {
            _customAuthenticationService = customAuthenticationService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Authenticate([FromBody] AuthenticationCredentialsDTO authenticationCredentialsDTO)
        {
            try
            {
                string newToken = await _customAuthenticationService.Authenticate(authenticationCredentialsDTO);
                return newToken;
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }

            
        }
    }
}
