using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tixora.Models;
using Tixora.Services.Interface;
using Tixora.ViewModels.UserViewModels;

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
        public async Task<IActionResult> Register(RegisterViewModel viewModel, IFormFile ImageUrl)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadFile(ImageUrl);
                if (uniqueFileName != null)
                {
                    viewModel.ProfileUrl = uniqueFileName;
                }
                else
                {
                    ModelState.AddModelError("", "Image upload failed");
                    return View("Register", viewModel);
                }

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
        [NonAction]
        private string? UploadFile(IFormFile ImageUrl)
        {
            string filePath = null;
            string uniqueFileName = null;

            // Handle Image Upload
            if (ImageUrl != null && ImageUrl.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/user");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImageUrl.FileName);
                filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ImageUrl.CopyTo(fileStream);
                }
                return uniqueFileName;
            }
            return null;
        }
        #region Login

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

        #endregion
        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutUserAsync();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Index()
        {
            var users = _userService.GetAllUsers();
            return View(users);
        }
        public IActionResult Edit(string id)
        {
            var user = _userService.GetUserById(id);
            return View(user);
        }
        [HttpPost]
        public IActionResult Edit(EditProfileViewModel model, IFormFile ImageUrl)
        {
            string uniqueFileName = UploadFile(ImageUrl);
            if (uniqueFileName != null)
            {
                model.ProfileUrl = uniqueFileName;
            }
            else
            {
                ModelState.AddModelError("", "Image upload failed");
                return View("Edit", model);
            }
            if (ModelState.IsValid)
            {
                var result = _userService.UpdateUserAsync(model);
                if (result.Result.Succeeded)
                {
                    return RedirectToAction("Index", "Account");
                }
                foreach (var error in result.Result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        public IActionResult Details(string id)
        {
            var user = _userService.GetUserById(id);
            return View(user);
        }
        [HttpGet]
        public IActionResult Delete(string id)
        {
            var user = _userService.GetUserById(id);
            if(user == null)
            {
                return NotFound("There is no Account for this id");
            }
            return View(user);
        }
        [HttpPost]
        public IActionResult ConfirmDelete(string Id)
        {
            var result = _userService.DeleteUserAsync(Id);
            if (result.Result.Succeeded)
            {
                return RedirectToAction("Index", "Account");
            }
            //foreach (var error in result.Result.Errors)
            //{
            //    ModelState.AddModelError("", error.Description);
            //}
            return NotFound("There is no Account for this id");

        }

    }
}
