using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.CurrencyRate.DTO;
using Application.CurrencyRate.Validators;
using Application.Helper;
using Application.Services;
using Domain.Entities.Transaction;
using Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.CurrencyRate
{
    public class Exchange
    {
        public class Command : IRequest<Result<Unit>>
        {
            public ExchangeDto ExchangeDto { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.ExchangeDto).SetValidator(new ExchangeValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IApplicationService _applicationService;
            private readonly IUnitOfWork _unitOfWork;
            private readonly AmountConfiguration _amountConfiguration;

            public Handler(IApplicationService applicationService, IUnitOfWork unitOfWork, IOptions<AmountConfiguration> amountConfiguration)
            {
                _applicationService = applicationService;
                _unitOfWork = unitOfWork;
                _amountConfiguration = amountConfiguration.Value;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var currencyRateFrom = await _applicationService.GetCurrencyRate(request.ExchangeDto.FromCurrency);
                if (currencyRateFrom == null)
                    return Result<Unit>.Failure("Currency doesn't exist");
                
                var currencyRateTo = await _applicationService.GetCurrencyRate(request.ExchangeDto.ToCurrency);
                if (currencyRateTo == null)
                    return Result<Unit>.Failure("Currency doesn't exist");
                
                var amountTo = currencyRateFrom.Sell / currencyRateTo.Buy * request.ExchangeDto.AmountFrom;

                if (amountTo > _amountConfiguration.CheckPersonAmount)
                {
                    var customer = await _applicationService.GetCustomer(request.ExchangeDto.PersonalNumber);

                    if (customer == null)
                        return Result<Unit>.Failure("Please fill information about Customer");

                }

                var customers = await _applicationService.GetCustomers();
                var root = customers.GenerateTree(c => c.PersonalNumber, 
                    c => c.RecommenderPersonalNumber, request.ExchangeDto.PersonalNumber);
                
               var moreThanLimit = await _applicationService.CalculateDailyLimit(root);

                if (moreThanLimit)
                    return Result<Unit>.Failure("Failed to create transaction, your limit is reached");
                
                _unitOfWork.Transaction.Add(new Transaction()
                {
                    FromAmount = request.ExchangeDto.AmountFrom,
                    FromCurrency = request.ExchangeDto.FromCurrency,
                    ToCurrency = request.ExchangeDto.ToCurrency,
                    PersonalNumber = request.ExchangeDto.PersonalNumber,
                    TimeStamp = DateTime.Now
                });
                
                var result = await _unitOfWork.SaveAsync(cancellationToken) > 0;

                return !result 
                    ? Result<Unit>.Failure("Failed to create transaction") 
                    : Result<Unit>.Success(Unit.Value);
            }
        }
    }
}