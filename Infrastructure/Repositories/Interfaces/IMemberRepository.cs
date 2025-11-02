using Infrastructure.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IMemberRepository : IGenericRepository<Member>
    {
        IEnumerable<Session> GetAllSessions();        

    }
}
