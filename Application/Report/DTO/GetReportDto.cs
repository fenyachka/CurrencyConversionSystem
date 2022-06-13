using System;

namespace Application.Report.DTO
{
    public class GetReportDto
    {
        public string PersonalNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}