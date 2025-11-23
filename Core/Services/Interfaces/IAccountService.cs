using Infrastructure.Entities.Users;

namespace Core.Services.Interfaces;

public interface IAccountService
{
    Task<ApplicationUser?> ValidateUserAsync(LoginViewModel input, CancellationToken cancellationToken = default);
}
