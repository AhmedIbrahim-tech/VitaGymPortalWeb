using Infrastructure.Entities.Users.Identity;

namespace Core.Services.Interfaces;

public interface IAccountService
{
    Task<ApplicationUser?> ValidateUserAsync(LoginViewModel input, CancellationToken cancellationToken = default);
    Task<ApplicationUser?> GetUserByEmailAsync(string email);
}
