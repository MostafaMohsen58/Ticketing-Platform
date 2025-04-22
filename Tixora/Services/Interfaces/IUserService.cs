using Microsoft.AspNetCore.Identity;
using Tixora.Models;
using Tixora.ViewModels;

namespace Tixora.Services.Interface
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterViewModel model);
        Task<SignInResult> LoginUserAsync(LoginViewModel model, bool rememberMe);
        Task SignOutUserAsync();
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<IdentityResult> UpdateUserProfileAsync(string userId, EditProfileViewModel model);
        Task<IdentityResult> DeleteUserAsync(string userId);
    }
}
