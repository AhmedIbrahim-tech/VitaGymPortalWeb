using Infrastructure.Entities;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(IAccountService accountService,SignInManager<ApplicationUser> signInManager)
        {
            _accountService = accountService;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
               return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel input)
        {
            if(!ModelState.IsValid)
            {
                return View(input);
            }
            var user =_accountService.ValidiateUser(input);
            if(user == null)
            {
                ModelState.AddModelError("InvalidLogin", "Email or Password is not Vaild");
                return View(input);
            }

            var result = _signInManager.PasswordSignInAsync(user, input.Password, input.RememberMe, false).Result;

            if(result.IsNotAllowed)
                ModelState.AddModelError("InvalidLogin", "User is not Allowed To Login");
            if (result.IsLockedOut)
                ModelState.AddModelError("InvalidLogin", "User is Locked out and not Allowed To Login");
            if (result.Succeeded)
               return RedirectToAction("Index", "Home");
            
            return View(input);
        }
        [HttpPost]
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
