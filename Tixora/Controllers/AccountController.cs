using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tixora.Models;
using Tixora.Services.Interface;
using Tixora.ViewModels;

namespace Tixora.Controllers
{
    public class AccountController : Controller
    {
        
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _userService.RegisterUserAsync(viewModel);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View("Register", viewModel);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(viewModel, viewModel.RememberMe);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Email or password invalid");
                }
            }
            return View("Login", viewModel);
        }

        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutUserAsync();
            return RedirectToAction("Register", "Account");
        }
    }
}
