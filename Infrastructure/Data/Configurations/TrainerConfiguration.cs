using Infrastructure.Entities.Users;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Data.Configurations
{
    public class TrainerConfiguration : GymUserConfiguration<Trainer>,IEntityTypeConfiguration<Trainer>
    {
        public new void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.Property(t => t.CreatedAt)
               .HasColumnName("HireDate")
               .HasDefaultValueSql("GETDATE()");

            base.Configure(builder);
        }
    }
   
}
