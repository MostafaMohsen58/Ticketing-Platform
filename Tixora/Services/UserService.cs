using Microsoft.AspNetCore.Identity;
using Tixora.Models;
using Tixora.Services.Interface;
using Tixora.ViewModels;

namespace Tixora.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterViewModel model)
        {
            ApplicationUser userFromDb = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = model.Gender,
                ProfileUrl = model.ProfileUrl,
                Address = model.Address,
                City = model.City,
                PhoneNumber = model.PhoneNumber,
                NationalId = model.NationalId,
                DateOfBrith = model.DateOfBrith,
            };

            IdentityResult identityResult = await _userManager.CreateAsync(userFromDb, model.Password);

            if (identityResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(userFromDb, "User");
                await _signInManager.SignInAsync(userFromDb, false);
            }

            return identityResult;
        }

        public async Task<SignInResult> LoginUserAsync(LoginViewModel model, bool rememberMe)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                //check password
                bool found = await _userManager.CheckPasswordAsync(user, model.Password);
                if (found)
                {
                    //create cookie By default add (Id - username - email - role)
                    await _signInManager.SignInAsync(user, model.RememberMe);
                    return SignInResult.Success;
                }
            }
            return SignInResult.Failed;

        }
        public async Task SignOutUserAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }
        public async Task<IdentityResult> UpdateUserProfileAsync(string userId, EditProfileViewModel model)
        {
            var user = await GetUserByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError[] { new IdentityError { Description = "User not found." } });
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Gender = model.Gender;
            user.ProfileUrl = model.ProfileUrl;
            user.Address = model.Address;
            user.City = model.City;
            user.PhoneNumber = model.PhoneNumber;

            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            var user = await GetUserByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError[] { new IdentityError { Description = "User not found." } });
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                await _signInManager.SignOutAsync();
            }
            return result;
        }
    }
}
