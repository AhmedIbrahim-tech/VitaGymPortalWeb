namespace Infrastructure.Data.Configurations
{
    public class GymUserConfiguration<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(u => u.Name)
                .HasMaxLength(50)
                .HasColumnType("varchar");

            builder.Property(u => u.Email)
                .HasMaxLength(100)
                .HasColumnType("varchar");

            builder.Property(u => u.Phone)
                .HasMaxLength(11)
                .HasColumnType("varchar");

            builder.Property(u => u.PhotoUrl)
                .HasMaxLength(500)
                .HasColumnType("varchar");

            builder.OwnsOne(u => u.Address, address =>
            {
                address.Property(a => a.Street)
                    .HasMaxLength(100)
                    .HasColumnType("varchar")
                    .HasColumnName("Street");
                address.Property(a => a.City)
                    .HasMaxLength(50)
                    .HasColumnType("varchar")
                    .HasColumnName("City");
                address.Property(a => a.BuildingNumber)
                    .HasMaxLength(20)
                    .HasColumnType("varchar")
                    .HasColumnName("BuildingNumber");
            });

            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.Phone).IsUnique();

            builder.ToTable(x => x.HasCheckConstraint("GymUser_Email_Check", "Email LIKE '%_@__%.__%'"));
            builder.ToTable(x =>
                x.HasCheckConstraint(
                    "GymUser_Phone_Check",
                    "(Phone LIKE '010%' OR Phone LIKE '011%' OR Phone LIKE '012%' OR Phone LIKE '015%') " +
                    "AND Phone NOT LIKE '%[^0-9]%'"
                )
            );
        }
    }
}
