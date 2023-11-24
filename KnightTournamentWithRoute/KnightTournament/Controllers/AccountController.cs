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
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Route("Register")]
        public async Task<IActionResult> Register()
        {
            RegisterViewModel registerViewModel = new RegisterViewModel();
            return View(registerViewModel);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var user = new AppUser() { UserName = registerViewModel.UserName, Email = registerViewModel.Email };
            var result = await _userManager.CreateAsync(user, registerViewModel.Password);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("Password", "Cannot create user");
            }

            await _signInManager.SignInAsync(user, isPersistent: true);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            LoginViewModel loginViewModel = new LoginViewModel();
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
            if (!result.Succeeded)
            {
                loginViewModel.ErrorMessage = "Invalid credentials";
                return View(loginViewModel);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("LogOff")]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
