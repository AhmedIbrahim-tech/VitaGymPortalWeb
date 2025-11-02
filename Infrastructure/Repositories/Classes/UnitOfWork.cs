namespace Infrastructure.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymDbContext _context;
        private readonly Dictionary<string, object> _repositories = [];

        public UnitOfWork(GymDbContext context)
        {
            _context = context;
        }

        public ISessionRepository SessionRepository { get ; set ; }
        public IBookingRepository BookingRepository { get ; set ; }
        public IMembershipRepository MembershipRepository { get ; set ; }

        public IGenericRepository<TEnity> GetRepository<TEnity>() where TEnity : BaseEntity
        {
            string typeName = typeof(TEnity).Name;
            if(_repositories.TryGetValue(typeName, out object? repository))
            {
                return (IGenericRepository<TEnity>)repository;
            }
            var newRepository = new GenericRepository<TEnity>(_context);
            _repositories.Add(typeName, newRepository);
            return newRepository;

        }

        public int SaveChanges() => _context.SaveChanges();
        
    }
}
