namespace Application.CurrencyRate.DTO
{
    public class CurrencyRateToReturnDto
    {
        public CurrencyRateToReturnDto(int currencyRateId, decimal exchangeRate, string currencyRateBaseCurrency,
            string currencyRatePriceCurrency)
        {
            Id = currencyRateId;
            Rate = exchangeRate;
            PriceCurrency = currencyRatePriceCurrency;
            BaseCurrency = currencyRateBaseCurrency;
        }

        public int Id { get; set; }
        public string BaseCurrency { get; set; }
        public string PriceCurrency { get; set; }
        public decimal Rate { get; set; }
    }
}