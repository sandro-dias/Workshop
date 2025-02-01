using Application.UseCases.Customer.CreateCustomer;
using Application.UseCases.Customer.CreateCustomer.Input;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Api.Controllers.Customer
{
    [ApiController]
    [Route("v1")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICreateCustomerUseCase _createCustomerUseCase;

        public CustomerController(ILogger<CustomerController> logger, ICreateCustomerUseCase createServiceUseCase)
        {
            _logger = logger;
            _createCustomerUseCase = createServiceUseCase;
        }

        /// <summary>
        /// Rota para criar um cliente de uma oficina.
        /// </summary>
        [HttpPost]
        [Authorize]
        [Route("api/create-customer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCustomer([Required][FromBody] CreateCustomerInput input)
        {
            try
            {
                var result = await _createCustomerUseCase.ExecuteAsync(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{ClassName}] It was not possible to post the customer. The message returned was: {@Message}", nameof(CustomerController), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao inserir cliente no banco de dados.");
            }
        }
    }
}
