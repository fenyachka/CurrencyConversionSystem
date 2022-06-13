using Domain.Entities.Currencies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config.Currencies
{
    public class CurrencyRateConfiguration : IEntityTypeConfiguration<CurrencyRate>
    {
        public void Configure(EntityTypeBuilder<CurrencyRate> builder)
        {
            builder.Property(p => p.Buy).IsRequired().HasColumnType("decimal(18, 2)");
            builder.Property(p => p.Sell).IsRequired().HasColumnType("decimal(18, 2)");
        }
    }
}