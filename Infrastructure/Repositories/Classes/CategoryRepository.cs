using Infrastructure.Entities.Sessions;

namespace Infrastructure.Repositories.Classes;

public class CategoryRepository(ApplicationDbContext _context) : GenericRepository<Category>(_context), ICategoryRepository
{
}
