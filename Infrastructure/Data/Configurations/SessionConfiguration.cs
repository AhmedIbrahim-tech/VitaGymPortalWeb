using Infrastructure.Entities;

namespace Infrastructure.Data.Configurations
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
           
            builder.HasOne(s => s.Trainer)
                .WithMany(t => t.Sessions)
                .HasForeignKey(s => s.TrainerId);

            builder.HasOne(s => s.Category)
                .WithMany(c => c.Sessions)
                .HasForeignKey(s => s.CategoryId);

            builder.ToTable(x =>
            {
                x.HasCheckConstraint("Session_CapacityCheck", "Capacity between 1 and 25 ");
                x.HasCheckConstraint("Session_EndTimeCheck", "StartDate < EndDate"); 
            });

        }
    }
    
}
