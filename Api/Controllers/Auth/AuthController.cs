using Application.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Api.Controllers.Auth
{
    [ApiController]
    [Route("v1")]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Rota para autenticar uma oficina.
        /// </summary>
        [HttpGet]
        [Route("api/authentication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAuthentication([FromServices] AuthenticationMiddleware middleware, [FromHeader][Required] string cNPJ, [FromHeader][Required] string password)
        {
            var token = await middleware.AuthenticateWorkshop(cNPJ, password);
            if (string.IsNullOrEmpty(token))
                return StatusCode(StatusCodes.Status404NotFound, "A senha inserida para esta oficina está incorreta");

            return Ok(token);
        }
    }
}
