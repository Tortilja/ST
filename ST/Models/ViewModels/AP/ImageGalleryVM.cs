using Microsoft.AspNetCore.Mvc.Rendering;

namespace ST.Models.ViewModels.AP
{
    public class ImageGalleryVM
    {
        public ImageGallery ImageGallery { get; set; }
        public IEnumerable<SelectListItem>? EventSelectList { get; set; }
    }
}
