namespace Infrastructure.Data.Configurations;

public class HealthRecordConfiguration : IEntityTypeConfiguration<HealthRecord>
{
    public void Configure(EntityTypeBuilder<HealthRecord> builder)
    {
        builder.Property(h => h.Height)
            .HasPrecision(5, 2);

        builder.Property(h => h.Weight)
            .HasPrecision(5, 2);

        builder.Property(h => h.BloodType)
            .HasMaxLength(10)
            .HasColumnType("varchar");

        builder.Property(h => h.Note)
            .HasMaxLength(500)
            .HasColumnType("varchar");

        builder.Property(h => h.PhotoUrl)
            .HasMaxLength(500)
            .HasColumnType("varchar");

        builder.HasIndex(h => h.MemberId)
            .IsUnique();
    }
}

