using Microsoft.AspNetCore.Mvc;

namespace Raymaker.SimpleApiJWTDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IJWTAuthentication auth;

        public UsersController(IJWTAuthentication authentication)
        {
            auth = authentication;
        }

        /// <summary>
        /// Login using username 'string' and password 'string' to get a token.
        /// </summary>
        /// <param name="credential">The credentials.</param>
        /// <returns>A JWT token.</returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserCredential credential)
        {
            var token = auth.Login(credential.UserName, credential.Password);
            if (token == null) return Unauthorized();
            return Ok(token);
        }
    }
}
