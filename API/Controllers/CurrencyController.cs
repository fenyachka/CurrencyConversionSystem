using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Currency;
using Application.Currency.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CurrencyController : BaseApiController
    {
        #region currencies

        /// <summary>
        /// Get list of currency.
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="code">Code</param>
        /// <param name="nameGeo">NameGeo</param>
        /// <param name="nameLat">NameLat</param>
        /// <remarks>
        /// Sample response:
        ///     [
        ///         {
        ///             "id": 1,
        ///             "code": "GEL",
        ///             "nameGeo": "ლარი",
        ///             "nameLat": "Lari"
        ///         },
        ///     ]
        ///
        /// </remarks>
        /// <response code="200">Returns List of currencies</response>
        [HttpGet]
        public async Task<ActionResult<List<CurrencyToReturnDto>>> GetCurrencies()
        {
            return HandleResult(await Mediator.Send(new List.Query {}));
        }

        
        /// <summary>
        /// Creates a currency.
        /// </summary>
        /// <param name="item"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /currency
        ///     {
        ///        "code": GEL,
        ///        "nameGeo": "ლარი",
        ///        "nameLat": "Lari"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns Success</response>
        /// <response code="400">Currency with same Code already exist</response>
        /// <response code="400">Failed to create currency</response>
        [HttpPost]
        public async Task<IActionResult> CreateCurrency(CreateCurrencyDto dto)
        {
            return HandleResult(await Mediator.Send(new Create.Command { CreateCurrencyDto = dto }));
        }
        #endregion
    }
}