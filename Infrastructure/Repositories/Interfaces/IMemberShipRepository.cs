namespace Infrastructure.Repositories.Interfaces;

public interface IMembershipRepository : IGenericRepository<MemberShip>
{
	Task<IEnumerable<MemberShip>> GetAllMembershipsWithMemberAndPlanAsync(Expression<Func<MemberShip, bool>> predicate, CancellationToken cancellationToken = default);
}
