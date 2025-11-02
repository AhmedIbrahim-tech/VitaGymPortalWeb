using Infrastructure.Entities;

namespace Infrastructure.Data.Configurations
{
    public class MemberShipConfiguration : IEntityTypeConfiguration<MemberShip>
    {
        public void Configure(EntityTypeBuilder<MemberShip> builder)
        {
            builder.Ignore(b => b.Id);

            builder.Property(b => b.CreatedAt)
                .HasColumnName("StartDate")
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(b => b.Member)
                .WithMany(s => s.MemberPlans)
                .HasForeignKey(b => b.MemberId);

            builder.HasOne(b => b.Plan)
                .WithMany(m => m.PlanMembers)
                .HasForeignKey(b => b.PlanId);

            builder.HasKey(b => new { b.MemberId, b.PlanId });
        }
    }
}
