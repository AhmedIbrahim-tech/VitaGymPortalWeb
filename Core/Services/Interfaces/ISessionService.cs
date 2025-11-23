namespace Core.Services.Interfaces;

public interface ISessionService
{
    Task<IEnumerable<SessionViewModel>> GetAllSessionsAsync(CancellationToken cancellationToken = default);
    Task<SessionViewModel?> GetSessionByIDAsync(int id, CancellationToken cancellationToken = default);
    Task<UpdateSessionViewModel?> GetSessionToUpdateAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> CreateSessionAsync(CreateSessionViewModel viewModel, CancellationToken cancellationToken = default);
    Task<bool> UpdateSessionAsync(int id, UpdateSessionViewModel viewModel, CancellationToken cancellationToken = default);
    Task<bool> RemoveSessionAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TrainerSelectViewModel>> LoadTrainersDropDownAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<CategorySelectViewModel>> LoadCategoriesDropDownAsync(CancellationToken cancellationToken = default);
}
