using Tixora.Settings;
using Tixora.Validation;

namespace Tixora.ViewModels.EventViewModel
{
    public class EditEventViewModel : EventViewModel
    {
        [AllowExtentions(FileSettings.AllowExtentions),
            MixSize(FileSettings.maxSizeByByets)]
        public IFormFile? Cover { get; set; } = default!;


    }
}
