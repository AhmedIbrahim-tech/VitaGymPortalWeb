using Infrastructure.Entities.Users.GymUsers;

namespace Infrastructure.Repositories.Classes;

public class TrainerRepository(ApplicationDbContext _context) : GenericRepository<Trainer>(_context), ITrainerRepository
{
}
