﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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

                // Check if the current user is an admin
                bool isAdmin = User.IsInRole("Admin");

                // If admin is creating a user, ensure we don't auto-sign in the new user
                if (isAdmin)
                {
                    // Register the user without auto sign-in
                    IdentityResult result = await _userService.RegisterUserAsync(viewModel, false);

                    if (result.Succeeded)
                    {
                        // Redirect back to admin user management page
                        return RedirectToAction("Index", "Account"); // Adjust this to your actual admin user list page
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View("Create", viewModel);
                }
                else
                {
                    // Regular user registration with auto sign-in
                    IdentityResult result = await _userService.RegisterUserAsync(viewModel, true);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Explore", "Event");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View("Create", viewModel);
                }

                //IdentityResult result = await _userService.RegisterUserAsync(viewModel);

                //if (result.Succeeded)
                //{
                //    return RedirectToAction("Index", "Account");
                //}

                //foreach (var error in result.Errors)
                //{
                //    ModelState.AddModelError("", error.Description);
                //}
            }
            //must to do
            //if(User.IsInRole("Admin"))
            //{
            //    return View("Create", viewModel);
            //}
            //else
            //{
            //    return View("Register", viewModel);
            //}
            //to test create new user for admin
            return View("Create", viewModel);


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
        
        public async Task<IActionResult> Login()
        {
            LoginViewModel loginViewModel = new LoginViewModel()
            {
                Schemes = await _userService.AuthenticationSchemes()
            };
            return View(loginViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(viewModel, viewModel.RememberMe);

                if (result.Succeeded&&User.IsInRole("User"))
                {
                    return RedirectToAction("Explore", "Event");
                }
                else if(result.Succeeded && User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Email or password invalid");
                }
            }
            return View("Login", viewModel);
        }

        #endregion
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutUserAsync();
            return RedirectToAction("Index", "Home");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index(string searchEmail, int pageNumber = 1, int pageSize = 3)
        {
            var users = _userService.GetAllUsers();

            if (!string.IsNullOrEmpty(searchEmail))
            {
                users = users
                    .Where(u => u.Email != null && u.Email.Contains(searchEmail, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            var totalUsers = users.Count;

            var pagedUsers = users
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalUsers / pageSize);
            ViewBag.SearchEmail = searchEmail;

            return View(pagedUsers);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string id)
        {
            var user = _userService.GetUserById(id);
            return View(user);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditProfileViewModel model, IFormFile? ImageUrl)
        {
            if (ImageUrl != null && ImageUrl.Length > 0)
            {
                string uniqueFileName = UploadFile(ImageUrl);
                if (!string.IsNullOrEmpty(uniqueFileName))
                {
                    model.ProfileUrl = uniqueFileName;
                }
                else
                {
                    ModelState.AddModelError("", "Image upload failed.");
                    return View("Edit", model);
                }
            }

            // If no new image uploaded, keep the old ProfileUrl (already bound from hidden input)
            if (ModelState.IsValid)
            {
                var result = await _userService.UpdateUserAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Details(string id)
        {
            var user = _userService.GetUserById(id);
            return View(user);
        }
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        public  IActionResult ExternalLogin(string provider, string returnUrl = "")
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });

            var properties =  _userService.MyConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return new ChallengeResult(provider, properties);
        }
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = "", string remoteError = "")
        {
            LoginViewModel loginViewModel = new LoginViewModel()
            {
                Schemes = await _userService.AuthenticationSchemes()
            };

            if (!string.IsNullOrEmpty(remoteError))
            {
                ModelState.AddModelError("", $"error yabny {remoteError}");
                return View("login", loginViewModel);
            }

            var info = await _userService.MyGetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError("", $"error yabny {remoteError}");
                return View("login", loginViewModel);
            }

            var signInResult = await _userService.MyExternalLoginSignInAsync(info);
            if (signInResult.Succeeded)
            {
                return RedirectToAction("Explore", "Event");
            }

            else
            {
                var userEmail = info.Principal.FindFirstValue(ClaimTypes.Email);
                if (userEmail != null)
                {
                    var user = await _userService.MyFindByEmailAsync(userEmail);
                    if (user != null)
                    {
                        return RedirectToAction("Explore", "Event");
                    }

                }
                ModelState.AddModelError("", $"something error");
                return View("login", "Event");
            }
        }
    }
}
