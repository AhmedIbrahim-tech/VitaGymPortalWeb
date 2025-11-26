using Infrastructure.Entities.HumanResources;

namespace Infrastructure.Data.Configurations
{
    public class TrainerPayrollConfiguration : IEntityTypeConfiguration<TrainerPayroll>
    {
        public void Configure(EntityTypeBuilder<TrainerPayroll> builder)
        {
            builder.Property(p => p.GrossAmount)
                .HasPrecision(10, 2);

            builder.Property(p => p.NetAmount)
                .HasPrecision(10, 2);

            builder.HasOne(p => p.Trainer)
                .WithMany(t => t.Payrolls)
                .HasForeignKey(p => p.TrainerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(x => x.HasCheckConstraint(
                "TrainerPayroll_PeriodCheck",
                "PeriodStart < PeriodEnd"
            ));
        }
    }
}

