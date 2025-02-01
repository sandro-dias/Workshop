using Application.UseCases.Service.GetReport.Input;
using FluentValidation;
using System;

namespace Application.UseCases.Service.GetReport.Validator
{
    public class GetReportInputValidator : AbstractValidator<GetReportInput>
    {
        public GetReportInputValidator()
        {
            RuleFor(x => x.EndDate.Value.Day)
                .LessThanOrEqualTo(DateTime.Now.Day);

            RuleFor(x => x.InitialDate)
                .NotEmpty()
                .NotNull()
                .LessThanOrEqualTo(x => x.EndDate);
        }
    }
}
