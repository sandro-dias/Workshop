using Application.UseCases.Service.GetReport.Input;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.UseCases.Service.GetReport
{
    public interface IGetReportUseCase
    {
        Task<IEnumerable<Domain.Entities.Service>> ExecuteAsync(GetReportInput input);
    }
}
