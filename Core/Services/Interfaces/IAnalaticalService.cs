namespace Core.Services.Interfaces;

public interface IAnalaticalService
{
    Task<AnalaticalViewModel> GetAnalaticalDataAsync(CancellationToken cancellationToken = default);
}
