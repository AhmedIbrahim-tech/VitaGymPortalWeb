using Infrastructure.Entities;

namespace Infrastructure.Data.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.Property(p => p.Amount)
            .HasPrecision(10, 2);

        builder.Property(p => p.PaymentDate)
            .HasDefaultValueSql("GETDATE()");

        builder.Property(p => p.Method)
            .HasMaxLength(20)
            .HasColumnType("varchar");

        builder.HasOne(p => p.Member)
            .WithMany(m => m.Payments)
            .HasForeignKey(p => p.MemberId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

