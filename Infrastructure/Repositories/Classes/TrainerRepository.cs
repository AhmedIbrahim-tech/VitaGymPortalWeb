using Infrastructure.Entities.Users;

namespace Infrastructure.Repositories.Classes;

public class TrainerRepository(ApplicationDbContext _context) : GenericRepository<Trainer>(_context), ITrainerRepository
{
}
