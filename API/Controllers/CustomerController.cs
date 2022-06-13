using System.Threading.Tasks;
using Application.Customer;
using Application.Customer.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Customer Controller
    /// </summary>
    public class CustomerController : BaseApiController
    {
        /// <summary>
        /// Creates a customer.
        /// </summary>
        /// <param name="PersonalNumber">PersonalNumber</param>
        /// <param name="FirstName">FirstName</param>
        /// <param name="LastName">LastName</param>
        /// <param name="RecommenderPersonalNumber">RecommenderPersonalNumber</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /customer
        ///     {
        ///        "personalNumber": "0101..",
        ///        "firstName": "James",
        ///        "lastName": "Bond",
        ///        "recommenderPersonalNumber": "0101.."
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns Success</response>
        /// <response code="400">Customer with entered Private Number already exists</response>
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto dto)
        {
            return HandleResult(await Mediator.Send(new Create.Command { CreateCustomerDto = dto }));
        }   
    }
}