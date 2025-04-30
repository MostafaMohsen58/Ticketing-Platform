using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Tixora.Models;
using Tixora.Services.Interface;
using Tixora.ViewModels.UserViewModels;

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


        public EditProfileViewModel? GetUserById(string userId)
        {
            var user = _userManager.FindByIdAsync(userId);

            if (user.Result != null)
            {
                var userViewModel = new EditProfileViewModel()
                {
                    Id = user.Result.Id,
                    Email = user.Result.Email,
                    ProfileUrl = user.Result.ProfileUrl,
                    Address = user.Result.Address,
                    City = user.Result.City,
                    PhoneNumber = user.Result.PhoneNumber
                };
                return userViewModel;
            }
            return null;
        }
        public async Task<IdentityResult> UpdateUserAsync(EditProfileViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError[] { new IdentityError { Description = "User not found." } });
            }


            user.ProfileUrl = model.ProfileUrl;
            user.Address = model.Address;
            user.City = model.City;
            user.PhoneNumber = model.PhoneNumber;

            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
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
        public List<EditProfileViewModel> GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            var userViewModels = new List<EditProfileViewModel>();
            foreach (var user in users)
            {
                var userVM = new EditProfileViewModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    ProfileUrl = user.ProfileUrl,
                    Address = user.Address,
                    City = user.City,
                    PhoneNumber = user.PhoneNumber
                };
                userViewModels.Add(userVM);
            }
            return userViewModels;

        }


        public UserProfileViewModel? GetUserProfileById(string userId)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                var viewModel = new UserProfileViewModel
                {
                    PersonalInfo = new PersonalInformation
                    {
                        FanName = string.Concat(user.FirstName, ' ', user.LastName),
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber
                    },
                    AccountInfo = new AccountInformation
                    {
                        City = user.City,
                        Address = user.Address
                    },
                    ProfileImagePath = user.ProfileUrl
                };
                return viewModel;
            }
            else
            {
                return null;
            }
        }
        public async Task<IdentityResult> UpdateUserProfileAsync(UserProfileViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.PersonalInfo.UserId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError[] { new IdentityError { Description = "User not found." } });
            }
            
            user.ProfileUrl = model.ProfileImagePath;

            return await _userManager.UpdateAsync(user);
        }
        public async Task<IdentityResult> UpdateUserProfilePersonalInfo(PersonalInformation model)
        {
            var user =await _userManager.Users.FirstOrDefaultAsync(u => u.Id == model.UserId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError[] { new IdentityError { Description = "User not found." } });
            }
            
            user.FirstName = model.FanName.Split(' ')[0];
            user.LastName = model.FanName.Split(' ')[1];
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;

            return await _userManager.UpdateAsync(user);
        }
        public async Task<IdentityResult> UpdateUserProfileAccountInfo(AccountInformation model,string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError[] { new IdentityError { Description = "User not found." } });
            }
            user.Address = model.Address;
            user.City = model.City;
            await _userManager.UpdateAsync(user);

            bool found = await _userManager.CheckPasswordAsync(user, model.OldPassword);
            if(!found)
            {
                return IdentityResult.Failed(new IdentityError[] { new IdentityError { Description = "Password is incorrect." } });
            }
            if (model.OldPassword != null)
            {
                var passwordChangeResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);

                if (passwordChangeResult.Succeeded)
                {
                    return passwordChangeResult;
                }

            }
            return IdentityResult.Failed(new IdentityError[] { new IdentityError { Description = "Please enter password." } });

        }

        public async Task<IEnumerable<AuthenticationScheme>> AuthenticationSchemes()
        {
            return await _signInManager.GetExternalAuthenticationSchemesAsync();
        }

        public async Task<AuthenticationProperties?> MyConfigureExternalAuthenticationProperties(string provider, string redirectUrl ="")
        {
            return _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        }

        public async Task<ExternalLoginInfo?> MyGetExternalLoginInfoAsync()
        {
            try
            {
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return null;
                }                
                return info;               
            }
            catch (Exception ex)
            {              
                return null;
            }
        }

        public async Task<SignInResult> MyExternalLoginSignInAsync(ExternalLoginInfo info)
        {
            try
            {
                var result = await _signInManager.ExternalLoginSignInAsync(
                    info.LoginProvider,
                    info.ProviderKey,
                    isPersistent: false,
                    bypassTwoFactor: true
                );
                return result;
            }
            catch (Exception ex)
            {
                return SignInResult.Failed;
            }
        }

        public async Task<IdentityUser> MyFindByEmailAsync(string useremail)
        {
            var user = await _userManager.FindByEmailAsync(useremail);

            if (user == null)
            {
                 user = new ApplicationUser
                 {
                     UserName = useremail,
                     Email = useremail,
                     EmailConfirmed = true,
                     Address = "Default Address",
                     City = "Default City",
                     DateOfBrith = new DateOnly(2000, 1, 1), // ✅ تاريخ صالح
                     FirstName = "First",
                     LastName = "Last",
                     Gender = Gender.Male, // أو Gender.Female حسب نوع enum
                     PhoneNumber = "01000000000",
                     NationalId = "00000000000000"

                 };

                var result = await _userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    // إضافة إلى الدور بعد نجاح الإنشاء
                    await _userManager.AddToRoleAsync(user, "User");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return user;
                }

                return null;
            }

            return user;
        }




    }


}

