using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Currency;
using Application.Currency.DTO;
using Application.CurrencyRate;
using Application.CurrencyRate.DTO;
using Domain.Entities.Currencies;
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
        
        #region currency rate
        
        
        /// <summary>
        /// Get currency rate
        /// </summary>
        /// <param name="code">Currency Code</param>
        /// <returns/>Last updated currency rate object/returns>
        /// <response code="200">Returns Success, Currency Rate object</response>
        /// <response code="400">If the item is null, returns Failure object with message</response>
        [HttpGet("rate")]
        public async Task<ActionResult<CurrencyRate>> GetCurrencyRate([FromQuery] string code)
        {
            return HandleResult(await Mediator.Send(new GetCurrencyRate.Query { CurrencyCode = code }));
        }
        
        /// <summary>
        /// Creates a currency rate.
        /// </summary>
        /// <param name="currencyId">currencyId</param>
        /// <param name="buy">buy</param>
        /// <param name="sell">sell</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /rate
        ///     {
        ///        "currencyId": 2,
        ///        "buy": "3.12",
        ///        "sell": "3.10"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns Success</response>
        /// <response code="400">Currency Rate already exist.</response>
        [HttpPost("rate")]
        public async Task<IActionResult> CreateCurrencyRate(CreateCurrencyRateDto dto)
        {
            return HandleResult(await Mediator.Send(new CreateCurrencyRate.Command { CreateCurrencyRateDto = dto }));
        }

        #endregion
        
        #region exchange
        /// <summary>
        /// Get exchange rate
        /// </summary>
        /// <param name="from">From Currency</param>
        /// <param name="to">To Currency</param>
        /// <param name="amount">Amount From</param>
        /// <returns/>Last updated currency rate object/returns>
        /// <response code="200">Returns CurrencyRateWithAmount object</response>
        /// <response code="400">If the item is null, returns Failure object with message</response>
        [HttpGet("exchange-rate")]
        public async Task<ActionResult<ExchangeRate>> GetExchangeRate([FromQuery] ExchangeRateDto exchangeRateDto)
        {
            return HandleResult(await Mediator.Send(new ExchangeRate.Query { ExchangeRateDto = exchangeRateDto}));
        }
        
        
        
        /// <summary>
        /// Creates a transaction.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /create-transaction
        ///     {
        ///        "fromCurrency": GEL,
        ///        "toCurrency": "USD",
        ///        "amountFrom": "100",
        ///        "personalNumber": "0101..",
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="400">Please fill information about Customer</response>
        /// <response code="400">Failed to create transaction, your limit is reached</response>
        /// <response code="400">Failed to create transaction</response>
        [HttpPost("exchange")]
        public async Task<IActionResult> CreateTransaction(ExchangeDto dto)
        {
            return HandleResult(await Mediator.Send(new Exchange.Command { ExchangeDto = dto }));
        }
        
        
        #endregion
    }
}