using Microsoft.AspNetCore.Identity;
using Tixora.Models;
using Tixora.Services.Interface;
using Tixora.ViewModels;

namespace Tixora.Services
{
    public class UserService:IUserService
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
    }
}
