using System.ComponentModel.DataAnnotations;
using Tixora.Validation;

namespace Tixora.ViewModels.UserViewModels
{
    public class UserProfileViewModel
    {
        public PersonalInformation? PersonalInfo { get; set; }
        public AccountInformation? AccountInfo { get; set; }
        public string? ProfileImagePath { get; set; }
    }
}
