using Infrastructure.Entities.Membership;

namespace Infrastructure.Data.Configurations
{
    public class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(p => p.Name)
                .HasMaxLength(50)
                .HasColumnType("varchar");

            builder.Property(p => p.Description)
                .HasMaxLength(500)
                .HasColumnType("varchar");

            builder.Property(p => p.Price)
                .HasPrecision(10, 2);

            builder.Property(p => p.ImageUrl)
                .HasMaxLength(500)
                .HasColumnType("varchar");

            builder.ToTable(x => x.HasCheckConstraint("Plan_DurationCheck", "DurationDays between 1 and 365"));
        }
    }
}
