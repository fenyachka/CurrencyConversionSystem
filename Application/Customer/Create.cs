using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Customer.DTO;
using Application.Customer.Vallidators;
using Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace Application.Customer
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public CreateCustomerDto CreateCustomerDto { get; set; }

        }
        public class CommandValidador : AbstractValidator<Command>
        {
            public CommandValidador()
            {
                RuleFor(x => x.CreateCustomerDto).SetValidator(new CreateCustomerDtoDtoValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IUnitOfWork _unitOfWork;   

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var existingCheck = _unitOfWork.Customer.Table
                    .FirstOrDefault(x => x.PersonalNumber == request.CreateCustomerDto.PersonalNumber);

                if (existingCheck != null)
                    return Result<Unit>.Failure("Customer with entered Private Number already exist.");

                _unitOfWork.Customer.Add(new Domain.Entities.Customer.Customer
                {
                    FirstName = request.CreateCustomerDto.FirstName,
                    LastName = request.CreateCustomerDto.LastName,
                    PersonalNumber = request.CreateCustomerDto.PersonalNumber,
                    RecommenderPersonalNumber = request.CreateCustomerDto.RecommenderPersonalNumber
                });

                var result = await _unitOfWork.SaveAsync(cancellationToken) > 0;

                return !result ? Result<Unit>.Failure("Failed to create customer") : Result<Unit>.Success(Unit.Value);
            }
        }
    }
}