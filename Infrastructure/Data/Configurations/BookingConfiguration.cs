namespace Infrastructure.Data.Configurations
{
    internal class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.Ignore(b => b.Id);

            builder.Property(b => b.CreatedAt)
                .HasColumnName("BookingDate")
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(b => b.Member)
                .WithMany(m => m.Bookings)
                .HasForeignKey(b => b.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.Session)
                .WithMany(s => s.Bookings)
                .HasForeignKey(b => b.SessionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(b => new { b.MemberId, b.SessionId });
        }
    }
}
