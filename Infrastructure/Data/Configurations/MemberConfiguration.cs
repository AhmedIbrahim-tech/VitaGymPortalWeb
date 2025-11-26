namespace Infrastructure.Data.Configurations;

public class MemberConfiguration : GymUserConfiguration<Member>, IEntityTypeConfiguration<Member>
{
    public new void Configure(EntityTypeBuilder<Member> builder)
    {
        // Configure 1:1 relationship with HealthRecord
        builder.HasOne(m => m.HealthRecord)
            .WithOne(h => h.Member)
            .HasForeignKey<HealthRecord>(h => h.MemberId)
            .OnDelete(DeleteBehavior.Cascade);

        base.Configure(builder);
    }
}

