namespace Core.Services.Interfaces;

public interface IPlanService
{
    Task<bool> UpdatePlanAsync(int planID, UpdatePlanViewModel viewModel, CancellationToken cancellationToken = default);
    Task<UpdatePlanViewModel?> GetPlanToUpdateAsync(int planID, CancellationToken cancellationToken = default);
    Task<IEnumerable<PlanViewModel>> GetAllPlansAsync(CancellationToken cancellationToken = default);
    Task<PlanViewModel?> GetPlanByIdAsync(int planID, CancellationToken cancellationToken = default);
    Task<bool> ActivateAsync(int planID, CancellationToken cancellationToken = default);
}
