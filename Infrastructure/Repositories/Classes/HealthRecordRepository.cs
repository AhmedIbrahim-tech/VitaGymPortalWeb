namespace Infrastructure.Repositories.Classes;

public class HealthRecordRepository(ApplicationDbContext _context) : GenericRepository<HealthRecord>(_context), IHealthRecordRepository
{
}
