using Application.Customer.DTO;
using FluentValidation;

namespace Application.Customer.Vallidators
{
    public class CreateCustomerDtoDtoValidator : AbstractValidator<CreateCustomerDto>
    {
        public CreateCustomerDtoDtoValidator()
        {
            RuleFor(p => p.FirstName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(p => p.LastName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(p => p.PersonalNumber).NotNull().NotEmpty().MaximumLength(11);
            RuleFor(p => p.RecommenderPersonalNumber).NotNull().NotEmpty().MaximumLength(11);

        }
    }
}