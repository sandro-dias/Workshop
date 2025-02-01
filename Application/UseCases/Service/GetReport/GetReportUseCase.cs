using Application.Data;
using Application.Data.Specification;
using Application.UseCases.Service.GetReport.Input;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.UseCases.Service.GetReport
{
    public class GetReportUseCase(IValidator<GetReportInput> validator, IUnitOfWork unitOfWork, ILogger<GetReportUseCase> logger) : IGetReportUseCase
    {
        private readonly IValidator<GetReportInput> _validator = validator;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<GetReportUseCase> _logger = logger;

        public async Task<IEnumerable<Domain.Entities.Service>> ExecuteAsync(GetReportInput input)
        {
            var validationResult = _validator.Validate(input);
            if (!validationResult.IsValid)
            {
                _logger.LogError("[{ClassName}] The input returned an error: {Errors}", nameof(GetReportUseCase), validationResult.Errors);
                return default;
            }

            var result = await _unitOfWork.ServiceRepository.ListAsync(new GetServicesReportByDatePeriodSpecification(input.InitialDate, input.EndDate.Value));
            return result;
        }
    }
}
