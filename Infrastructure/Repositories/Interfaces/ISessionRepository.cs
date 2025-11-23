namespace Infrastructure.Repositories.Interfaces;

public interface ISessionRepository : IGenericRepository<Session>
{
    Task<IEnumerable<Session>> GetAllSessionsWithTrainerAndCategoryAsync(CancellationToken cancellationToken = default);
    Task<Session?> GetSessionWithTrainerAndCategoryAsync(int sessionId, CancellationToken cancellationToken = default);
    Task<int> GetCountOfBookedSlotsAsync(int sessionId, CancellationToken cancellationToken = default);
}
