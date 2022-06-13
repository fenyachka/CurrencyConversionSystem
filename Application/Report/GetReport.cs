using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Helper;
using Application.Report.DTO;
using Application.Report.Validators;
using Application.Services;
using AutoMapper;
using Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Report
{
    public class GetReport
    {
        public class Query : IRequest<Result<ReportToReturnDto>>
        {
            public GetReportDto GetReportDto { get; set; }
        }
        
        public class QueryValidator : AbstractValidator<GetReport.Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.GetReportDto).SetValidator(new GetReportDtoValidator());
            }
        }
        public class Handler : IRequestHandler<Query, Result<ReportToReturnDto>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IApplicationService _applicationService;
            private readonly IMapper _mapper;
        
            public Handler(IUnitOfWork unitOfWork, IApplicationService applicationService, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _applicationService = applicationService;
                _mapper = mapper;
            }
        
            public async Task<Result<ReportToReturnDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var customerExist = await _unitOfWork.Customer.TableNoTracking.FirstOrDefaultAsync(c =>
                        c.PersonalNumber == request.GetReportDto.PersonalNumber, cancellationToken: cancellationToken);
                if(customerExist == null)
                    return Result<ReportToReturnDto>.Failure("Customer doesn't exist");
                
                var transactionCount = await _applicationService.GetDailyTransactionCount(request.GetReportDto.PersonalNumber, request.GetReportDto.StartDate,
                    request.GetReportDto.EndDate);
                
                var customers = await _applicationService.GetCustomers();
                var root = customers.GenerateTree(c => c.PersonalNumber, 
                    c => c.RecommenderPersonalNumber, request.GetReportDto.PersonalNumber);

                var allTransactionCount = await _applicationService.AllTransactionCount(root, request.GetReportDto.StartDate, request.GetReportDto.EndDate);

                return Result<ReportToReturnDto>.Success(new ReportToReturnDto
                {
                    PersonalNumber = request.GetReportDto.PersonalNumber,
                    TransactionCount = transactionCount,
                    AllTransactionCount = allTransactionCount
                });
            }
        }
    }
}