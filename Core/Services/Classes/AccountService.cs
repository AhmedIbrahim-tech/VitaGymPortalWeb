namespace Core.Services.Classes
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public ApplicationUser? ValidiateUser(LoginViewModel input)
        {
            var user = _userManager.FindByEmailAsync(input.Email).Result;
            if (user == null) return null;

            var IsPasswordVaild = _userManager.CheckPasswordAsync(user, input.Password).Result;
            return IsPasswordVaild ? user : null;

        }
    }
}
