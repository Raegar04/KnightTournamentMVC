using KnightTournament.Models;
using KnightTournament.Models.Enums;
using KnightTournament.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KnightTournament.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [Route("Register")]
        public async Task<IActionResult> Register(string registerUrl = null)
        {
            //if (!await _roleManager.RoleExistsAsync(Role.StakeHolder.ToString()))
            //{
            //    await _roleManager.CreateAsync(new IdentityRole(Role.StakeHolder.ToString()));
            //    await _roleManager.CreateAsync(new IdentityRole(Role.Knight.ToString()));
            //}
            RegisterViewModel registerViewModel = new RegisterViewModel();
            registerViewModel.ReturnUrl = registerUrl;
            return View(registerViewModel);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel, string registerUrl = null)
        {

            registerViewModel.ReturnUrl = registerUrl;
            registerUrl = registerUrl ?? Url.Content("~/");

            var user = new AppUser() { UserName = registerViewModel.UserName, Email = registerViewModel.Email };
            var result = await _userManager.CreateAsync(user, registerViewModel.Password);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("Password", "Cannot create user");
            }

            //await _userManager.AddToRoleAsync(user, registerViewModel.SelectedRole.ToString());
            await _signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login(string registerUrl = null)
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            loginViewModel.ReturnUrl = registerUrl ?? Url.Content("~/");
            return View(loginViewModel);
        }

        [HttpGet]
        [Route("Detail")]
        public async Task<IActionResult> Detail()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(user);
        }

        [HttpPost]
        [Route("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var result = await _signInManager.PasswordSignInAsync(
                                loginViewModel.UserName, loginViewModel.Password, loginViewModel.RememberMe, lockoutOnFailure: false);
            //if (result.IsLockedOut) { return View("Lockout", $"You've been locked out. Try in {_signInManager.Options.Lockout.DefaultLockoutTimeSpan.ToString()}"); }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid login try");
                return View(loginViewModel);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //[HttpGet]
        //[Route("ForgotPassword")]
        //public async Task<IActionResult> ForgotPassword()
        //{
        //    var user = (await _userManager.GetUserAsync(User));
        //    var forgotPasswordVM = new ForgotPasswordViewModel() { Email = user.Email };
        //    ForgotPasswordViewModel.CorrectCode = new Random().Next(1000, 9999).ToString();
        //    return View(forgotPasswordVM);
        //}
        //[HttpPost]
        //[Route("ForgotPassword")]
        //public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
        //{
        //    if (forgotPasswordViewModel.Code == ForgotPasswordViewModel.CorrectCode)
        //    {
        //        return RedirectToAction("ChangePassword");
        //    }
        //    return RedirectToAction("Index", "Home");
        //}
        //[HttpGet]
        //[Route("ChangePassword")]
        //public async Task<IActionResult> ChangePassword()
        //{
        //    return View(new ChangePasswordViewModel());
        //}
        //[HttpPost]
        //[Route("ChangePassword")]
        //public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    await _userManager.ChangePasswordAsync(user, user.PasswordHash, changePasswordViewModel.Password);
        //    return RedirectToAction("Index", "Home");
        //}
    }
}
