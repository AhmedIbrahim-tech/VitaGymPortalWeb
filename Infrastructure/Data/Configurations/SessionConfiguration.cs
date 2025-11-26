using Infrastructure.Entities.Sessions;

namespace Infrastructure.Data.Configurations
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.Property(s => s.Title)
                .HasMaxLength(100)
                .HasColumnType("varchar");

            builder.Property(s => s.Description)
                .HasMaxLength(500)
                .HasColumnType("varchar");

            builder.Property(s => s.ImageUrl)
                .HasMaxLength(500)
                .HasColumnType("varchar");

            builder.HasOne(s => s.Trainer)
                .WithMany(t => t.Sessions)
                .HasForeignKey(s => s.TrainerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Category)
                .WithMany(c => c.Sessions)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable(x =>
            {
                x.HasCheckConstraint("Session_CapacityCheck", "Capacity between 1 and 100");
                x.HasCheckConstraint("Session_EndTimeCheck", "StartDate < EndDate");
            });
        }
    }
}
