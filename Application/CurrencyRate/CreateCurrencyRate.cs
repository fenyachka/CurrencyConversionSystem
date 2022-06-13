using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.CurrencyRate.DTO;
using Application.CurrencyRate.Validators;
using AutoMapper;
using Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace Application.CurrencyRate
{
    public class CreateCurrencyRate
    {
        public class Command : IRequest<Result<Unit>>
        {
            public CreateCurrencyRateDto CreateCurrencyRateDto { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.CreateCurrencyRateDto).SetValidator(new CreateCurrencyRateDtoValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var existingCheck = _unitOfWork.CurrencyRate.TableNoTracking.FirstOrDefault();

                if (existingCheck != null)
                {
                    return Result<Unit>.Failure("Currency Rate already exists.");
                }

                var currencyRate = _mapper.Map<CreateCurrencyRateDto, Domain.Entities.Currencies.CurrencyRate>(request.CreateCurrencyRateDto);

                _unitOfWork.CurrencyRate.Add(currencyRate);

                var result = await _unitOfWork.SaveAsync(cancellationToken) > 0;

                return !result ? Result<Unit>.Failure("Failed to create currencyRate") : Result<Unit>.Success(Unit.Value);
            }
        }
    }
}