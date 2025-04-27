using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tixora.Services.Interface;
using Tixora.ViewModels.UserViewModels;

namespace Tixora.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }
        public ActionResult Edit()
        {
            Claim claimId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (claimId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var userId = claimId.Value;


            // Get the user's profile data from your service
            var user = _userService.GetUserProfileById(userId);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
       

        [HttpPost]
        public IActionResult UploadProfileImage(IFormFile image)
        {
            Claim claimId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (claimId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var userId = claimId.Value;
            var modelVM = _userService.GetUserProfileById(userId);

            if (modelVM == null)
            {
                return NotFound();
            }
            modelVM.PersonalInfo.UserId = userId;

            string uniqueFileName = UploadFile(image);
            if (uniqueFileName != null)
            {
                modelVM.ProfileImagePath = uniqueFileName;
            }
            else
            {
                ModelState.AddModelError("", "Image upload failed");
                return View("Edit", modelVM);
            }

            var result = _userService.UpdateUserProfileAsync(modelVM);
            if (result.Result.Succeeded)
            {
                return RedirectToAction("Edit", "Profile");
            }
            foreach (var error in result.Result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View("Edit", modelVM);
        }

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


        [HttpPost]
        public IActionResult SavePersonalInfo(UserProfileViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Claim claimId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (claimId == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                var userId = claimId.Value;
                
                PersonalInformation personalInfo = new PersonalInformation
                {
                    FanName = viewModel.PersonalInfo.FanName,
                    Email = viewModel.PersonalInfo.Email,
                    PhoneNumber = viewModel.PersonalInfo.PhoneNumber,
                    UserId = userId
                };

                var result = _userService.UpdateUserProfilePersonalInfo(personalInfo);
                if (result.Result.Succeeded)
                {
                    return RedirectToAction("Edit", "Profile");
                }
                foreach (var error in result.Result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("Edit",viewModel);
            }
            return View("Edit", viewModel);
        }

        [HttpPost]
        public IActionResult UpdateAccountInfo(UserProfileViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Claim claimId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (claimId == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                var userId = claimId.Value;
                AccountInformation AccountInfo = new AccountInformation
                {
                    City = viewModel.AccountInfo.City,
                    Address = viewModel.AccountInfo.Address,
                    OldPassword = viewModel.AccountInfo.OldPassword,
                    Password = viewModel.AccountInfo.Password,
                    ConfirmPassword = viewModel.AccountInfo.ConfirmPassword
                };

                var result = _userService.UpdateUserProfileAccountInfo(AccountInfo, userId);
                
                if (result.Result.Succeeded)
                {
                    return RedirectToAction("Edit", "Profile");
                }
                foreach (var error in result.Result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View("Edit", viewModel);
        }
    }
}
