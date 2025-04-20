using Microsoft.AspNetCore.Identity;
using Tixora.ViewModels;

namespace Tixora.Services.Interface
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterViewModel model);
        Task<SignInResult> LoginUserAsync(LoginViewModel model, bool rememberMe);
        Task SignOutUserAsync();
    }
}
