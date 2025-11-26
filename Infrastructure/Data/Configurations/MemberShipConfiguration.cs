namespace Infrastructure.Data.Configurations;

public class MemberShipConfiguration : IEntityTypeConfiguration<MemberShip>
{
    public void Configure(EntityTypeBuilder<MemberShip> builder)
    {
        builder.HasOne(ms => ms.Member)
            .WithMany(m => m.MemberShips)
            .HasForeignKey(ms => ms.MemberId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ms => ms.Plan)
            .WithMany(p => p.MemberShips)
            .HasForeignKey(ms => ms.PlanId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(x => x.HasCheckConstraint(
            "MemberShip_DateCheck",
            "StartDate < EndDate"
        ));
    }
}

