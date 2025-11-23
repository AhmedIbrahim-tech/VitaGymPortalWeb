namespace Infrastructure.Repositories.Classes;

public class PlanRepository(ApplicationDbContext _context) : GenericRepository<Plan>(_context), IPlanRepository
{
}
