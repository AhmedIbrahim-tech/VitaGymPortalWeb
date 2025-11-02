using Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Classes
{
    public class HealthRecordRepository : IHealthRecordRepository
    {
        private readonly GymDbContext _context;
        public HealthRecordRepository(GymDbContext context)
        {
            _context = context;
        }
        public int Add(HealthRecord healthRecord)
        {
            _context.Add(healthRecord);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            var HealthRecord = _context.HealthRecords.Find(id);
            if (HealthRecord != null)
            {
                _context.HealthRecords.Remove(HealthRecord);
                return _context.SaveChanges();
            }
            return 0;
        }

        public IEnumerable<HealthRecord> GetAll() => _context.HealthRecords.ToList();

        public HealthRecord? GetByID(int id) => _context.HealthRecords.Find(id);

        public int Update(HealthRecord healthRecord)
        {
            _context.Update(healthRecord);
            return _context.SaveChanges();
        }

    }
}
