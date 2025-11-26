using Infrastructure.Entities.Users.GymUsers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Configurations
{
    public class TrainerConfiguration : GymUserConfiguration<Trainer>, IEntityTypeConfiguration<Trainer>
    {
        public new void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.Property(t => t.BasicSalary)
                .HasPrecision(10, 2);

            builder.Property(t => t.HireDate)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(t => t.AnnualLeaveBalanceDays)
                .HasDefaultValue(0);

            base.Configure(builder);
        }
    }
}
