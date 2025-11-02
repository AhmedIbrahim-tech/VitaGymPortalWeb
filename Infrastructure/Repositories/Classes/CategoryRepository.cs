using Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Classes
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly GymDbContext _context;
        public CategoryRepository(GymDbContext context)
        {
            _context = context;
        }
        public int Add(Category category)
        {
            _context.Add(category);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            var Category = _context.Categories.Find(id);
            if (Category != null)
            {
                _context.Categories.Remove(Category);
                return _context.SaveChanges();
            }
            return 0;
        }

        public IEnumerable<Category> GetAll() => _context.Categories.ToList();

        public Category? GetByID(int id) => _context.Categories.Find(id);

        public int Update(Category category)
        {
            _context.Update(category);
            return _context.SaveChanges();
        }

    }
}
