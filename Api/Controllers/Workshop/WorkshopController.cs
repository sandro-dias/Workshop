using Application.UseCases.CreateWorkshop;
using Application.UseCases.CreateWorkshop.Input;
using Application.UseCases.GetWorkshopWorkload;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Api.Controllers.PostWorkshop
{
    [ApiController]
    [Route("v1")]
    public class WorkshopController(ILogger<WorkshopController> logger, ICreateWorkshopUseCase postWorkshopUseCase, IGetWorkshopWorkloadUseCase getWorkshopWorkload) : ControllerBase
    {
        private readonly ILogger<WorkshopController> _logger = logger;
        private readonly ICreateWorkshopUseCase _postWorkshopUseCase = postWorkshopUseCase;
        private readonly IGetWorkshopWorkloadUseCase _getWorkshopWorkload = getWorkshopWorkload;

        /// <summary>
        /// Rota para criar uma oficina.
        /// </summary>
        [HttpPost]
        [Route("api/create-workshop/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostWorkshop([FromBody, Required] CreateWorkshopInput input)
        {
            try
            {
                var workshop = await _postWorkshopUseCase.ExecuteAsync(input);
                return Ok(workshop);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{ClassName}] It was not possible to post the workshop. The message returned was: {@Message}", nameof(WorkshopController), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao inserir oficina no banco de dados.");
            }
        }

        /// <summary>
        /// Rota para buscar a carga de trabalho da oficina nos próximas 5 dias úteis
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("api/get-workshop-workload/{workShopId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetWorkshopWorkload([FromRoute, Required] long workShopId)
        {
            try
            {
                var workloadList = await _getWorkshopWorkload.ExecuteAsync(workShopId);
                return Ok(workloadList);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{ClassName}] It was not possible to get the workshop workload. The message returned was: {@Message}", nameof(WorkshopController), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar a carga disponível dos próximos dias da oficina no banco de dados.");
            }
        }
    }
}
