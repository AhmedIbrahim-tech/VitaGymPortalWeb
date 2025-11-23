using Infrastructure.Entities.Users;

namespace Infrastructure.Repositories.Classes;

public class MemberRepository(ApplicationDbContext _context) : GenericRepository<Member>(_context), IMemberRepository
{
}
