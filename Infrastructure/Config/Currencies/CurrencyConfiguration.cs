using Domain.Entities.Currencies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config.Currencies
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.Property(p => p.Code).IsRequired().HasColumnType("varchar(3)");
            builder.Property(p => p.NameGeo).IsRequired().HasColumnType("nvarchar(250)");
            builder.Property(p => p.NameLat).IsRequired().HasColumnType("varchar(250)");
        }
    }
}