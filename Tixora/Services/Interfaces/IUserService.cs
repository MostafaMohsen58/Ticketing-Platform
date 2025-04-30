using System.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Tixora.Models;
using Tixora.ViewModels.UserViewModels;

namespace Tixora.Services.Interface
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterViewModel model);
        Task<SignInResult> LoginUserAsync(LoginViewModel model, bool rememberMe);
        Task SignOutUserAsync();
        EditProfileViewModel GetUserById(string userId);
        Task<IdentityResult> UpdateUserAsync(EditProfileViewModel model);
        Task<IdentityResult> DeleteUserAsync(string userId);
        List<EditProfileViewModel> GetAllUsers();

        UserProfileViewModel GetUserProfileById(string userId);
        Task<IdentityResult> UpdateUserProfileAsync(UserProfileViewModel model);
        Task<IdentityResult> UpdateUserProfilePersonalInfo(PersonalInformation model);
        Task<IdentityResult> UpdateUserProfileAccountInfo(AccountInformation model, string userId);

        Task<IEnumerable<AuthenticationScheme>> AuthenticationSchemes();
        Task<AuthenticationProperties?> MyConfigureExternalAuthenticationProperties(string provider, string redirectUrl);
        Task<ExternalLoginInfo?> MyGetExternalLoginInfoAsync();
        Task<SignInResult> MyExternalLoginSignInAsync(ExternalLoginInfo info);
        Task<IdentityUser> MyFindByEmailAsync(string useremail);
    }
}
