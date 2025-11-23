using Core.Mappers;
using Infrastructure.Entities.Users;

namespace Core.Services.Classes;

public class MembershipService(IUnitOfWork _unitOfWork, IMembershipRepository _membershipRepository) : IMembershipService
{
    #region Create Membership

    public async Task<bool> CreateMembershipAsync(CreateMemberShipViewModel createdMemberShip, CancellationToken cancellationToken = default)
    {
        try
        {
            if (!await IsMemberExistsAsync(createdMemberShip.MemberId, cancellationToken) ||
                !await IsPlanExistsAsync(createdMemberShip.PlanId, cancellationToken) ||
                await HasActiveMemberShipAsync(createdMemberShip.MemberId, cancellationToken))
            {
                return false;
            }

            var memberShipToCreate = createdMemberShip.ToMemberShip();
            var plan = await _unitOfWork.GetRepository<Plan>().GetByIDAsync(createdMemberShip.PlanId, cancellationToken);
            memberShipToCreate.EndDate = DateTime.Now.AddDays(plan!.DurationDays);

            await _unitOfWork.GetRepository<MemberShip>().AddAsync(memberShipToCreate, cancellationToken);
            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }
        catch
        {
            return false;
        }
    }

    #endregion

    #region Delete Membership

    public async Task<bool> DeleteMemberShipAsync(int memberId, CancellationToken cancellationToken = default)
    {
        var repo = _unitOfWork.GetRepository<MemberShip>();
        var memberships = await repo.GetAllAsync(x => x.MemberId == memberId && x.EndDate >= DateTime.Now, cancellationToken);
        var activeMembership = memberships.FirstOrDefault();

        if (activeMembership is null)
        {
            return false;
        }

        repo.Delete(activeMembership);
        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }

    #endregion

    #region Get All Memberships

    public async Task<IEnumerable<MemberShipViewModel>> GetAllMemberShipsAsync(CancellationToken cancellationToken = default)
    {
        var memberShips = await _membershipRepository.GetAllMembershipsWithMemberAndPlanAsync(
            x => x.EndDate >= DateTime.Now, cancellationToken);
        var memberShipsList = memberShips.ToList();

        if (!memberShipsList.Any())
        {
            return [];
        }

        return memberShipsList.Select(m => m.ToMemberShipViewModel());
    }

    #endregion

    #region Get Plans For Dropdown

    public async Task<IEnumerable<PlanSelectListViewModel>> GetPlansForDropDownAsync(CancellationToken cancellationToken = default)
    {
        var plans = await _unitOfWork.GetRepository<Plan>().GetAllAsync(x => x.IsActive == true, cancellationToken);
        return plans.Select(p => p.ToPlanSelectListViewModel());
    }

    #endregion

    #region Get Members For Dropdown

    public async Task<IEnumerable<MemberSelectListViewModel>> GetMembersForDropDownAsync(CancellationToken cancellationToken = default)
    {
        var members = await _unitOfWork.GetRepository<Member>().GetAllAsync(null, cancellationToken);
        return members.Select(m => m.ToMemberSelectListViewModel());
    }

    #endregion

    #region Helper Methods

    private async Task<bool> IsMemberExistsAsync(int memberId, CancellationToken cancellationToken = default)
    {
        var members = await _unitOfWork.GetRepository<Member>().GetAllAsync(x => x.Id == memberId, cancellationToken);
        return members.Any();
    }

    private async Task<bool> IsPlanExistsAsync(int planId, CancellationToken cancellationToken = default)
    {
        var plans = await _unitOfWork.GetRepository<Plan>().GetAllAsync(x => x.Id == planId, cancellationToken);
        return plans.Any();
    }

    private async Task<bool> HasActiveMemberShipAsync(int memberId, CancellationToken cancellationToken = default)
    {
        var memberships = await _unitOfWork.GetRepository<MemberShip>().GetAllAsync(
            x => x.MemberId == memberId && x.EndDate >= DateTime.Now, cancellationToken);
        return memberships.Any();
    }

    #endregion
}
