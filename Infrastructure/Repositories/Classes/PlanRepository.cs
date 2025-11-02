using Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Classes
{
    public class PlanRepository : IPlanRepository
    {
        private readonly GymDbContext _context;
        public PlanRepository(GymDbContext context)
        {
            _context = context;
        }
        public int Add(Plan plan)
        {
            _context.Add(plan);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            var Plan = _context.Plans.Find(id);
            if (Plan != null)
            {
                _context.Plans.Remove(Plan);
                return _context.SaveChanges();
            }
            return 0;
        }

        public IEnumerable<Plan> GetAll() => _context.Plans.ToList();

        public Plan? GetByID(int id) => _context.Plans.Find(id);

        public int Update(Plan plan)
        {
            _context.Update(plan);
            return _context.SaveChanges();
        }
    }
}
