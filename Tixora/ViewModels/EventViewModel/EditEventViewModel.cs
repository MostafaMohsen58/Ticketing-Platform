using Tixora.Settings;
using Tixora.Validation;

namespace Tixora.ViewModels.EventViewModel
{
    public class EditEventViewModel : EventViewModel
    {
        public string? Curuntcover { get; set; }
        [AllowExtentions(FileSettings.AllowExtentions),
            MixSize(FileSettings.maxSizeByByets)]
        public IFormFile? Cover { get; set; } = default!;

    }
}
