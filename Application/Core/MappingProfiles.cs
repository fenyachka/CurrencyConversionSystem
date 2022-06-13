using Application.Currency.DTO;
using Application.CurrencyRate.DTO;
using AutoMapper;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateCurrencyDto, Domain.Entities.Currencies.Currency>();
            CreateMap<Domain.Entities.Currencies.Currency, CurrencyToReturnDto>();
            
            CreateMap<Domain.Entities.Currencies.CurrencyRate, CurrencyRateToReturnDto>();
            CreateMap<CreateCurrencyRateDto, Domain.Entities.Currencies.CurrencyRate>();
        }
    }
}