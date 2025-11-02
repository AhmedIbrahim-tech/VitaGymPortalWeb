

namespace Infrastructure.Repositories.Classes
{
	public class MembershipRepository : GenericRepository<MemberShip>, IMembershipRepository
	{
		private readonly GymDbContext _dbContext;

		public MembershipRepository(GymDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}

		public IEnumerable<MemberShip> GetAllMembershipsWithMemberAndPlan(Func<MemberShip, bool> predicate)
		{
			return _dbContext.MemberShips.Include(X => X.Plan).Include(X => X.Member).Where(predicate).ToList();
		}
	}
}
