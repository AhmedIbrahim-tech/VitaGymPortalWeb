using Infrastructure.Entities;

namespace Infrastructure.Repositories.Interfaces
{
	public interface IMembershipRepository : IGenericRepository<MemberShip>
	{
		IEnumerable<MemberShip> GetAllMembershipsWithMemberAndPlan(Func<MemberShip, bool> predicate);
	}
}
