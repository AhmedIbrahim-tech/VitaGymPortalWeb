using AutoMapper;

namespace Core.Services.Classes
{
	public class MembershipService : IMembershipService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

        public IMembershipRepository _membershipRepository { get; set; }

        public MembershipService(IUnitOfWork unitOfWork, IMapper mapper,IMembershipRepository membershipRepository)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
            _membershipRepository = membershipRepository;
        }
		public bool CreateMembership(CreateMemberShipViewModel CreatedMemberShip)
		{
			try
			{
				if (!IsMemberExists(CreatedMemberShip.MemberId) || !IsPlanExists(CreatedMemberShip.PlanId)
					|| HasActiveMemberShip(CreatedMemberShip.MemberId)) return false;
				var MemberShipToCreate = _mapper.Map<MemberShip>(CreatedMemberShip);
				var Plan = _unitOfWork.GetRepository<Plan>().GetByID(CreatedMemberShip.PlanId);
				MemberShipToCreate.EndDate = DateTime.Now.AddDays(Plan!.DurationDays);
				_unitOfWork.GetRepository<MemberShip>().Add(MemberShipToCreate);
				return _unitOfWork.SaveChanges() > 0;
			}
			catch
			{
				return false;
			}
		}
		public bool DeleteMemberShip(int MemberId)
		{
			var Repo = _unitOfWork.GetRepository<MemberShip>();
			var ActiveMemberships = Repo.GetAll(X => X.MemberId == MemberId && X.Status == "Active").FirstOrDefault();
			if (ActiveMemberships is null) return false;
			Repo.Delete(ActiveMemberships);
			return _unitOfWork.SaveChanges() > 0;
		}
		public IEnumerable<MemberShipViewModel> GetAllMemberShips()
		{
			var MemberShips = _membershipRepository.GetAllMembershipsWithMemberAndPlan(X => X.Status == "Active");
			if (!MemberShips.Any()) return [];
			return _mapper.Map<IEnumerable<MemberShipViewModel>>(MemberShips);
		}
		public IEnumerable<PlanSelectListViewModel> GetPlansForDropDown()
		{
			var Plans = _unitOfWork.GetRepository<Plan>().GetAll(X => X.IsActive == true);
			return _mapper.Map<IEnumerable<PlanSelectListViewModel>>(Plans);
		}
		public IEnumerable<MemberSelectListViewModel> GetMembersForDropDown()
		{
			var Members = _unitOfWork.GetRepository<Member>().GetAll();
			return _mapper.Map<IEnumerable<MemberSelectListViewModel>>(Members);
		}

		#region Helper Methods 

		private bool IsMemberExists(int MemberId)
		{
			return _unitOfWork.GetRepository<Member>().GetAll(X => X.Id == MemberId).Any();
		}
		private bool IsPlanExists(int PlanId)
		{
            return _unitOfWork.GetRepository<Plan>().GetAll(X => X.Id == PlanId).Any();
        }
		private bool HasActiveMemberShip(int memberId)
		{
			return _unitOfWork.GetRepository<MemberShip>().GetAll(X => X.MemberId == memberId && X.Status == "Active").Any();
		}

		#endregion
	}
}
