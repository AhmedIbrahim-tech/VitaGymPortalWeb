namespace Core.Services.Interfaces;

public interface IMembershipService
{
	Task<IEnumerable<MemberShipViewModel>> GetAllMemberShipsAsync(CancellationToken cancellationToken = default);
	Task<bool> CreateMembershipAsync(CreateMembershipViewModel createdMemberShip, CancellationToken cancellationToken = default);
	Task<bool> DeleteMemberShipAsync(int membershipId, CancellationToken cancellationToken = default);
	Task<IEnumerable<PlanSelectListViewModel>> GetPlansForDropDownAsync(CancellationToken cancellationToken = default);
	Task<IEnumerable<MemberSelectListViewModel>> GetMembersForDropDownAsync(CancellationToken cancellationToken = default);
}
