using System;
using Application.Report.DTO;
using FluentValidation;

namespace Application.Report.Validators
{
    public class GetReportDtoValidator : AbstractValidator<GetReportDto>
    {
        public GetReportDtoValidator()
        {
            RuleFor(p => p.PersonalNumber).NotNull().NotEmpty().MaximumLength(11);
            RuleFor(p => p.EndDate).Must(IsValidDate);
            RuleFor(p => p.StartDate).Must(IsValidDate);
            
        }
        
        protected bool IsValidDate(DateTime? date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}