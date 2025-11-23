namespace Core.Services.Interfaces;

public interface ITrainerService
{
    Task<bool> CreateTrainerAsync(CreateTrainerViewModel trainerViewModel, CancellationToken cancellationToken = default);
    Task<bool> UpdateTrainerAsync(int id, TrainerToUpdateViewModel trainerViewModel, CancellationToken cancellationToken = default);
    Task<bool> RemoveTrainerAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TrainerViewModel>> GetAllTrainersAsync(CancellationToken cancellationToken = default);
    Task<TrainerViewModel?> GetTrainerDetailsAsync(int id, CancellationToken cancellationToken = default);
    Task<TrainerToUpdateViewModel?> GetTrainerToUpdateAsync(int id, CancellationToken cancellationToken = default);
}
