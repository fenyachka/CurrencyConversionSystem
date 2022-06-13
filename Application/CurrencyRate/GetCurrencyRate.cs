using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Services;
using MediatR;

namespace Application.CurrencyRate
{
    public class GetCurrencyRate
    {
        public class Query : IRequest<Result<Domain.Entities.Currencies.CurrencyRate>>
        {
            public string CurrencyCode { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<Domain.Entities.Currencies.CurrencyRate>>
        {
            private readonly IApplicationService _applicationService;
        
            public Handler(IApplicationService applicationService)
            {
                _applicationService = applicationService;
            }
        
            public async Task<Result<Domain.Entities.Currencies.CurrencyRate>> Handle(Query request, CancellationToken cancellationToken)
            {
                var currencyRate = await _applicationService.GetCurrencyRate(request.CurrencyCode);
                
                return currencyRate == null
                    ? Result<Domain.Entities.Currencies.CurrencyRate>.Failure($"Couldn't retrieve rate from {request.CurrencyCode}")
                    : Result<Domain.Entities.Currencies.CurrencyRate>.Success(currencyRate);
            }
        }
    }
}