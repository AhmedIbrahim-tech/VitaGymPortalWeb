using Infrastructure.Entities;

namespace Infrastructure.Data.Configurations
{
    public class HealthRecordConfiguration : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(EntityTypeBuilder<HealthRecord> builder)
        {
            builder.Ignore(hr=>hr.CreatedAt);
            builder.Ignore(hr => hr.UpdatedAt);

        }
    }
   
}
