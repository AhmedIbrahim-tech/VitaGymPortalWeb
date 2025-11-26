using Infrastructure.Entities.Users.GymUsers;

namespace Infrastructure.Repositories.Classes;

public class MemberRepository(ApplicationDbContext _context) : GenericRepository<Member>(_context), IMemberRepository
{
}
