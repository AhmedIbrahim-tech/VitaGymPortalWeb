namespace Infrastructure.Repositories.Classes
{
    public class SessionRepository : GenericRepository<Session>,ISessionRepository
    {
        private readonly GymDbContext _context;
        public SessionRepository(GymDbContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Session> GetAllSessionsWithTrainerAndCategory()
        {
           return _context.Sessions
                .Include(s => s.Trainer)
                .Include(s => s.Category)
                .ToList();
        }

        public int GetCountOfBookedSlots(int sessionId)
        {
            return _context.Bookings
                .Count(b => b.SessionId == sessionId);
        }

        public Session GetSessionWithTrainerAndCategory(int sessionID)
        {
            return _context.Sessions
                .Include(s => s.Trainer)
                .Include(s => s.Category)
                .FirstOrDefault(x =>x.Id ==sessionID);
        }

    }
}
