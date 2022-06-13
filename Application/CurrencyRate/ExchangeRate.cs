using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.CurrencyRate.DTO;
using Application.CurrencyRate.Validators;
using Application.Services;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Application.CurrencyRate
{
    public class ExchangeRate
    {
        public class Query : IRequest<Result<CurrencyRateWithAmount>>
        {
            public ExchangeRateDto ExchangeRateDto { get; set; }
        }
        
        public class QueryValidator : AbstractValidator<ExchangeRate.Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.ExchangeRateDto).SetValidator(new ExchangeRateValidator());
            }
        }
        public class Handler : IRequestHandler<Query, Result<CurrencyRateWithAmount>>
        {
            private readonly IApplicationService _applicationService;
            private readonly IMapper _mapper;
        
            public Handler(IApplicationService applicationService, IMapper mapper)
            {
                _applicationService = applicationService;
                _mapper = mapper;
            }
        
            public async Task<Result<CurrencyRateWithAmount>> Handle(Query request, CancellationToken cancellationToken)
            {
                var currencyRateFrom = await _applicationService.GetCurrencyRate(request.ExchangeRateDto.FromCurrency);
                if (currencyRateFrom == null)
                    return Result<CurrencyRateWithAmount>.Failure("Currency doesn't exist");
                
                var currencyRateTo = await _applicationService.GetCurrencyRate(request.ExchangeRateDto.ToCurrency);
                if (currencyRateTo == null)
                    return Result<CurrencyRateWithAmount>.Failure("Currency doesn't exist");

                var amountTo = currencyRateFrom.Sell / currencyRateTo.Buy * request.ExchangeRateDto.AmountFrom;
                
                return Result<CurrencyRateWithAmount>.Success(new CurrencyRateWithAmount()
                {
                    CurrencyFrom = request.ExchangeRateDto.FromCurrency,
                    CurrencyTo = request.ExchangeRateDto.ToCurrency,
                    AmountFrom = request.ExchangeRateDto.AmountFrom,
                    AmountTo = amountTo
                });
            }
        }
    }
}