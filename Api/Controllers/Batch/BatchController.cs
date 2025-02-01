using Application.Services.CreateWorkingDay;
using Application.Services.CreateWorkingDay.Input;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Api.Controllers.Batch
{
    /// <summary>
    /// Rota para testes e troubleshooting dos dias de trabalho
    /// </summary>
    [ApiController]
    [Route("v1")]
    public class BatchController : ControllerBase
    {
        private readonly ILogger<BatchController> _logger;

        public BatchController(ILogger<BatchController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("api/create-working-day/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateWorkingDay([FromBody, Required] CreateWorkingDayInput input, [FromServices] ICreateWorkingDayService createWorkingDayService)
        {
            try
            {
                var result = await createWorkingDayService.CreateWorkingDay(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{ClassName}] It was not possible to post the working day. The message returned was: {@Message}", nameof(BatchController), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao inserir dia de trabalho no banco de dados.");
            }
        }
    }
}
