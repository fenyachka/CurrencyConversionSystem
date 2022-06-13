using Domain.Entities.Transaction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config.Transactions
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.Property(p => p.FromAmount).IsRequired().HasColumnType("decimal(18, 4)");;
            builder.Property(p => p.ToAmount).IsRequired().HasColumnType("decimal(18, 4)");;
        }
    }
}