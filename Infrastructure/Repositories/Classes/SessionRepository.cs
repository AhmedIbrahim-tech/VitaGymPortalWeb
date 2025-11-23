namespace Infrastructure.Repositories.Classes;

public class SessionRepository(ApplicationDbContext _context) : GenericRepository<Session>(_context), ISessionRepository
{
    public async Task<IEnumerable<Session>> GetAllSessionsWithTrainerAndCategoryAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Sessions
            .Include(s => s.Trainer)
            .Include(s => s.Category)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Session?> GetSessionWithTrainerAndCategoryAsync(int sessionId, CancellationToken cancellationToken = default)
    {
        return await _context.Sessions
            .Include(s => s.Trainer)
            .Include(s => s.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == sessionId, cancellationToken);
    }

    public async Task<int> GetCountOfBookedSlotsAsync(int sessionId, CancellationToken cancellationToken = default)
    {
        return await _context.Bookings
            .CountAsync(b => b.SessionId == sessionId, cancellationToken);
    }
}
