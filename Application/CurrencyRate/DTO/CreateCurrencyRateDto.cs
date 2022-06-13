namespace Application.CurrencyRate.DTO
{
    public class CreateCurrencyRateDto
    {
        public int CurrencyId { get; set; }
        public decimal Buy { get; set; }
        public decimal Sell { get; set; }
    }
}