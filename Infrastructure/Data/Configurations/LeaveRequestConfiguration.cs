namespace Infrastructure.Data.Configurations;

public class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
{
    public void Configure(EntityTypeBuilder<LeaveRequest> builder)
    {
        builder.Property(l => l.Reason)
            .HasMaxLength(500)
            .HasColumnType("varchar");

        builder.Property(l => l.AdminComment)
            .HasMaxLength(500)
            .HasColumnType("varchar");

        builder.HasOne(l => l.Trainer)
            .WithMany(t => t.LeaveRequests)
            .HasForeignKey(l => l.TrainerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(l => l.LeaveType)
            .WithMany(lt => lt.LeaveRequests)
            .HasForeignKey(l => l.LeaveTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(x => x.HasCheckConstraint(
            "LeaveRequest_DateCheck",
            "StartDate <= EndDate"
        ));

        builder.ToTable(x => x.HasCheckConstraint(
            "LeaveRequest_TotalDaysCheck",
            "TotalDays > 0"
        ));
    }
}

