using Infrastructure.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface ISessionRepository 
    {
        IEnumerable<Session> GetAllSessionsWithTrainerAndCategory();

        Session GetSessionWithTrainerAndCategory(int sessionID);

        int GetCountOfBookedSlots(int sessionId);
    }
}
