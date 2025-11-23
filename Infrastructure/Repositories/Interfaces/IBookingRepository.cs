namespace Infrastructure.Repositories.Interfaces;

public interface IBookingRepository : IGenericRepository<Booking>
{
	/// <summary>
	/// Get all bookings for a specific session with member information
	/// </summary>
	Task<IEnumerable<Booking>> GetBySessionIdAsync(int sessionId, CancellationToken cancellationToken = default);
}
