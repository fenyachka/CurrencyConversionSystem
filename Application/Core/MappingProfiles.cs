using Application.Currency.DTO;
using AutoMapper;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateCurrencyDto, Domain.Entities.Currencies.Currency>();
            CreateMap<Domain.Entities.Currencies.Currency, CurrencyToReturnDto>();
        }
    }
}