using System;

namespace Application.UseCases.Service.GetReport.Input
{
    public class GetReportInput
    {
        public DateTime InitialDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
