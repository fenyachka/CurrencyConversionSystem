using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Report;
using Application.Report.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ReportController : BaseApiController
    {
        /// <summary>
        /// Get Report.
        /// </summary>
        /// 
        /// {
        ///  "personalNumber": "string",
        ///  "startDate": "2022-06-12T17:50:21.843Z",
        ///  "endDate": "2022-06-12T17:50:21.843Z"
        /// }
        /// 
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate">End Date</param>
        /// <response code="200">Returns Report</response>
        [HttpGet]
        public async Task<ActionResult<List<ReportToReturnDto>>> GetReport([FromQuery] GetReportDto reportDto)
        {
            return HandleResult(await Mediator.Send(new GetReport.Query {GetReportDto = reportDto}));
        }

    }
}