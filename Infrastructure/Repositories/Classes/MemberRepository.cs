using Infrastructure.Contexts;

namespace Infrastructure.Repositories.Classes
{
    public class MemberRepository : GenericRepository<Member>, IMemberRepository
    {
        public MemberRepository(GymDbContext context) : base(context)
        {
        }

        public IEnumerable<Session> GetAllSessions()
        {
            throw new NotImplementedException();
        }
    }
}
