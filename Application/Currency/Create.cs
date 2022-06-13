using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Currency.DTO;
using Application.Currency.Validators;
using AutoMapper;
using Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace Application.Currency
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public CreateCurrencyDto CreateCurrencyDto { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.CreateCurrencyDto).SetValidator(new CreateCurrencyDtoValidator());
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
                var existingCheck = _unitOfWork.Currency.TableNoTracking.FirstOrDefault(c =>
                    c.Code.Equals(request.CreateCurrencyDto.Code));

                if (existingCheck != null)
                    return Result<Unit>.Failure("Currency with same Code already exist.");
                
                var currency = _mapper.Map<CreateCurrencyDto, Domain.Entities.Currencies.Currency>(request.CreateCurrencyDto);

                _unitOfWork.Currency.Add(currency);

                var result = await _unitOfWork.SaveAsync(cancellationToken) > 0;

                return !result ? Result<Unit>.Failure("Failed to create currency") : Result<Unit>.Success(Unit.Value);
            }
        }
    }
}