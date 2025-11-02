namespace Infrastructure.Repositories.Classes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly GymDbContext _context;

        public GenericRepository(GymDbContext context)
        {
            _context = context;
        }
        public void Add(TEntity entity) => _context.Add(entity);

        public void Delete(TEntity entity) => _context.Remove(entity);
       
        public IEnumerable<TEntity> GetAll(Func<TEntity, bool>? condition = null)
        {
            if (condition == null)
            {
                return _context.Set<TEntity>().AsNoTracking().ToList();
            }
            return _context.Set<TEntity>().AsNoTracking().Where(condition).ToList();
        }

        public TEntity? GetByID(int id) => _context.Set<TEntity>().Find(id);

        public void Update(TEntity entity) => _context.Update(entity);

    }
}
