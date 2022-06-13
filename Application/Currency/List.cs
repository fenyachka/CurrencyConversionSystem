using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Currency.DTO;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Currency
{
    public class List
    {
        public class Query : IRequest<Result<List<CurrencyToReturnDto>>>
        { }
        public class Handler : IRequestHandler<Query, Result<List<CurrencyToReturnDto>>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Result<List<CurrencyToReturnDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = await _unitOfWork.Currency.TableNoTracking.ToListAsync(cancellationToken: cancellationToken);
                
                var result = new List<CurrencyToReturnDto>(query
                    .Select(_mapper.Map<Domain.Entities.Currencies.Currency, CurrencyToReturnDto>));

                return Result<List<CurrencyToReturnDto>>.Success(result);
            }
        }
    }
}