namespace Infrastructure.Data.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.CategoryName)
                .HasMaxLength(20)
                .HasColumnType("varchar");
            
        }
    }
}
