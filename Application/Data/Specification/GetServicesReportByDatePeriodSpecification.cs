using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Linq;

namespace Application.Data.Specification
{
    public class GetServicesReportByDatePeriodSpecification : Specification<Service>
    {
        public GetServicesReportByDatePeriodSpecification(DateTime initialDate, DateTime endDate)
        {
            if (endDate == DateTime.MinValue)
                endDate = DateTime.Now;

            Query
                .Where(c => c.Date.Date.Day >= initialDate.Day)
                .Where(c => c.Date.Date.Day <= endDate.Day);
        }
    }
}
