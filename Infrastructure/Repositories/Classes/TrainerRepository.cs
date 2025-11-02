namespace Infrastructure.Repositories.Classes
{
    public class TrainerRepository :ITrainerRepository
    {
        private readonly GymDbContext _context;
        public TrainerRepository(GymDbContext context)
        {
            _context = context;
        }
        public int Add(Trainer trainer)
        {
            _context.Add(trainer);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            var Trainer = _context.Trainers.Find(id);
            if (Trainer != null)
            {
                _context.Trainers.Remove(Trainer);
                return _context.SaveChanges();
            }
            return 0;
        }

        public IEnumerable<Trainer> GetAll() => _context.Trainers.ToList();

        public Trainer? GetByID(int id) => _context.Trainers.Find(id);

        public int Update(Trainer trainer)
        {
            _context.Update(trainer);
            return _context.SaveChanges();
        }
    }
}
