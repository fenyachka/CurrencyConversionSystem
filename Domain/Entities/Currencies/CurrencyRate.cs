using System;

namespace Domain.Entities.Currencies
{
    public class CurrencyRate : BaseEntity<int>
    {
        
        public Currency Currency { get; set; }
        public decimal Buy { get; set; }
        public decimal Sell { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}