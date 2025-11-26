using Infrastructure.Entities.Membership;

namespace Infrastructure.Repositories.Interfaces;

public interface IMembershipRepository : IGenericRepository<MemberShip>
{
	Task<IEnumerable<MemberShip>> GetAllMembershipsWithMemberAndPlanAsync(Expression<Func<MemberShip, bool>> predicate, CancellationToken cancellationToken = default);
	Task<MemberShip?> GetMembershipByIdWithMemberAndPlanAsync(int id, CancellationToken cancellationToken = default);
}
