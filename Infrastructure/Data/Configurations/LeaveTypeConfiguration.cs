namespace Infrastructure.Data.Configurations;

public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
{
    public void Configure(EntityTypeBuilder<LeaveType> builder)
    {
        builder.Property(l => l.Name)
            .HasMaxLength(50)
            .HasColumnType("varchar");

        builder.ToTable(x => x.HasCheckConstraint(
            "LeaveType_AnnualAllowanceCheck",
            "AnnualAllowanceDays >= 0"
        ));
    }
}

