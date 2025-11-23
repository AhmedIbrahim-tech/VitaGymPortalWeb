using Infrastructure.Entities.Users;

namespace Core.Services.Classes;

public class AccountService(UserManager<ApplicationUser> _userManager) : IAccountService
{
    #region Validate User

    public async Task<ApplicationUser?> ValidateUserAsync(LoginViewModel input, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(input.Email);
        if (user == null)
        {
            return null;
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, input.Password);
        return isPasswordValid ? user : null;
    }

    #endregion
}
