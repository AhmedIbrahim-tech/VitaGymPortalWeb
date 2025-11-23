namespace Core.Services.Interfaces;

public interface IMembershipService
{
	Task<IEnumerable<MemberShipViewModel>> GetAllMemberShipsAsync(CancellationToken cancellationToken = default);
	Task<bool> CreateMembershipAsync(CreateMemberShipViewModel createdMemberShip, CancellationToken cancellationToken = default);
	Task<bool> DeleteMemberShipAsync(int memberId, CancellationToken cancellationToken = default);
	Task<IEnumerable<PlanSelectListViewModel>> GetPlansForDropDownAsync(CancellationToken cancellationToken = default);
	Task<IEnumerable<MemberSelectListViewModel>> GetMembersForDropDownAsync(CancellationToken cancellationToken = default);
}
