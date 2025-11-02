using Core.ViewModels;

namespace Core.Services.Classes
{
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlanService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public bool Activate(int planID)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetByID(planID);
            if (plan == null || HasActiveMemberShips(planID))
            {
                return false;
            }
            plan.IsActive = !plan.IsActive;
            plan.UpdatedAt = DateTime.Now;

            _unitOfWork.GetRepository<Plan>().Update(plan);
            return _unitOfWork.SaveChanges() > 0 ;
        }

        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var plans = _unitOfWork.GetRepository<Plan>().GetAll();
            if (plans == null||!plans.Any())
            {
                return [];
            }
            return plans.Select(p => new PlanViewModel
            { 
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                DurationDays = p.DurationDays,
                Price = p.Price,
                IsActive = p.IsActive
            });

        }

        public PlanViewModel? GetPlanById(int planID)
        {

            var plan = _unitOfWork.GetRepository<Plan>().GetByID(planID);
            if (plan == null )
            {
                return null;
            }
            return  new PlanViewModel
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                Price = plan.Price,
                IsActive = plan.IsActive
            };
        }

        public UpdatePlanViewModel? GetPlanToUpdate(int planID)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetByID(planID);
            if (plan == null||plan.IsActive ==false)
            {
                return null;
            }

            return new UpdatePlanViewModel
            {
               
                PlanName = plan.Name,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                Price = plan.Price,
                
            };

        }

        public bool UpdatePlan(int planID, UpdatePlanViewModel viewModel)
        {

            try
            {
                var plan = _unitOfWork.GetRepository<Plan>().GetByID(planID);
                if (plan == null || HasActiveMemberShips(planID))
                {
                    return false;
                }
                plan.Description = viewModel.Description;
                plan.DurationDays = viewModel.DurationDays;
                plan.Price = viewModel.Price;
                plan.Name = viewModel.PlanName;
                plan.UpdatedAt = DateTime.Now;

                _unitOfWork.GetRepository<Plan>().Update(plan);
                return _unitOfWork.SaveChanges() > 0;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #region Helper Methods
        private bool HasActiveMemberShips(int planID)
        {
            return _unitOfWork.GetRepository<MemberShip>().GetAll(ms =>ms.PlanId == planID&&ms.Status=="Active").Any();
        }
        #endregion
    }
}
