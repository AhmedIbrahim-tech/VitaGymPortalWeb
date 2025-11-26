using Infrastructure.Entities.Sessions;

namespace Infrastructure.Repositories.Classes;

public class BookingRepository(ApplicationDbContext _context) : GenericRepository<Booking>(_context), IBookingRepository
{
    public async Task<IEnumerable<Booking>> GetBySessionIdAsync(int sessionId, CancellationToken cancellationToken = default)
	{
		return await _context.Bookings
			.Include(x => x.Member)
			.AsNoTracking()
			.Where(x => x.SessionId == sessionId)
			.ToListAsync(cancellationToken);
	}
}
